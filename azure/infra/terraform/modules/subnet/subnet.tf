# projcore Subnet 1
resource "azurerm_subnet" "projcore_subnet_default" {
  name                                           = "default" # this names are currently set so keeping with them
  resource_group_name                            = var.projcore_rg_name
  address_prefixes                               = local.default_ip
  virtual_network_name                           = var.projcore_vn_01_name
  service_endpoints                              = ["Microsoft.Storage", "Microsoft.KeyVault", "Microsoft.ContainerRegistry"]
  enforce_private_link_endpoint_network_policies = false
  enforce_private_link_service_network_policies  = false
  delegation {
    name = "Microsoft.Web.serverFarms"

    service_delegation {
      actions = [
        "Microsoft.Network/virtualNetworks/subnets/action",
      ]
      name = "Microsoft.Web/serverFarms"
    }
  }

}

# projcore Subnet 2
resource "azurerm_subnet" "projcore_subnet_worker" {
  name                                           = "worker" # this names are currently set so keeping with them
  resource_group_name                            = var.projcore_rg_name
  address_prefixes                               = local.worker_ip
  virtual_network_name                           = var.projcore_vn_01_name
  service_endpoints                              = ["Microsoft.Sql"]
  enforce_private_link_endpoint_network_policies = false
  enforce_private_link_service_network_policies  = false

  delegation {
    name = "DelegationService" #"Microsoft.ContainerInstance/containerGroups"
    service_delegation {
      actions = [
        "Microsoft.Network/virtualNetworks/subnets/action",
      ]
      name = "Microsoft.ContainerInstance/containerGroups"
    }
  }
}
