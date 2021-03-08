
# #core key vault
# resource "azurerm_key_vault" "core_kv" {
#   name = format("%s-%s", local.small_name, "c-kv-01")
#   #name                            = format("%s-%s", var.core_rg_name, "kv-01")
#   location                        = var.rg_location
#   resource_group_name             = var.core_rg_name
#   tenant_id                       = data.azurerm_client_config.current.tenant_id
#   enabled_for_disk_encryption     = true
#   enabled_for_deployment          = true
#   enabled_for_template_deployment = true
#   sku_name                        = "premium"

#   tags = merge({
#     },
#     var.common_tags
#   )

# }

##secrets keyvault
resource "azurerm_key_vault" "secrets_kv" {
  name = format("%s-%s", local.small_name, "s-kv-01")
  #name                            = format("%s-%s", var.secrets_rg_name, "kv")
  location                        = var.rg_location
  resource_group_name             = var.secrets_rg_name
  tenant_id                       = data.azurerm_client_config.current.tenant_id
  enabled_for_disk_encryption     = false
  enabled_for_deployment          = false
  enabled_for_template_deployment = true
  sku_name                        = "standard"

  network_acls {
    default_action = "Deny"
    bypass         = "AzureServices"
    #    ip_rules                   = ["79.70.26.28/32", ]
    ip_rules = [ #"217.32.40.255/32",
    "176.250.148.253/32", ]
    virtual_network_subnet_ids = [var.projcore_default_sn_id]
  }

  tags = merge({
    },
    var.common_tags
  )

}
