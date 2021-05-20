data "azurerm_client_config" "current" {
}

# data "azuread_application" "bkp_mngt_serv" {
#   display_name = "Backup Management Service"
# }
# data "azuread_application" "sec_aud_tool" {
#   display_name = "SecOps-Audit-Tool"
# }


# data "azuread_group" "tps_del_team" {
#   display_name = "s118-teacherpaymentservice-Delivery Team USR"
# }

# # data "azuread_application" "az_app_serv" {
# #   display_name = "Microsoft Azure App Service"
# # }

# data "azuread_application" "pipeline" {
#   display_name = "s118d.bsvc.cip.azdo"
# }

# above not working due to a privilege missing 
# https://registry.terraform.io/providers/hashicorp/azuread/latest/docs/data-sources/group 
# ticket raised with CIP team
