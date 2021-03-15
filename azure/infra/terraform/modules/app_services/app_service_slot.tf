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
    "APPINSIGHTS_INSTRUMENTATIONKEY"                 = "e5dc1015-ade5-4aa9-817d-6d0c995e8f8d"
    "CANONICAL_HOSTNAME"                             = "development.additional-teaching-payment.education.gov.uk"
    "DFE_SIGN_IN_API_CLIENT_ID"                      = "teacherpayments"
    "DFE_SIGN_IN_API_ENDPOINT"                       = "https://pp-api.signin.education.gov.uk"
    "DFE_SIGN_IN_API_SECRET"                         = "gigantism-tapir-amplitude-snoopiest"
    "DFE_SIGN_IN_IDENTIFIER"                         = "teacherpayments"
    "DFE_SIGN_IN_ISSUER"                             = "https://pp-oidc.signin.education.gov.uk:443"
    "DFE_SIGN_IN_REDIRECT_BASE_URL"                  = "https://development.additional-teaching-payment.education.gov.uk"
    "DFE_SIGN_IN_SECRET"                             = "polarized-understudy-adrenal-rehydrate"
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_HOST"     = "s118d01-app-db.postgres.database.azure.com"
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_NAME"     = "development"
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_PASSWORD" = "Xt7nGrvn.DiCUyJ"
    "DFE_TEACHERS_PAYMENT_SERVICE_DATABASE_USERNAME" = "tps_development@s118d01-app-db"
    "ENVIRONMENT_NAME"                               = "development"
    "GECKOBOARD_API_KEY"                             = "deee6d58372836d7a9528157188cbadd"
    "GOOGLE_ANALYTICS_ID"                            = ""
    "GOVUK_VERIFY_VSP_HOST"                          = "https://s118d01-app-vsp-as.azurewebsites.net"
    "LOGSTASH_HOST"                                  = "fcd29968-96d0-4621-b081-ff8df333799f-ls.logit.io"
    "LOGSTASH_PORT"                                  = "17000"
    "NOTIFY_API_KEY"                                 = "staging-3e6d260a-4e66-4263-8ee9-87adbcabdf02-6de6304e-2f13-4f10-b54d-83adbf7cdacb"
    "RAILS_ENV"                                      = "production"
    "RAILS_SERVE_STATIC_FILES"                       = "true"
    "ROLLBAR_ACCESS_TOKEN"                           = "a7c3e525e3004ad4abfc70886d8923f6"
    "SECRET_KEY_BASE"                                = "762bf695638fc59ca35d4b99458dcc4e06000872ebcca10c3d6e8d0cd767a2ce4f401d50e313242753170293f87680005a330e6da35019a24bb776e11f6269ab"
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
    "SAML_PRIMARY_ENCRYPTION_KEY"   = "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDCyd6CjRDfz0ZRbauslXJdcr+mmvv9cvAXqDblAzwkmbD4ER/S0K+4QWymv3UKUtIDEDN1vbQ++eAweDp3GMJ2wu7R+Kg914xjoHSUkuqkriRP79kA0iNWTu9qTIbJipp77KCTCpG0nZKl9uuyXdoYB8fIE6jBvcvr+7c7jEFu+hlOOfxhgXCawVQ1yrWDaJThR7oGBCA8S4hKUbXtBWddHX0P7crikQrZmmFoD5fp2AigafHVBcKJSDD7Qdc2Xtw9Ohz1H+AmYA+Ne3tCkfaMuf/t3wjvN5HIhC+IdJNyjWLTTJTYcPCTYV414q8PZhGtsKWu27RUWYNNyewk99WpAgMBAAECggEAPdw4QwOjwUAlV8TZiWSovk6r2LBNqK7w2sJ8NHyzg/gfQJjHn37Q31Y/uDEDbXTh50Ek5paJgJqAfUQhZSNC1s6PY7VxULp8hkM14t1JkWUQeSZ1pxIVUNqepAaLo8PVzddXI3tuzIpiQTzKm4m8snb+FmdP579r3JTDGzEuspxCxoGCH3Yho4tQuToaOT7suv7XO/8ryp/Pppx/NtD5/weGxODcDx8WdH3wJSzGLG/NYcR15v4GK0PSP1PWIJB+k3FcZDWI5qPxMc3fSDF9QYR1WSpKNJtyf+ikLuZ/2cn+4ypAz/APSIgLOBr+0sINZSNsAw+fKEqKfZr8zqKzRQKBgQDh/GUd5pX27upcHq1/DVeuNtaNnNKwyUy+hOEmjDUuzfVIw+HIW6ACPZk+WuXxN/aw2FdBpcMFGIgMqx9g6LbJ2GhavVTtGc2v3v9CGQcB1muZ9d6GMwKUOa0lzsohwr3h6ugiuUxWdg3wTy3U5AoYhlUkX6XlVz+u8fDEwwaqXwKBgQDcqMCN1FNPiRFzzAygdVanl6QBrsTtZDcuKKAHJtp4Gdhw9KjyR29PiVDeuTdWNpwiDFC7xyeEoq0oGX85IWfgW7GMB4dJJaD51A+Z08PvsZMCZ6QrhGGs0PzY2cXQjZsrxwh0TwdJ0z0rWzjFSXihYOlSg6ZNPKvIuJ8OS5MM9wKBgQDDji3SWh+bt9OGM+Xe9CTT1RCFKxgHc6q9ky9itCGxhvijJx7SyrcEOVnK483nl44aKpwXauIAHuJcVBirO7YxpNto6j37j0C22Dn7cLznki065dDSQIIS0nZLKU8xN9wpC6YlpkOw7ifRtDLs7wjtgBFdkId2lrG9KhA3tKyn8QKBgHYtHqsW6uNu2lz1DFInZZhuO1T3X9pv0Km08jfCVED8PpIIRi8zmOa0+Q2/jmZq6uXFSLu4pm5/nVMbjKpxPKvJD+aZ5ZeEQplqPEy7QHWv2bbxANiZlqIybyhIQYw3OsKQXvPDm3irXFhLC7WFasirGWqqvTciZjIixnSmfwPzAoGAQmhX0a9zPC77cyig8OOT18eX6Rr7ufj32bM2NhixtfPsDPEjofLt57w6rrYiUaKofaL97Wo5Ae4NmaoAhDquI4pooo37LEtFNixoVtYbYL8siv2LF3dASnqPd17RIe2EmmIU8IQW3rSVJ7aXpERqs9XSZJ4SrwlQQWHuR+/e3hs="
    "SAML_SECONDARY_ENCRYPTION_KEY" = ""
    "SAML_SIGNING_KEY"              = "MIIEvwIBADANBgkqhkiG9w0BAQEFAASCBKkwggSlAgEAAoIBAQCqY4VyKHjMr8eR95rlizkFiez+k8onJtfIF2wKieh7DSq7PpBLOk/NzdKqDDfj5IJI5lU9VP1bknqBjjTeT8lvbOQ/5a6HWzu/VI2gaCkxVO21/gqFTOHeEaurGlU0B3RDM40TpltnMSck3tVUdbOEI4Tbpb6kk4FzK7nMWj+aBFOFL7abcATdgv/47inXBi6oPz1wl/1ycApOTV+elswLrkuVvoSErelVZJKDc2JHPL1rpTGckShKGjO1dqQL3OJuKIrPrCsanZmmjcVqb91QmNNtsQ/ljF+v8bGYk5U03GceQk5fu/dGOrdynwK4u9QwEn7xufZPH6qw7hdSQ0JrAgMBAAECggEBAJ+cutVFtGOKBplmKWP+F1xo5+lS6Hvklx6JUlxM88rResYMxX6zES7pL/67k1+YH7PKkO7zPqMZhEj4ve3DMB+BEhAJSITjogSJYGJzMKNVu5hQceqNKsisJPQTpt0mXnLVR8Kwg88W7GRNmemUmKWf4EUh7oSc6RbKTzJvGEVdYNAucYD6pkGZu4VfYvE/C0V/zzj0Lehsmcb3d13etRwzVfcIMghS7f8ZWHEhANLhqiSnG0bWduwUVQra150NTUQr/oPo7cnFsutU4i8duY+Fk6fv06gPRAr8GbshKmaRlwXiKEbecxQTj8H/nn1uTpDitd04uMlu1fWS1M3MS4ECgYEA0txWHHTMpTGFCH8RkcsqpBc9p/Gt4UNDOUfGZ+R6lEzz1qtT6a20tTtgubNeO2rlIkBhIGjzEEvCnt7fdkBNW2f1ogNcsOL8q1/2zDgQauHbO4X/YxgT+Y6fWsl9Row2ZybPV+SSgsCV/a8dSjV9JIk/vG/OAQC+nZssWP2unbUCgYEAzt06NDT/rq66QlaWtGYvwUHDyF2bja/mBMjOoW/1147J/j8XGhH/tzIR5KUkn1axsFv3JAZf/OUkZ0XPre3VZ9xCxvjY+k5dferAjLIBmqztFXrtikavqIoRzWegqLJn9iC1sHSYbwpi27NL8YC5EKoFPN67wx/pOgj6E9x7c58CgYAfhJfKgRrlxlUgQ/4YUMn65Q5uQA3BkhBGS5g4h0pbSHVaLRnJ4BEW1d1LZKg9MWk2iXD7KzTJxsk8fgvg3ROzCgMJSH9eYU9rPljhha/Oihv+9bSK11qE/vCK3XtARE0NdhugA6ZyrN6+oKI8KWqfP3SrcfrU44uae0tRsfmb0QKBgQC/N7gpsiP2sQTzdV7xetQB7GyxtfWoT0Rsf8KhcADsNM16+467Ro/+I4ZuuEjWESlHQHt/DXWFK7suy1ViDUsLKot/qV/IYNADKRC7/X+GHvs4s90HyLdvL9Gs8XGM/v0igrHwXpbZbdxcAazdnIN1Z/RXg/xELidEwi+IVHK5OQKBgQC3EvqZ8ZDxAx9o8uEwnzcXOwcwXmIMRD1zQsePhc6ypO7oimw3SHgoyHV7cl2+CzVuyABM7f/NLF98T+iPz8MpXyY4wNHH9XSq5X9Ke0J+UqYnK2k9Q0wak1MnGkvrpSqo45I25H07tZ7UFYn/fNKk1iUI0KOevQxhKXwvY2cTGg=="
    "SERVICE_ENTITY_IDS"            = "['https://development.additional-teaching-payment.education.gov.uk']"
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
