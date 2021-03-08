# ---------------------------------------------------------------------------------------------------------------------
# REQUIRED PARAMETERS
# You must provide a value for each of these parameters.
# ---------------------------------------------------------------------------------------------------------------------
# variable "mandatory_variable" {
#   type        = string
#   description = "This is mandatory as there is no default declaration"
# }

variable "app_rg_name" {
  type        = string
  description = "Resource group for the application"
}
variable "projcore_network_prof" {
  type        = string
  description = "Project Core network profile"
}
variable "container_version" {
  type        = string
  description = "Specific version of the docker contaner"
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
  verify_entity_id   = "development.additional-teaching-payment.education.gov.uk"
  verify_environment = "INTEGRATION"
}
