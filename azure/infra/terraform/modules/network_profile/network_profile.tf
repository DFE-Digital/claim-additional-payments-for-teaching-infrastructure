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
      subnet_id = var.projcore_worker_sn_id
    }
  }

  tags = merge({
    },
    var.common_tags
  )

}
