targetScope = 'subscription'

@minLength(1)
@maxLength(64)
@description('Name of the the environment which is used to generate a short unique hash used in all resources.')
param environmentName string

@minLength(1)
@description('Primary location for all resources')
param location string

@description('Id of the user or app to assign application roles')
param principalId string

// Optional parameters to override the default azd resource naming conventions.
// Add the following to main.parameters.json to provide values:
// "resourceGroupName": {
//      "value": "myGroupName"
// }
param resourceGroupName string = ''
param logAnalyticsName string = ''
param applicationInsightsName string = ''
param applicationInsightsDashboardName string = ''
param keyVaultName string = ''
param keyVaultResourceGroup string = ''
param appServiceName string = ''
param dbServerName string = ''
param dbName string = ''

@secure()
param dbAdminPassword string

@secure()
param dbAppUserPassword string

@description('Unique suffix to append to deployment names to avoid conflicts')
param deploymentSuffix string = utcNow()

var abbrs = loadJsonContent('./abbreviations.json')

// Tags that should be applied to all resources.
// 
// Note that 'azd-service-name' tags should be applied separately to service host resources.
// Example usage:
//   tags: union(tags, { 'azd-service-name': <service name in azure.yaml> })
var tags = {
  'azd-env-name': environmentName
}

// Generate a unique token to be used in naming resources.
var resourceToken = toLower(uniqueString(subscription().id, environmentName, location))

// Name of the service defined in azure.yaml
// A tag named azd-service-name with this value should be applied to the service host resource, such as:
//   Microsoft.Web/sites for appservice, function
// Example usage:
//   tags: union(tags, { 'azd-service-name': apiServiceName })
var webServiceName = 'web'

// Organize resources in a resource group
resource rg 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: !empty(resourceGroupName) ? resourceGroupName : '${abbrs.resourcesResourceGroups}${environmentName}'
  location: location
  tags: tags
}

// Add resources to be provisioned below.

// Reference existing resource group where Key Vault is located
resource keyVaultRg 'Microsoft.Resources/resourceGroups@2021-04-01' existing = {
  name: !empty(keyVaultResourceGroup) ? keyVaultResourceGroup : resourceGroupName
}

module monitoring 'core/monitor/monitoring.bicep' = {
  name: 'monitoring-${deploymentSuffix}'
  params: {
    location: location
    tags: tags
    logAnalyticsName: !empty(logAnalyticsName) ? logAnalyticsName : '${abbrs.operationalInsightsWorkspaces}${resourceToken}'
    applicationInsightsName: !empty(applicationInsightsName) ? applicationInsightsName : '${abbrs.insightsComponents}${resourceToken}'
    applicationInsightsDashboardName: !empty(applicationInsightsDashboardName) ? applicationInsightsDashboardName : '${abbrs.portalDashboards}${resourceToken}'
    deploymentSuffix: deploymentSuffix
  }
  scope: rg
}

// Reference existing Key Vault in its resource group (eastus)
resource keyVault 'Microsoft.KeyVault/vaults@2022-07-01' existing = {
  name: !empty(keyVaultName) ? keyVaultName : '${abbrs.keyVaultVaults}${resourceToken}'
  scope: keyVaultRg
}

module web 'services/web.bicep' = {
  name: 'web-${deploymentSuffix}'
  params: {
    name: !empty(appServiceName) ? appServiceName : '${abbrs.webSitesAppService}${resourceToken}'
    location: location
    tags: tags
    serviceName: webServiceName
    applicationInsightsName: monitoring.outputs.applicationInsightsName
    keyVaultName: keyVault.name
    keyVaultResourceGroup: keyVaultRg.name
    deploymentSuffix: deploymentSuffix
  }
  scope: rg
}


// PostgreSQL Flexible Server
module database 'core/database/postgresql/flexibleserver.bicep' = {
  name: 'database-${deploymentSuffix}'
  params: {
    name: !empty(dbServerName) ? dbServerName : '${abbrs.postgreSQLServers}${resourceToken}'
    location: location
    tags: tags
    databaseName: !empty(dbName) ? dbName : '${abbrs.postgreSQLServersDatabases}${resourceToken}'
    keyVaultName: keyVault.name
    keyVaultResourceGroup: keyVaultRg.name
    connectionStringKey: 'ConnectionStrings--DefaultConnection'
    administratorLogin: 'psqladmin'
    administratorLoginPassword: dbAdminPassword
    appUserLogin: 'appuser'
    appUserLoginPassword: dbAppUserPassword
    version: '16'
    allowAzureIPsFirewall: true
    sku: {
      name: 'Standard_B1ms'
      tier: 'Burstable'
    }
    storage: {
      storageSizeGB: 32
    }
  }
  scope: rg
}

// Azure Cache for Redis
module redis 'core/cache/redis.bicep' = {
  name: 'redis-${deploymentSuffix}'
  params: {
    name: '${abbrs.cacheRedis}${resourceToken}'
    location: location
    tags: tags
    keyVaultName: keyVault.name
    keyVaultResourceGroup: keyVaultRg.name
    connectionStringKey: 'ConnectionStrings--RedisConnection'
    sku: {
      name: 'Basic'
      family: 'C'
      capacity: 0
    }
  }
  scope: rg
}

// Azure Service Bus (RabbitMQ alternative)
module serviceBus 'core/messaging/servicebus.bicep' = {
  name: 'servicebus-${deploymentSuffix}'
  params: {
    name: '${abbrs.serviceBusNamespaces}${resourceToken}'
    location: location
    tags: tags
    keyVaultName: keyVault.name
    keyVaultResourceGroup: keyVaultRg.name
    connectionStringKey: 'ConnectionStrings--RabbitMqConnection'
    sku: {
      name: 'Basic'
      tier: 'Basic'
    }
  }
  scope: rg
}

module webKeyVaultAccess 'core/security/keyvault-access.bicep' = {
  name: 'webKeyVaultAccess-${deploymentSuffix}'
  params: {
    keyVaultName: keyVault.name
    principalId: web.outputs.identityPrincipalId
  }
  scope: keyVaultRg
}

// Key Vault access for staging slot
module stagingSlotKeyVaultAccess 'core/security/keyvault-access.bicep' = {
  name: 'stagingSlotKeyVaultAccess-${deploymentSuffix}'
  params: {
    keyVaultName: keyVault.name
    principalId: web.outputs.stagingSlotIdentityPrincipalId
  }
  scope: keyVaultRg
}

// Add outputs from the deployment here, if needed.
//
// This allows the outputs to be referenced by other bicep deployments in the deployment pipeline,
// or by the local machine as a way to reference created resources in Azure for local development.
// Secrets should not be added here.
//
// Outputs are automatically saved in the local azd environment .env file.
// To see these outputs, run `azd env get-values`,  or `azd env get-values --output json` for json output.
output AZURE_LOCATION string = location
output AZURE_TENANT_ID string = tenant().tenantId
output AZURE_KEY_VAULT_NAME string = keyVault.name
output AZURE_KEY_VAULT_ENDPOINT string = keyVault.properties.vaultUri
output APPLICATIONINSIGHTS_CONNECTION_STRING string = monitoring.outputs.applicationInsightsConnectionString
output AZURE_SQL_CONNECTION_STRING_KEY string = database.outputs.connectionStringKey
output WEB_BASE_URI string = web.outputs.uri
output WEB_STAGING_URI string = web.outputs.stagingSlotUri
