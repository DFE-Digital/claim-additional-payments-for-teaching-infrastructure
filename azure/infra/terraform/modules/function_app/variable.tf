# ---------------------------------------------------------------------------------------------------------------------
# REQUIRED PARAMETERS
# You must provide a value for each of these parameters.
# ---------------------------------------------------------------------------------------------------------------------
# variable "mandatory_variable" {
#   type        = string
#   description = "This is mandatory as there is no default declaration"
# }

variable "func_rg_name" {
  type        = string
  description = "Resource group for the application"
}
variable "func_app_id" {
  type        = string
  description = "Function app service plan ID"
}
variable "func_sa_name" {
  type        = string
  description = "Function app storage account name"
}
variable "func_sa_key" {
  type        = string
  description = "Function app storage account key"
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
  small_name = format("%s%s", substr(var.func_rg_name, 0, 7), substr(var.func_rg_name, 8, 8)) # temp fix due to -infradev being in RG name during dev
}
