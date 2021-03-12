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


# ---------------------------------------------------------------------------------------------------------------------
# LOCAL CALCULATED
# ---------------------------------------------------------------------------------------------------------------------

# locals {
#   calculated_local_value = uuid()
# }

locals {
  webtests = {
    as = {
      name          = format("%s-%s-%s-%s", var.app_rg_name, "ai-at", var.app_rg_name, "as-webtest")
      kind          = "ping"
      frequency     = 300
      timeout       = 120
      enabled       = true
      retry_enabled = true
      geo_locations = ["emea-nl-ams-azr", "emea-ru-msa-edge", "emea-se-sto-edge", "emea-gb-db3-azr", "emea-fr-pra-edge"]
      URL           = "development.additional-teaching-payment.education.gov.uk/healthcheck" #<- need to check for test and prod
    },
    vsp-as = {
      name          = format("%s-%s-%s-%s", var.app_rg_name, "ai-at", var.app_rg_name, "vsp-as-webtest")
      kind          = "ping"
      frequency     = 300
      timeout       = 120
      enabled       = true
      retry_enabled = true
      geo_locations = ["emea-nl-ams-azr", "emea-ru-msa-edge", "emea-se-sto-edge", "emea-gb-db3-azr", "emea-fr-pra-edge"]
      #URL           = format("%s%s", var.app_rg_name, "-vsp-as.azurewebsites.net/admin/healthcheck") 
      URL = "s118d01-app-vsp-as.azurewebsites.net/admin/healthcheck" #<- hardcoding for infradev ONLY
    }
  }

}
