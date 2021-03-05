resource "azurerm_storage_account" "core_sa_01" {
  #name = format("%s%s", local.small_name, "diags")
  name                     = format("%s%s", format("%s%s", substr(var.core_rg_name, 0, 7), substr(var.core_rg_name, 8, 4)), "diags")
  location                 = var.rg_location
  resource_group_name      = var.core_rg_name
  account_tier             = "Standard"
  account_replication_type = "GRS"

  tags = merge({
    },
    var.common_tags
  )
}

resource "azurerm_storage_account" "core_secret_01" {
  #name = format("%s%s", trim(local.small_name, "-"), "storage")
  name                     = format("%s%s", format("%s%s", substr(var.secrets_rg_name, 0, 7), substr(var.secrets_rg_name, 8, 7)), "storage")
  location                 = var.rg_location
  resource_group_name      = var.secrets_rg_name
  account_tier             = "Standard"
  account_replication_type = "GRS"

  tags = merge({
    },
    var.common_tags
  )
}

resource "azurerm_storage_account" "funcappsa" {
  #name = format("%s%s", trim(local.small_name, "-"), "fapp")
  name                     = format("%s%s", format("%s%s", substr(var.func_rg_name, 0, 7), substr(var.func_rg_name, 8, 7)), "fapp")
  resource_group_name      = var.func_rg_name
  location                 = var.rg_location
  account_tier             = "Standard"
  account_replication_type = "LRS"

  tags = merge({
    },
    var.common_tags
  )

}
