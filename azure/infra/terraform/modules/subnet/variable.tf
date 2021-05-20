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
#   description = "Core resource group for the key vault"
# }
variable "projcore_rg_name" {
  type        = string
  description = "Project Core resource group for the key vault"
}
# variable "core_vn_01_name" {
#   type        = string
#   description = "Core network for the key vault"
# }
variable "projcore_vn_01_name" {
  type        = string
  description = "Project Core network for the key vault"
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


locals {

  default_ip = var.rg_prefix == "s118d01" ? ["11.0.1.16/28"] : var.rg_prefix == "s118t01" ? ["11.0.2.16/28"] : var.rg_prefix == "s118p01" ? ["11.0.3.16/28"] : ["192.168.1.16/28"]
  worker_ip  = var.rg_prefix == "s118d01" ? ["11.0.1.0/28"] : var.rg_prefix == "s118t01" ? ["11.0.2.0/28"] : var.rg_prefix == "s118p01" ? ["11.0.3.0/28"] : ["192.168.1.0/28"]

}
