resource "azurerm_app_service" "app_as" {
  name                = format("%s-%s", var.app_rg_name, "as")
  resource_group_name = var.app_rg_name
  location            = var.rg_location
  app_service_plan_id = azurerm_app_service_plan.app_asp.id

  client_affinity_enabled = true
  https_only              = true
  # this needs to be defined

  site_config {
    always_on = true
    default_documents = [
      "Default.htm",
      "Default.html",
      "Default.asp",
      "index.htm",
      "index.html",
      "iisstart.htm",
      "default.aspx",
      "index.php",
      "hostingstart.html",
    ]
    health_check_path         = "/healthcheck"
    scm_type                  = "None"
    use_32_bit_worker_process = true
  }

  app_settings = {
    "ADMIN_ALLOWED_IPS"                              = "::/0,0.0.0.0/0"
    "APPINSIGHTS_INSTRUMENTATIONKEY"                 = "e5dc1015-ade5-4aa9-817d-6d0c995e8f8d"
    "CANONICAL_HOSTNAME"                             = ""
    "DFE_SIGN_IN_API_CLIENT_ID"                      = "teacherpayments"
    "DFE_SIGN_IN_API_ENDPOINT"                       = ""
    "DFE_SIGN_IN_API_SECRET"                         = ""
    "DFE_SIGN_IN_IDENTIFIER"                         = "teacherpayments"
    "DFE_SIGN_IN_ISSUER"                             = ""
    "DFE_SIGN_IN_REDIRECT_BASE_URL"                  = ""
    "DFE_SIGN_IN_SECRET"                             = ""
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_HOST"     = ""
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_NAME"     = ""
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_PASSWORD" = ""
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_USERNAME" = ""
    "ENVIRONMENT_NAME"                               = "development"
    "GECKOBOARD_API_KEY"                             = ""
    "GOOGLE_ANALYTICS_ID"                            = ""
    "GOVUK_VERIFY_VSP_HOST"                          = ""
    "LOGSTASH_HOST"                                  = ""
    "LOGSTASH_PORT"                                  = "17000"
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

resource "azurerm_app_service" "app_vsp_as" {
  name                = format("%s-%s", var.app_rg_name, "vsp-as")
  resource_group_name = var.app_rg_name
  location            = var.rg_location
  app_service_plan_id = azurerm_app_service_plan.app_vsp_asp.id

  client_affinity_enabled = true
  https_only              = true
  # this needs to be defined

  site_config {
    always_on = true
    default_documents = [
      "Default.htm",
      "Default.html",
      "Default.asp",
      "index.htm",
      "index.html",
      "iisstart.htm",
      "default.aspx",
      "index.php",
      "hostingstart.html",
    ]
    #    health_check_path         = "/healthcheck"
    scm_type                  = "None"
    use_32_bit_worker_process = true
  }

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

