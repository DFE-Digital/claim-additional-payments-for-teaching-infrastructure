resource "azurerm_role_assignment" "terraform" {
  scope                = azurerm_key_vault.secrets_kv.id
  role_definition_name = "Key Vault Administrator"
  principal_id         = data.azurerm_client_config.current.object_id
}

