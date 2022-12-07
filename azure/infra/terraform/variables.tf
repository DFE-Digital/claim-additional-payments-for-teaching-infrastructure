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

variable "db_name" {
  type        = string
  description = "Database name in the postgres server"
  default     = null
}

locals {
  tags = {
    "Environment"      = var.env_tag
    "Parent Business"  = "Teacher Training and Qualifications"
    "Portfolio"        = "Early Years and Schools Group"
    "Product"          = "Claim Additional Payments for teaching"
    "Service"          = "Teacher services"
    "Service Line"     = "Teaching Workforce"
    "Service Offering" = "Claim Additional Payments for teaching"
  }
  input_region = "westeurope"
}
