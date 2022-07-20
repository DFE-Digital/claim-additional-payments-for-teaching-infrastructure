# ---------------------------------------------------------------------------------------------------------------------
# REQUIRED PARAMETERS
# You must provide a value for each of these parameters.
# ---------------------------------------------------------------------------------------------------------------------
# variable "mandatory_variable" {
#   type        = string
#   description = "This is mandatory as there is no default declaration"
# }

variable "input_region" {
  type        = string
  description = "Location for all of the Azure resources "
}
variable "rg_prefix" {
  type        = string
  description = "Resource group prefix"
}
variable "env_tag" {
  type        = string
  description = "CIP 'Environment' tag value"
}

variable "azdo_sp" {
  type        = string
  description = "Azure Devops service principal"
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

# ---------------------------------------------------------------------------------------------------------------------
# LOCAL CALCULATED
# ---------------------------------------------------------------------------------------------------------------------
# locals {
#   calculated_local_value = uuid()
# }

locals {
  tags = {
    "Environment"      = var.env_tag
    "Parent Business"  = "Teacher Training and Qualifications"
    "Portfolio"        = "Early Years and Schools Group"
    "Product"          = "Claim Additional Payments for teaching"
    "Service"          = "Teacher services"
    "Service Line"     = "Teaching Workforce"
    "Service Offering" = "Claim Additional Payments (for teaching)"
  }
}
