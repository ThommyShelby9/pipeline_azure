metadata description = 'Creates an Azure App Service deployment slot.'

param name string
param location string = resourceGroup().location
param tags object = {}

param appServiceName string
param appServicePlanId string

// Runtime Properties
@allowed([
  'dotnet', 'dotnetcore', 'dotnet-isolated', 'node', 'python', 'java', 'powershell', 'custom'
])
param runtimeName string
param runtimeVersion string
param runtimeNameAndVersion string = '${runtimeName}|${runtimeVersion}'

// Slot Properties
param kind string = 'app,linux'
param alwaysOn bool = true
param ftpsState string = 'FtpsOnly'
param healthCheckPath string = ''
@secure()
param appSettings object = {}
param applicationInsightsName string = ''
param keyVaultName string = ''
param keyVaultResourceGroup string = ''
param managedIdentity bool = !empty(keyVaultName)

resource appService 'Microsoft.Web/sites@2022-03-01' existing = {
  name: appServiceName
}

resource slot 'Microsoft.Web/sites/slots@2022-03-01' = {
  name: name
  parent: appService
  location: location
  tags: tags
  kind: kind
  properties: {
    serverFarmId: appServicePlanId
    siteConfig: {
      linuxFxVersion: runtimeNameAndVersion
      alwaysOn: alwaysOn
      ftpsState: ftpsState
      minTlsVersion: '1.2'
      healthCheckPath: healthCheckPath
    }
    httpsOnly: true
  }

  identity: { type: managedIdentity ? 'SystemAssigned' : 'None' }
}

resource slotConfig 'Microsoft.Web/sites/slots/config@2022-03-01' = {
  name: 'appsettings'
  parent: slot
  properties: union(appSettings,
    !empty(applicationInsightsName) ? { APPLICATIONINSIGHTS_CONNECTION_STRING: applicationInsights!.properties.ConnectionString } : {},
    !empty(keyVaultName) ? { AZURE_KEY_VAULT_ENDPOINT: keyVault!.properties.vaultUri } : {})
}

resource keyVaultRg 'Microsoft.Resources/resourceGroups@2021-04-01' existing = {
  scope: subscription()
  name: !empty(keyVaultResourceGroup) ? keyVaultResourceGroup : resourceGroup().name
}

resource keyVault 'Microsoft.KeyVault/vaults@2022-07-01' existing = if (!(empty(keyVaultName))) {
  name: keyVaultName
  scope: keyVaultRg
}

resource applicationInsights 'Microsoft.Insights/components@2020-02-02' existing = if (!empty(applicationInsightsName)) {
  name: applicationInsightsName
}

output name string = slot.name
output uri string = 'https://${slot.properties.defaultHostName}'
output identityPrincipalId string = managedIdentity ? slot.identity.principalId : ''
