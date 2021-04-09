resource "azurerm_app_service_slot" "app_as_slot" {
  name                = "staging"
  app_service_name    = azurerm_app_service.app_as.name
  resource_group_name = var.app_rg_name
  location            = var.rg_location
  app_service_plan_id = azurerm_app_service_plan.app_asp.id
  https_only          = true

  # site_config {
  #   dotnet_framework_version = "v4.0"
  # }

  app_settings = {
    "ADMIN_ALLOWED_IPS"                              = data.azurerm_key_vault_secret.AdminAllowedIPs.value
    "APPINSIGHTS_INSTRUMENTATIONKEY"                 = data.azurerm_application_insights.app_ai.instrumentation_key
    "CANONICAL_HOSTNAME"                             = local.verify_entity_id
    "DFE_SIGN_IN_API_CLIENT_ID"                      = data.azurerm_key_vault_secret.DfeSignInApiClientId.value
    "DFE_SIGN_IN_API_ENDPOINT"                       = data.azurerm_key_vault_secret.DfeSignInApiEndpoint.value
    "DFE_SIGN_IN_API_SECRET"                         = data.azurerm_key_vault_secret.DfeSignInApiSecret.value
    "DFE_SIGN_IN_IDENTIFIER"                         = data.azurerm_key_vault_secret.DfeSignInIdentifier.value
    "DFE_SIGN_IN_ISSUER"                             = data.azurerm_key_vault_secret.DfeSignInIssuer.value
    "DFE_SIGN_IN_REDIRECT_BASE_URL"                  = data.azurerm_key_vault_secret.DfeSignInRedirectBaseUrl.value
    "DFE_SIGN_IN_SECRET"                             = data.azurerm_key_vault_secret.DfeSignInSecret.value
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_HOST"     = format("%s.%s", format("%s-%s", var.app_rg_name, "db"), "postgres.database.azure.com")
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_NAME"     = local.environment
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_PASSWORD" = data.azurerm_key_vault_secret.DatabasePassword.value
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_USERNAME" = format("%s@%s", data.azurerm_key_vault_secret.DatabaseUsername.value, format("%s-%s", var.app_rg_name, "db")) # "tps_development@s118d01-app-db"
    "ENVIRONMENT_NAME"                               = local.environment
    "GECKOBOARD_API_KEY"                             = data.azurerm_key_vault_secret.GeckoboardAPIKey.value
    "GOOGLE_ANALYTICS_ID"                            = ""
    "LOGSTASH_HOST"                                  = data.azurerm_key_vault_secret.LogstashHost.value
    "LOGSTASH_PORT"                                  = "17000"
    "NOTIFY_API_KEY"                                 = data.azurerm_key_vault_secret.NotifyApiKey.value
    "RAILS_ENV"                                      = "production" #local.environment
    "RAILS_SERVE_STATIC_FILES"                       = "true"
    "ROLLBAR_ACCESS_TOKEN"                           = data.azurerm_key_vault_secret.RollbarInfraToken.value
    "SECRET_KEY_BASE"                                = data.azurerm_key_vault_secret.SecretKeyBase.value
    "WORKER_COUNT"                                   = "2"
    #    "GOVUK_VERIFY_VSP_HOST"                          = format("%s%s.%s", "https://", azurerm_app_service.app_vsp_as.name, "azurewebsites.net")    
  }

  tags = merge({
    },
    var.common_tags
  )

}


# resource "azurerm_app_service_slot" "app_vsp_as_slot" {
#   name                = "staging"
#   app_service_name    = azurerm_app_service.app_vsp_as.name
#   resource_group_name = var.app_rg_name
#   location            = var.rg_location
#   app_service_plan_id = azurerm_app_service_plan.app_vsp_asp.id
#   https_only          = true

#   # site_config {
#   #   dotnet_framework_version = "v4.0"
#   # }

#   app_settings = {
#     "EUROPEAN_IDENTITY_ENABLED"     = "true"
#     "SAML_PRIMARY_ENCRYPTION_KEY"   = data.azurerm_key_vault_secret.SamlEncryptionKey.value
#     "SAML_SECONDARY_ENCRYPTION_KEY" = ""
#     "SAML_SIGNING_KEY"              = data.azurerm_key_vault_secret.SamlSigningKey.value
#     "SERVICE_ENTITY_IDS"            = format("%s%s%s", "['https://", local.verify_entity_id, "']")
#     "VERIFY_ENVIRONMENT"            = local.verify_environment
#   }
#   # connection_string {
#   #   name  = "Database"
#   #   type  = "SQLServer"
#   #   value = "Server=some-server.mydomain.com;Integrated Security=SSPI"
#   # }

#   tags = merge({
#     },
#     var.common_tags
#   )

# }
