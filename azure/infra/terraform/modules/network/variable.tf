# ---------------------------------------------------------------------------------------------------------------------
# REQUIRED PARAMETERS
# You must provide a value for each of these parameters.
# ---------------------------------------------------------------------------------------------------------------------
# variable "mandatory_variable" {
#   type        = string
#   description = "This is mandatory as there is no default declaration"
# }

# variable "core_rg_name" {
#   type        = string
#   description = "Core resource group for the network profile"
# }
variable "projcore_rg_name" {
  type        = string
  description = "Project Core resource group for the network profile"
}
variable "rg_prefix" {
  type        = string
  description = "The prefix to be used for all resources"
}
variable "rg_location" {
  type        = string
  description = "The location of the resource group and the resources"
}
variable "common_tags" {
  type        = map(string)
  description = "Map of the mandatory standard DfE tags"
}

# ---------------------------------------------------------------------------------------------------------------------
# LOCAL CALCULATED
# ---------------------------------------------------------------------------------------------------------------------
# locals {
#   calculated_local_value = uuid()
# }

locals {



  ip_address_space = var.rg_prefix == "s118d01" ? ["11.0.1.0/24"] : var.rg_prefix == "s118t01" ? ["11.0.2.0/24"] : var.rg_prefix == "s118p01" ? ["11.0.3.0/24"] : ["192.168.1.0/24"]
  ip_dns_servers   = var.rg_prefix == "s118d01" ? ["11.0.1.4", "11.0.1.5"] : var.rg_prefix == "s118t01" ? ["11.0.2.4", "11.0.2.5"] : var.rg_prefix == "s118p01" ? ["11.0.3.4", "11.0.3.5"] : ["192.168.1.4", "192.168.1.5"]

}
