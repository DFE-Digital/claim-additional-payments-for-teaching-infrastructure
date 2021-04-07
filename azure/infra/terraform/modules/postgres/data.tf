data "azurerm_key_vault" "secrets_kv" {
  name                = "s118t01-secrets-kv"
  resource_group_name = "s118t01-secrets"
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
