output "projcore_network_prof" {
  value       = module.network_profile.projcore_network_prof
  description = "Network Profile Id"
}

output "app_asp_id" {
  value       = module.app_services.app_service_plan_id
  description = "App Service Plan ID"
}