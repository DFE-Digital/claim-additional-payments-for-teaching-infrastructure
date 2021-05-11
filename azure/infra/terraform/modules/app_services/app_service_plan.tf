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

# resource "azurerm_app_service_plan" "app_vsp_asp" {
#   name                = format("%s-%s", var.app_rg_name, "vsp-asp")
#   resource_group_name = var.app_rg_name
#   location            = var.rg_location
#   is_xenon            = false
#   kind                = "linux"
#   per_site_scaling    = false
#   reserved            = true

#   sku {
#     tier = "PremiumV2"
#     size = "P1v2"
#   }

#   tags = merge({
#     },
#     var.common_tags
#   )

# }

resource "azurerm_app_service_plan" "fapp_consump_sp" {
  name                = format("%s-%s", var.func_rg_name, "fapp-consump-sp")
  resource_group_name = var.func_rg_name
  location            = var.rg_location
  kind                = "FunctionApp"

  sku {
    tier = "Dynamic"
    size = "Y1"
  }

  tags = merge({
    },
    var.common_tags
  )

}

resource "azurerm_app_service_plan" "fapp_prem_id" {
  name                = format("%s-%s", var.func_rg_name, "premium-asp")
  resource_group_name = var.func_rg_name
  location            = var.rg_location
  kind                = "app"

  sku {
    tier = "Premium"
    size = "P1"
  }

  tags = merge({
    },
    var.common_tags
  )

}