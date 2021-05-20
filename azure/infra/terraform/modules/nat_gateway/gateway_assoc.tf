resource "azurerm_subnet_nat_gateway_association" "sub_nat_assoc_01" {
  subnet_id      = data.azurerm_subnet.subnet_01.id
  nat_gateway_id = azurerm_nat_gateway.nat_gateway_01.id
}
