# ---------------------------------------------------------------------------------------------------------------------
# REQUIRED PARAMETERS
# You must provide a value for each of these parameters.
# ---------------------------------------------------------------------------------------------------------------------
# variable "mandatory_variable" {
#   type        = string
#   description = "This is mandatory as there is no default declaration"
#}

variable "projcore_rg_name" {
  type        = string
  description = "Core resource group for the key vault"
}
variable "la_id" {
  type        = string
  description = "Log analytics workspace ID"
}
variable "la_name" {
  type        = string
  description = "Log analytics workspace Name"
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
# OPTIONAL PARAMETERS
# These parameters have reasonable defaults.
# ---------------------------------------------------------------------------------------------------------------------
# variable "optional_variable" {
#   type        = list(any)
#   description = "Having a default declaration makes a parameter optional"
#   default     = []
# }

variable "solutions" {
  type = list(object({
    publisher = string
    product   = string
  }))
  default = [
    {
      publisher = "Microsoft"
      product   = "OMSGallery/SecurityInsights"
    },
    {
      publisher = "Microsoft"
      product   = "OMSGallery/ContainerInsights"
    }
  ]
}
