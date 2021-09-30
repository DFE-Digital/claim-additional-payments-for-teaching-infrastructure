# NOTE: the Name used for Redis needs to be globally unique
resource "azurerm_redis_cache" "redis_01" {
  name                = format("%s-%s", var.app_rg_name, "redis-cache")
  location            = var.rg_location
  resource_group_name = var.app_rg_name
  capacity            = 2
  family              = "C"
  sku_name            = "Standard"
  # hostname
  enable_non_ssl_port = false
  minimum_tls_version = "1.2"

  redis_configuration {
    enable_authentication = true
    #maxclients                      = 1000
    maxfragmentationmemory_reserved = 2
    maxmemory_delta                 = 2
    maxmemory_policy                = "volatile-lru"
    maxmemory_reserved              = 2
  }

  tags = merge({
    },
    var.common_tags
  )
}
