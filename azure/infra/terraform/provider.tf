provider "azurerm" {
  features {}

}

provider "azuread" {
}

provider "random" {
}

terraform {
  required_version = "= 0.14.9"
  backend "azurerm" {
    key = "terraform.tfstate"
  }

  required_providers {
    azuread = {
      source  = "hashicorp/azuread"
      version = "=1.4.0"
    }
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=2.49.0"
    }
    random = {
      source  = "hashicorp/random"
      version = "=3.1.0"
    }
  }
}
