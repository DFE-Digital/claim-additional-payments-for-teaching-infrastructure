resource "azurerm_container_group" "cont_reg_01" {
  name                = format("%s-%s", var.app_rg_name, "worker-aci")
  location            = var.rg_location
  resource_group_name = var.app_rg_name
  ip_address_type     = "Private"
  # ip_address_type = "Public"
  #dns_name_label      = "s118d01-aci-label-01"
  os_type = "Linux"
  # network_profile_id = var.projcore_network_prof

  container {
    name   = format("%s-%s", var.app_rg_name, "worker-container")
    image  = format("%s%s", "dfedigital/teacher-payments-service:", var.container_version)
    cpu    = "1"
    memory = "1.5"

    environment_variables = {
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
      "LOGSTASH_PORT"                                  = "17000"
      "NOTIFY_API_KEY"                                 = ""
      "RAILS_ENV"                                      = "production"
      "RAILS_SERVE_STATIC_FILES"                       = "true"
      "ROLLBAR_ACCESS_TOKEN"                           = ""
      "SECRET_KEY_BASE"                                = ""
      "WORKER_COUNT"                                   = "2"
    }

    # ports {
    #   port     = 443
    #   protocol = "TCP"
    # }
  }

  tags = merge({
    },
    var.common_tags
  )
}
