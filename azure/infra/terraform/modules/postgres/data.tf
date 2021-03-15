data "azurerm_key_vault" "secrets_kv" {
  name                = "s118d01infradev-s-kv-01"
  resource_group_name = "s118d01-infradev-secrets"
}

data "azurerm_key_vault_secret" "postgres_pw" {
  name         = "DatabasePassword"
  key_vault_id = data.azurerm_key_vault.secrets_kv.id
}

data "azurerm_subscription" "current" {

}
