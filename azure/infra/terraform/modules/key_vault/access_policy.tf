# access policies set after deployment
resource "azurerm_key_vault_access_policy" "secrets_kv_access" {
  key_vault_id = azurerm_key_vault.secrets_kv.id

  tenant_id = data.azurerm_client_config.current.tenant_id
  object_id = "6642920a-1aab-49bb-9a20-365131195349"

  key_permissions = [
    "Get",
    "List",
    "Update",
    "Create",
    "Import",
    "Delete",
    "Recover",
    "Backup",
    "Restore",
    "Decrypt",
    "Encrypt",
    "UnwrapKey",
    "WrapKey",
    "Verify",
    "Sign",
  ]

  secret_permissions = [
    "Get",
    "List",
    "Set",
    "Delete",
    "Recover",
    "Backup",
    "Restore"
  ]

  certificate_permissions = [
    "Get",
    "List",
    "Update",
    "Create",
    "Import",
    "Delete",
    "Recover",
    "Backup",
    "Restore",
    "ManageContacts",
    "ManageIssuers",
    "GetIssuers",
    "ListIssuers",
    "SetIssuers",
    "DeleteIssuers",
  ]

}

# access policies set after deployment
resource "azurerm_key_vault_access_policy" "secret_kv_access_aas" {
  key_vault_id = azurerm_key_vault.secrets_kv.id

  tenant_id = data.azurerm_client_config.current.tenant_id
  # Can be found by Azure Portal > Enterprise Applications > s118*.bsvc.cip.azdo > Object ID
  object_id = var.azdo_sp

  secret_permissions = [
    "Get"
  ]

  key_permissions = [
  ]

  certificate_permissions = [
  ]
}
