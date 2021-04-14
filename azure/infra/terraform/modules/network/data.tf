data "azurerm_key_vault" "secrets_kv" {
  name                = format("%s-%s", var.rg_prefix, "secrets-kv")
  resource_group_name = format("%s-%s", var.rg_prefix, "secrets")
}

data "azurerm_key_vault_secret" "vnet_range" {
  name         = "vnet-ips"
  key_vault_id = data.azurerm_key_vault.secrets_kv.id
}

data "azurerm_key_vault_secret" "vnet_dns_range" {
  name         = "vnet-dns-ips"
  key_vault_id = data.azurerm_key_vault.secrets_kv.id
}
