data "azurerm_key_vault" "secrets_kv" {
  name                = "s118d01-secrets-kv"
  resource_group_name = "s118d01-secrets"
}

data "azurerm_key_vault_secret" "postgres_pw" {
  name         = "DatabasePassword"
  key_vault_id = data.azurerm_key_vault.secrets_kv.id
}

data "azurerm_subscription" "current" {

}
