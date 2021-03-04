output "core_sn_id" {
  value       = azurerm_subnet.core_subnet_01.id
  description = "Subnet ID"
}

output "projcore_sn_default_id" {
  value       = azurerm_subnet.projcore_subnet_default.id
  description = "Subnet ID"
}

output "projcore_sn_worker_id" {
  value       = azurerm_subnet.projcore_subnet_worker.id
  description = "Subnet ID"
}
