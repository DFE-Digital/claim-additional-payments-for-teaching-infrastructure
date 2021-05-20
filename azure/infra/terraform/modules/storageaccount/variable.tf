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
#   description = "Core resource group for the storage accounts"
# }
variable "secrets_rg_name" {
  type        = string
  description = "Secrets resource group for the storage accounts"
}
variable "func_rg_name" {
  type        = string
  description = "Function app resource group for the storage accounts"
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
  small_name = format("%s%s", substr(var.secrets_rg_name, 0, 7), substr(var.secrets_rg_name, 8, 8)) # temp fix due to -infradev being in RG name during dev
}
