# projcore Subnet 1
resource "azurerm_subnet" "projcore_subnet_default" {
  name                = "default" # this names are currently set so keeping with them
  resource_group_name = var.projcore_rg_name
  #address_prefixes    = ["192.168.1.16/28"]
  address_prefixes                               = ["11.0.1.16/28"]
  virtual_network_name                           = var.projcore_vn_01_name
  service_endpoints                              = ["Microsoft.Storage", "Microsoft.KeyVault"]
  enforce_private_link_endpoint_network_policies = false
  enforce_private_link_service_network_policies  = false
}

# projcore Subnet 2
resource "azurerm_subnet" "projcore_subnet_worker" {
  name                = "worker" # this names are currently set so keeping with them
  resource_group_name = var.projcore_rg_name
  #address_prefixes    = ["192.168.1.0/28"]
  address_prefixes                               = ["11.0.1.0/28"]
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
