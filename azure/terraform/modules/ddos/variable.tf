variable "rg_name" {
  type        = string
  description = "Resource group for the application"
}

variable "rg_location" {
  type        = string
  description = "The location of the resource group and the resources"
}

variable "common_tags" {
  type        = map(string)
  description = "Map of the mandatory standard DfE tags"
}
