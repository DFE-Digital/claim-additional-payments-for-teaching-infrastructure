resource "azurerm_nat_gateway_public_ip_association" "dat_pip_assoc_01" {
  nat_gateway_id       = data.azurerm_nat_gateway.nat_gateway_01.id
  public_ip_address_id = azurerm_public_ip.pip_01.id
}
