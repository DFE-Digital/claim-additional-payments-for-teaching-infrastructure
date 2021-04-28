# ---------------------------------------------------------------------------------------------------------------------
# REQUIRED PARAMETERS
# You must provide a value for each of these parameters.
# ---------------------------------------------------------------------------------------------------------------------
# variable "mandatory_variable" {
#   type        = string
#   description = "This is mandatory as there is no default declaration"
# }

variable "region" {
  type        = string
  description = "The region the resources are to be deployed to"
}
variable "rg_prefix" {
  type        = string
  description = "The prefix to be used for all resources"
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

variable "res_groups" {
  type = map(any)
  default = {
    alerts     = "alerts",
    alertsplat = "alerts-platform",
    app        = "app",
    projcore   = "ProjectCore",
    secrets    = "secrets",
    funcapp    = "funcapp"
    contreg    = "contreg"
  }
}
