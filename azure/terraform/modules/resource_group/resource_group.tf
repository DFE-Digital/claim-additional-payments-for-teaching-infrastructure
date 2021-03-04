resource "azurerm_resource_group" "rg_creation" {
  for_each = var.res_groups

  name     = format("%s-%s", var.rg_prefix, each.value)
  location = var.region

  tags = merge({
    },
    var.common_tags
  )

}



