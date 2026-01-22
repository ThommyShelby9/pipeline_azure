param name string
param location string = resourceGroup().location
param tags object = {}

param serviceName string = 'web'
param applicationInsightsName string = ''
param keyVaultName string = ''
param deploymentSuffix string = utcNow()

// App Service Plan - S1 required for deployment slots
module appServicePlan '../core/host/appserviceplan.bicep' = {
  name: 'appServicePlan-${deploymentSuffix}'
  params: {
    name: '${name}-plan'
    location: location
    tags: tags
    sku: {
      name: 'S1'
      tier: 'Standard'
      capacity: 1
    }
  }
}

// Production App Service
module appService '../core/host/appservice.bicep' = {
  name: 'appService-${deploymentSuffix}'
  params: {
    name: name
    location: location
    tags: union(tags, { 'azd-service-name': serviceName })
    appServicePlanId: appServicePlan.outputs.id
    applicationInsightsName: applicationInsightsName
    keyVaultName: keyVaultName
    runtimeName: 'dotnetcore'
    runtimeVersion: '8.0'
    healthCheckPath: '/health'
    appSettings: {
      ASPNETCORE_ENVIRONMENT: 'Production'
    }
    deploymentSuffix: deploymentSuffix
  }
}

// Staging Slot for Blue-Green deployment
module stagingSlot '../core/host/appservice-slot.bicep' = {
  name: 'stagingSlot-${deploymentSuffix}'
  params: {
    name: 'staging'
    location: location
    tags: union(tags, { 'azd-slot-name': 'staging' })
    appServiceName: appService.outputs.name
    appServicePlanId: appServicePlan.outputs.id
    applicationInsightsName: applicationInsightsName
    keyVaultName: keyVaultName
    runtimeName: 'dotnetcore'
    runtimeVersion: '8.0'
    healthCheckPath: '/health'
    appSettings: {
      ASPNETCORE_ENVIRONMENT: 'Staging'
    }
  }
}

output name string = appService.outputs.name
output uri string = appService.outputs.uri
output identityPrincipalId string = appService.outputs.identityPrincipalId
output stagingSlotName string = stagingSlot.outputs.name
output stagingSlotUri string = stagingSlot.outputs.uri
output stagingSlotIdentityPrincipalId string = stagingSlot.outputs.identityPrincipalId
