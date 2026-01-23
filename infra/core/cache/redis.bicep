metadata description = 'Creates an Azure Cache for Redis instance.'
param name string
param location string = resourceGroup().location
param tags object = {}

param sku object = {
  name: 'Basic'
  family: 'C'
  capacity: 0
}

param keyVaultName string
param keyVaultResourceGroup string = ''
param connectionStringKey string = 'ConnectionStrings--RedisConnection'

resource keyVaultRg 'Microsoft.Resources/resourceGroups@2021-04-01' existing = {
  scope: subscription()
  name: !empty(keyVaultResourceGroup) ? keyVaultResourceGroup : resourceGroup().name
}

resource keyVault 'Microsoft.KeyVault/vaults@2022-07-01' existing = {
  name: keyVaultName
  scope: keyVaultRg
}

resource redis 'Microsoft.Cache/redis@2023-08-01' = {
  name: name
  location: location
  tags: tags
  properties: {
    sku: sku
    enableNonSslPort: false
    minimumTlsVersion: '1.2'
    publicNetworkAccess: 'Enabled'
    redisConfiguration: {
      'maxmemory-policy': 'allkeys-lru'
    }
  }
}

var connectionString = '${redis.properties.hostName}:${redis.properties.sslPort},password=${redis.listKeys().primaryKey},ssl=True,abortConnect=False'

module connectionStringSecret '../security/keyvault-secret.bicep' = {
  name: 'redis-connectionString-secret'
  scope: keyVaultRg
  params: {
    name: connectionStringKey
    keyVaultName: keyVault.name
    secretValue: connectionString
  }
}

output name string = redis.name
output hostName string = redis.properties.hostName
output sslPort int = redis.properties.sslPort
output connectionStringKey string = connectionStringKey
