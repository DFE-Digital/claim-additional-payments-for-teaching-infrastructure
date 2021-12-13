output "app_service_plan_id" {
  value       = azurerm_app_service_plan.app_asp.id
  description = "App Service plan ID"
}

# output "func_app_service_plan_id" {
#   value       = azurerm_app_service_plan.fapp_prem_id.id
#   description = "Function App Service plan ID"
# }
