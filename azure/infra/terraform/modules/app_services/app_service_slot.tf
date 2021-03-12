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
    "ADMIN_ALLOWED_IPS"                              = "::/0,0.0.0.0/0"
    "APPINSIGHTS_INSTRUMENTATIONKEY"                 = ""
    "CANONICAL_HOSTNAME"                             = ""
    "DFE_SIGN_IN_API_CLIENT_ID"                      = "teacherpayments"
    "DFE_SIGN_IN_API_ENDPOINT"                       = ""
    "DFE_SIGN_IN_API_SECRET"                         = ""
    "DFE_SIGN_IN_IDENTIFIER"                         = "teacherpayments"
    "DFE_SIGN_IN_ISSUER"                             = ""
    "DFE_SIGN_IN_REDIRECT_BASE_URL"                  = ""
    "DFE_SIGN_IN_SECRET"                             = ""
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_HOST"     = ""
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_NAME"     = "development"
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_PASSWORD" = ""
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_USERNAME" = ""
    "ENVIRONMENT_NAME"                               = "development"
    "GECKOBOARD_API_KEY"                             = ""
    "GOOGLE_ANALYTICS_ID"                            = ""
    "GOVUK_VERIFY_VSP_HOST"                          = ""
    "LOGSTASH_HOST"                                  = ""
    "LOGSTASH_PORT"                                  = ""
    "NOTIFY_API_KEY"                                 = ""
    "RAILS_ENV"                                      = "production"
    "RAILS_SERVE_STATIC_FILES"                       = "true"
    "ROLLBAR_ACCESS_TOKEN"                           = ""
    "SECRET_KEY_BASE"                                = ""
    "WORKER_COUNT"                                   = "2"
  }
  # connection_string {
  #   name  = "Database"
  #   type  = "SQLServer"
  #   value = "Server=some-server.mydomain.com;Integrated Security=SSPI"
  # }

  tags = merge({
    },
    var.common_tags
  )

}


resource "azurerm_app_service_slot" "app_vsp_as_slot" {
  name                = "staging"
  app_service_name    = azurerm_app_service.app_vsp_as.name
  resource_group_name = var.app_rg_name
  location            = var.rg_location
  app_service_plan_id = azurerm_app_service_plan.app_vsp_asp.id
  https_only          = true

  # site_config {
  #   dotnet_framework_version = "v4.0"
  # }

  app_settings = {
    "EUROPEAN_IDENTITY_ENABLED"     = "true"
    "SAML_PRIMARY_ENCRYPTION_KEY"   = ""
    "SAML_SECONDARY_ENCRYPTION_KEY" = ""
    "SAML_SIGNING_KEY"              = ""
    "SERVICE_ENTITY_IDS"            = ""
    "VERIFY_ENVIRONMENT"            = "INTEGRATION"
  }
  # connection_string {
  #   name  = "Database"
  #   type  = "SQLServer"
  #   value = "Server=some-server.mydomain.com;Integrated Security=SSPI"
  # }

  tags = merge({
    },
    var.common_tags
  )

}
