resource "azurerm_postgresql_database" "app_dev" {
  name                = local.db_name
  resource_group_name = var.app_rg_name
  server_name         = azurerm_postgresql_server.app_postgres.name
  charset             = "UTF8"
  collation           = "English_United States.1252"
}

resource "azurerm_postgresql_database" "dqt_dev" {
  name                = "dqt_poc"
  resource_group_name = var.app_rg_name
  server_name         = azurerm_postgresql_server.app_postgres.name
  charset             = "UTF8"
  collation           = "English_United States.1252"
}

resource "random_uuid" "create_table" {
  keepers = {
    # Generate a new uuid each time any of the variables or the template file changes
    content = templatefile(
      "${path.module}/templates/createtables.sql.tpl",
      {
        # this is where key = value variables would go for injection into the template
      }
    )
  }
}

resource "null_resource" "sql_command" {
  triggers = {
    template = random_uuid.create_table.id
  }
  provisioner "local-exec" {
    command     = ".'${path.module}/scripts/ExecSQLCommand.ps1' -subscriptionId \"${data.azurerm_subscription.current.subscription_id}\" -ResourceGroupName \"${var.app_rg_name}\" -Server \"${azurerm_postgresql_server.app_postgres.name}\" -SecretID \"${data.azurerm_key_vault_secret.postgres_pw.id}\" -Database \"${azurerm_postgresql_database.dqt_dev.name}\" -QueryString \"${random_uuid.create_table.keepers.content}\" "
    interpreter = ["pwsh", "-Command"]
  }
}

resource "azurerm_postgresql_database" "dqt" {
  name                = "dqt"
  resource_group_name = var.app_rg_name
  server_name         = azurerm_postgresql_server.app_postgres.name
  charset             = "UTF8"
  collation           = "English_United States.1252"
}

################

resource "azurerm_postgresql_database" "app_dev_11" {
  name                = local.db_name
  resource_group_name = var.app_rg_name
  server_name         = azurerm_postgresql_server.app_postgres_11.name
  charset             = "UTF8"
  collation           = "English_United States.1252"
}

resource "azurerm_postgresql_database" "dqt_dev_11" {
  name                = "dqt_poc"
  resource_group_name = var.app_rg_name
  server_name         = azurerm_postgresql_server.app_postgres_11.name
  charset             = "UTF8"
  collation           = "English_United States.1252"
}

resource "random_uuid" "create_table_11" {
  keepers = {
    # Generate a new uuid each time any of the variables or the template file changes
    content = templatefile(
      "${path.module}/templates/createtables.sql.tpl",
      {
        # this is where key = value variables would go for injection into the template
      }
    )
  }
}

resource "null_resource" "sql_command_11" {
  triggers = {
    template = random_uuid.create_table.id
  }
  provisioner "local-exec" {
    command     = ".'${path.module}/scripts/ExecSQLCommand.ps1' -subscriptionId \"${data.azurerm_subscription.current.subscription_id}\" -ResourceGroupName \"${var.app_rg_name}\" -Server \"${azurerm_postgresql_server.app_postgres_11.name}\" -SecretID \"${data.azurerm_key_vault_secret.postgres_pw.id}\" -Database \"${azurerm_postgresql_database.dqt_dev.name}\" -QueryString \"${random_uuid.create_table.keepers.content}\" "
    interpreter = ["pwsh", "-Command"]
  }
}

resource "azurerm_postgresql_database" "dqt_11" {
  name                = "dqt"
  resource_group_name = var.app_rg_name
  server_name         = azurerm_postgresql_server.app_postgres_11.name
  charset             = "UTF8"
  collation           = "English_United States.1252"
}
