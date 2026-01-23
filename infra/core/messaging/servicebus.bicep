metadata description = 'Creates an Azure Service Bus namespace for messaging.'
param name string
param location string = resourceGroup().location
param tags object = {}

param sku object = {
  name: 'Basic'
  tier: 'Basic'
}

param keyVaultName string
param keyVaultResourceGroup string = ''
param connectionStringKey string = 'ConnectionStrings--RabbitMqConnection'

resource keyVaultRg 'Microsoft.Resources/resourceGroups@2021-04-01' existing = {
  scope: subscription()
  name: !empty(keyVaultResourceGroup) ? keyVaultResourceGroup : resourceGroup().name
}

resource keyVault 'Microsoft.KeyVault/vaults@2022-07-01' existing = {
  name: keyVaultName
  scope: keyVaultRg
}

resource serviceBusNamespace 'Microsoft.ServiceBus/namespaces@2022-10-01-preview' = {
  name: name
  location: location
  tags: tags
  sku: sku
  properties: {
    minimumTlsVersion: '1.2'
    publicNetworkAccess: 'Enabled'
  }
}

resource serviceBusAuthorizationRule 'Microsoft.ServiceBus/namespaces/AuthorizationRules@2022-10-01-preview' existing = {
  parent: serviceBusNamespace
  name: 'RootManageSharedAccessKey'
}

// Service Bus connection string in RabbitMQ-compatible format (AMQP 1.0)
var connectionString = serviceBusAuthorizationRule.listKeys().primaryConnectionString

module connectionStringSecret '../security/keyvault-secret.bicep' = {
  name: 'servicebus-connectionString-secret'
  scope: keyVaultRg
  params: {
    name: connectionStringKey
    keyVaultName: keyVault.name
    secretValue: connectionString
  }
}

output name string = serviceBusNamespace.name
output endpoint string = serviceBusNamespace.properties.serviceBusEndpoint
output connectionStringKey string = connectionStringKey
