
resource "random_uuid" "availability" {
  for_each = local.webtests
  keepers = {
    rg_name = var.app_rg_name
  }
}
