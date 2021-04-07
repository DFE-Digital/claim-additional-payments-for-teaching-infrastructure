data "azurerm_key_vault" "secrets_kv" {
  name                = format("%s-%s", var.rg_prefix, "secrets-kv")
  resource_group_name = format("%s-%s", var.rg_prefix, "secrets")
}

data "azurerm_key_vault_secret" "postgres_user" {
  name         = "DatabaseUsername"
  key_vault_id = data.azurerm_key_vault.secrets_kv.id
}

data "azurerm_key_vault_secret" "postgres_pw" {
  name         = "DatabasePassword"
  key_vault_id = data.azurerm_key_vault.secrets_kv.id
}

data "azurerm_subscription" "current" {

}
