variable "app_rg_name" {
  type        = string
  description = "Application resource group for the network profile"
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
variable "db_name" {
  type        = string
  description = "Database name in the postgres server"
}
variable "geo_redundant_backup_enabled" {
  type        = string
  description = "Turn Geo-redundant server backups on/off"
}
