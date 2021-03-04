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
variable "input_environment" {
  type        = string
  description = "Which environmnet is being built, Dev, Test, Prod or Infradev"
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
  state_container = {
    infradev = {
      container_name = "s118d01tfstate"
    },
    dev = {
      container_name = "s118d01devtfstate"
    },
    test = {
      container_name = "s118d01testtfstate"
    },
    prod_west = {
      container_name = "s118d01prodwesttfstate"
    },
    prod_north = {
      container_name = "s118d01prodnorthtfstate"
    }
  }
}
