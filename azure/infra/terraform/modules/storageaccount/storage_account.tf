resource "azurerm_storage_account" "secret_01" {
  #name = format("%s%s", trim(local.small_name, "-"), "storage")
  name                     = format("%s%s", format("%s%s", substr(var.secrets_rg_name, 0, 7), substr(var.secrets_rg_name, 8, 7)), "storage")
  location                 = var.rg_location
  resource_group_name      = var.secrets_rg_name
  account_tier             = "Standard"
  account_replication_type = "LRS"
  allow_blob_public_access       = true 

  tags = merge({
    },
    var.common_tags
  )
}

# resource "azurerm_storage_account" "funcappsa" {
#   #name = format("%s%s", trim(local.small_name, "-"), "fapp")
#   name                     = format("%s%s", format("%s%s", substr(var.func_rg_name, 0, 7), substr(var.func_rg_name, 8, 7)), "fapp")
#   resource_group_name      = var.func_rg_name
#   location                 = var.rg_location
#   account_tier             = "Standard"
#   account_replication_type = "LRS"

#   tags = merge({
#     },
#     var.common_tags
#   )

# }
