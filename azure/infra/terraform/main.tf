module "resource_group" {
  source      = "./modules/resource_group"
  region      = local.input_region
  rg_prefix   = var.rg_prefix
  common_tags = local.tags
}

module "storage_account" {
  source          = "./modules/storageaccount"
  secrets_rg_name = module.resource_group.secrets_rg_name
  rg_prefix       = var.rg_prefix
  rg_location     = module.resource_group.rg_location
  common_tags     = local.tags
}

module "key_vault" {
  source                 = "./modules/key_vault"
  secrets_rg_name        = module.resource_group.secrets_rg_name
  rg_prefix              = var.rg_prefix
  rg_location            = module.resource_group.rg_location
  common_tags            = local.tags
  azdo_sp                = var.azdo_sp
}

module "log_analytics" {
  source           = "./modules/log_analytics"
  projcore_rg_name = module.resource_group.projcore_rg_name
  rg_prefix        = var.rg_prefix
  rg_location      = module.resource_group.rg_location
  common_tags      = local.tags
}

module "sentinel" {
  source           = "./modules/sentinel"
  projcore_rg_name = module.resource_group.projcore_rg_name
  la_id            = module.log_analytics.la_id
  la_name          = module.log_analytics.la_name
  rg_prefix        = var.rg_prefix
  rg_location      = module.resource_group.rg_location
  common_tags      = local.tags
}

module "app_insights" {
  source      = "./modules/app_insights"
  app_rg_name = module.resource_group.app_rg_name
  rg_prefix   = var.rg_prefix
  rg_location = module.resource_group.rg_location
  common_tags = local.tags
}

module "app_services" {
  source       = "./modules/app_services"
  app_rg_name  = module.resource_group.app_rg_name
  rg_prefix    = var.rg_prefix
  rg_location  = module.resource_group.rg_location
  common_tags  = local.tags

  depends_on = [module.app_insights]
}

module "postgres" {
  source                = "./modules/postgres"
  app_rg_name           = module.resource_group.app_rg_name
  rg_prefix             = var.rg_prefix
  rg_location           = local.input_region
  common_tags           = local.tags
  db_name               = var.db_name
}
