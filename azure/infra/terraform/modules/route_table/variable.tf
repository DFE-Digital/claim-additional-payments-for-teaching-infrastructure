# ---------------------------------------------------------------------------------------------------------------------
# REQUIRED PARAMETERS
# You must provide a value for each of these parameters.
# ---------------------------------------------------------------------------------------------------------------------
# variable "mandatory_variable" {
#   type        = string
#   description = "This is mandatory as there is no default declaration"
# }

variable "projcore_rg_name" {
  type        = string
  description = "ProjCore resource group for the key vault"
}
variable "core_sn_id" {
  type        = string
  description = "Core subnet ID"
} # <- removed while vnet deployment not in place
variable "rg_location" {
  type        = string
  description = "The location of the resource group and the resources"
}
variable "common_tags" {
  type        = map(string)
  description = "Map of the mandatory standard DfE tags"
}
