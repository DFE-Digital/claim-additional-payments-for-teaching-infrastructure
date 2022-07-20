variable "secrets_rg_name" {
  type        = string
  description = "Secrets resource group for the key vault"
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
variable "azdo_sp" {
  type        = string
  description = "Azure Devops service principal"
}

locals {
  small_name = format("%s%s", substr(var.secrets_rg_name, 0, 7), substr(var.secrets_rg_name, 8, 8)) # temp fix due to -infradev being in RG name during dev
}
