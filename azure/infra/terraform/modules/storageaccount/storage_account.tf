resource "azurerm_storage_account" "secret_01" {
  name                            = format("%s%s", format("%s%s", substr(var.secrets_rg_name, 0, 7), substr(var.secrets_rg_name, 8, 7)), "storage")
  location                        = var.rg_location
  resource_group_name             = var.secrets_rg_name
  account_tier                    = "Standard"
  account_replication_type        = "LRS"
  allow_nested_items_to_be_public = true

  tags = var.common_tags
}
