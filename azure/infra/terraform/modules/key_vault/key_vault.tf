##secrets keyvault
resource "azurerm_key_vault" "secrets_kv" {
  #name = format("%s-%s", local.small_name, "s-kv-01")
  name                            = format("%s-%s", var.secrets_rg_name, "kv")
  location                        = var.rg_location
  resource_group_name             = var.secrets_rg_name
  tenant_id                       = data.azurerm_client_config.current.tenant_id
  enabled_for_disk_encryption     = false
  enabled_for_deployment          = false
  enabled_for_template_deployment = true
  # enable_rbac_authorization       = true
  sku_name = "standard"

  network_acls {
    default_action = "Allow"
    bypass         = "AzureServices"
    #    ip_rules                   = ["79.70.26.28/32", ]
    ip_rules = ["13.69.20.148/32",
      "13.69.253.198/32",
      "13.79.234.50/32",
      "13.94.96.87/32",
      "137.116.242.45/32",
      "137.117.215.183/32",
      "137.117.228.175/32",
      "20.73.51.134/32",
      "20.73.82.130/32",
      "23.97.209.240/32",
      "40.127.106.246/32",
      "40.68.123.65/32",
      "40.89.167.46/32",
      "40.89.177.224/32",
      "51.103.105.155/32",
      "51.103.113.22/32",
      "51.103.84.115/32",
      "51.11.247.40/32",
      "51.136.161.225/32",
      "51.144.135.27/32",
      "52.143.167.98/32",
      "52.169.29.110/32",
      "52.169.42.231/32",
      "65.52.68.33/32",
      "78.146.223.133/32",
      "81.98.192.53/32",
      "82.24.130.89/32",
    "94.10.62.74/32", ]
    virtual_network_subnet_ids = [var.projcore_default_sn_id]
  }

  lifecycle {
    ignore_changes = [
      network_acls["default_action"]
    ]
  }

  tags = merge({
    },
    var.common_tags
  )

}
