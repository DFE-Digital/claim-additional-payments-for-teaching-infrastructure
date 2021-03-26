#set common tags
module "env_vars" {
  source            = "./modules/env_vars"
  input_environment = var.input_environment
}

#resource group
module "resource_group" {
  source      = "./modules/resource_group"
  region      = var.input_region
  rg_prefix   = module.env_vars.rg_prefix
  common_tags = module.env_vars.common_tags
}

#storage account
module "storage_account" {
  source = "./modules/storageaccount"
  #  core_rg_name    = module.resource_group.core_rg_name
  secrets_rg_name = module.resource_group.secrets_rg_name
  #  secrets_tmp_rg_name = module.resource_group.secrets_tmp_rg_name
  func_rg_name = module.resource_group.func_rg_name
  rg_location  = module.resource_group.rg_location
  common_tags  = module.env_vars.common_tags
}

#networking
module "network" {
  source = "./modules/network"
  #  core_rg_name     = module.resource_group.core_rg_name
  projcore_rg_name = module.resource_group.projcore_rg_name
  rg_location      = module.resource_group.rg_location
  common_tags      = module.env_vars.common_tags
}

# subnet section
module "subnet" {
  source = "./modules/subnet"
  #  core_rg_name        = module.resource_group.core_rg_name
  projcore_rg_name = module.resource_group.projcore_rg_name
  #  core_vn_01_name     = module.network.core_vn_01_name
  projcore_vn_01_name = module.network.projcore_vn_01_name
  rg_location         = module.resource_group.rg_location
  common_tags         = module.env_vars.common_tags
}

#network profiles
module "network_profile" {
  source                = "./modules/network_profile"
  projcore_rg_name      = module.resource_group.projcore_rg_name
  projcore_worker_sn_id = module.subnet.projcore_sn_worker_id
  rg_location           = module.resource_group.rg_location
  common_tags           = module.env_vars.common_tags

  depends_on = [module.subnet]
}

# # # route table section
# # module "route_table" {
# #   source = "./modules/route_table"
# #   core_rg_name = module.resource_group.core_rg_name
# #   core_sn_id   = module.subnet.core_sn_id
# #   rg_location = module.resource_group.rg_location
# #   common_tags = module.env_vars.common_tags
# # }

# # # NSG
# # module "network_security_group" {
# #   source          = "./modules/nsg"
# #   core_rg_name    = module.resource_group.core_rg_name
# #   core_vn_01_name = module.network.core_vn_01_name
# #   rg_location     = module.resource_group.rg_location
# #   common_tags     = module.env_vars.common_tags

# #   depends_on = [module.subnet]
# # }

# # recover_services managed centrally in CORE
# module "recovery_services_vault" {
#   source           = "./modules/recovery_services_vault"
#   projcore_rg_name = module.resource_group.core_rg_name
#   rg_location      = module.resource_group.rg_location
#   common_tags      = module.env_vars.common_tags

# }

#key vault
module "key_vault" {
  source = "./modules/key_vault"
  #  core_rg_name           = module.resource_group.core_rg_name
  secrets_rg_name        = module.resource_group.secrets_rg_name
  projcore_default_sn_id = module.subnet.projcore_sn_default_id
  rg_location            = module.resource_group.rg_location
  common_tags            = module.env_vars.common_tags
}

# log analytics
module "log_analytics" {
  source           = "./modules/log_analytics"
  projcore_rg_name = module.resource_group.projcore_rg_name
  rg_location      = module.resource_group.rg_location
  common_tags      = module.env_vars.common_tags
}

#sentinal
module "sentinel" {
  source           = "./modules/sentinel"
  projcore_rg_name = module.resource_group.projcore_rg_name
  la_id            = module.log_analytics.la_id
  la_name          = module.log_analytics.la_name
  rg_location      = module.resource_group.rg_location
  common_tags      = module.env_vars.common_tags
}

# app insights
module "app_insights" {
  source      = "./modules/app_insights"
  app_rg_name = module.resource_group.app_rg_name
  rg_location = module.resource_group.rg_location
  common_tags = module.env_vars.common_tags
}

# app services - removed pending investigation 
module "app_services" {
  source       = "./modules/app_services"
  app_rg_name  = module.resource_group.app_rg_name
  func_rg_name = module.resource_group.func_rg_name
  rg_location  = module.resource_group.rg_location
  common_tags  = module.env_vars.common_tags

  depends_on = [module.app_insights]  
}

# Function app
module "function_app" {
  source       = "./modules/function_app"
  func_rg_name = module.resource_group.func_rg_name
  func_app_id  = module.app_services.func_app_service_plan_id
  func_sa_name = module.storage_account.func_sa_name
  func_sa_key  = module.storage_account.func_sa_key
  rg_location  = module.resource_group.rg_location
  common_tags  = module.env_vars.common_tags
}

# postgres
module "postgres" {
  source                = "./modules/postgres"
  app_rg_name           = module.resource_group.app_rg_name
  projcore_sn_worker_id = module.subnet.projcore_sn_worker_id
  rg_location           = module.resource_group.rg_location
  common_tags           = module.env_vars.common_tags

  depends_on = [module.subnet]
}

# #container
# module "container" {
#   source                = "./modules/container_reg"
#   app_rg_name           = module.resource_group.app_rg_name
#   projcore_network_prof = module.network_profile.projcore_network_prof
#   container_version     = module.env_vars.container_version
#   rg_location           = module.resource_group.rg_location
#   common_tags           = module.env_vars.common_tags

#   depends_on = [module.network_profile, module.app_insights]    
# }

# # below two modules are commented out as the two components are currently not required
# # # #ddos
# # # module "ddos" {
# # #   source      = "./modules/ddos"
# # #   rg_name     = module.resource_group.infra_rg_name
# # #   rg_location = module.resource_group.rg_location
# # #   common_tags = module.env_vars.common_tags
# # # }

# # # #security Centre
# # # module "security_centre" {
# # #   source = "./modules/security_centre"
# # #   la_id  = module.log_analytics.la_id
# # #   common_tags = module.env_vars.common_tags
# # # }
