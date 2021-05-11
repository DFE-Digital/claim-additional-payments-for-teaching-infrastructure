resource "azurerm_network_profile" "projcore_net_prof" {
  name                = format("%s-%s", var.projcore_rg_name, "worker-np")
  location            = var.rg_location
  resource_group_name = var.projcore_rg_name

  # container_network_interface {
  #   name = "projcorecnic"

  #   ip_configuration {
  #     name      = "projcorecontipconfig"
  #     subnet_id = var.projcore_worker_sn_id
  #   }
  # }
  container_network_interface {
    name = "eth0"

    ip_configuration {
      name      = "ipConfig1"
      subnet_id = "/subscriptions/8655985a-2f87-44d7-a541-0be9a8c2779d/resourceGroups/s118d01-ProjectCore/providers/Microsoft.Network/virtualNetworks/s118d01-ProjectCore-wkrvn/subnets/worker" # var.projcore_worker_sn_id
    }
  }

  tags = merge({
    },
    var.common_tags
  )

}
