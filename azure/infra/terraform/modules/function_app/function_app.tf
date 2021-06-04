resource "azurerm_function_app" "dqt_func_app" {
  # name = format("%s%s", trim(local.small_name, "-"), "fapp")
  name                       = format("%s%s", format("%s%s", substr(var.func_rg_name, 0, 7), substr(var.func_rg_name, 8, 7)), "fapp")
  resource_group_name        = var.func_rg_name
  location                   = var.rg_location
  app_service_plan_id        = var.func_app_id
  storage_account_name       = var.func_sa_name
  storage_account_access_key = var.func_sa_key
  enable_builtin_logging     = false 
  version                    = "~3"
  https_only                 = true


  tags = merge({
    },
    var.common_tags
  )

}
