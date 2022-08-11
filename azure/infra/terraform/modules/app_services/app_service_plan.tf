resource "azurerm_app_service_plan" "app_asp" {
  name                = format("%s-%s", var.app_rg_name, "asp")
  resource_group_name = var.app_rg_name
  location            = var.rg_location
  is_xenon            = false
  kind                = "linux"
  per_site_scaling    = false
  reserved            = true

  sku {
    tier = "PremiumV2"
    size = "P1v2"
  }

  tags = merge({
    },
    var.common_tags
  )

}

# resource "azurerm_app_service_plan" "fapp_consump_sp" {
#   name                = format("%s-%s", var.func_rg_name, "fapp-consump-sp")
#   resource_group_name = var.func_rg_name
#   location            = var.rg_location
#   kind                = "FunctionApp"

#   sku {
#     tier = "Dynamic"
#     size = "Y1"
#   }

#   tags = merge({
#     },
#     var.common_tags
#   )

# }

# resource "azurerm_app_service_plan" "fapp_prem_id" {
#   name                = format("%s-%s", var.func_rg_name, "premium-asp")
#   resource_group_name = var.func_rg_name
#   location            = var.rg_location
#   kind                = "app"

#   sku {
#     tier = "Premium"
#     size = "P1"
#   }

#   tags = merge({
#     },
#     var.common_tags
#   )

# }



// Now Read the Certificate###dev
data "azurerm_key_vault_secret" "webapp_certificate" {
  name         = "development-additional-teaching-payment-education-gov-uk"######need to parametise
  key_vault_id =  "/subscriptions/8655985a-2f87-44d7-a541-0be9a8c2779d/resourceGroups/s118d01-secrets/providers/Microsoft.KeyVault/vaults/s118d01-secrets-kv"
}

// Now bind the webapp to the domain and look for certificate.
resource "azurerm_app_service_custom_hostname_binding" "website_app_hostname_bind" { //Website App
  depends_on = [
    azurerm_app_service_certificate.cert,
  ]
  hostname            = var.host_name
  app_service_name    = format("%s-%s", var.app_rg_name, "asp")
  resource_group_name = module.resource_group.app_rg_name
  ssl_state           = "SniEnabled"
  thumbprint          = azurerm_app_service_certificate.cert.thumbprint
}


// Get Certificate from External KeyVault
resource "azurerm_app_service_certificate" "cert" {
  name                = "sslCertificate-wildcard-additional-teaching-payment-education-gov-uk"
  resource_group_name = module.resource_group.app_rg_name
  location            = module.resource_group.rg_location
  pfx_blob            = data.azurerm_key_vault_secret.development-additional-teaching-payment-education-gov-uk.value###########need to parametise ###
}
