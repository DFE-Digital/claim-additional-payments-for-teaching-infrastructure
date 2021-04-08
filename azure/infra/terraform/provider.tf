provider "azurerm" {
  #alias = "main"
  # Whilst version is optional, we /strongly recommend/ using it to pin the version of the Provider being used

  #source.com
  #dev
  subscription_id = "8655985a-2f87-44d7-a541-0be9a8c2779d"
  #test
  # subscription_id = "e9299169-9666-4f15-9da9-5332680145af"
  #prod west
  # subscription_id = "88bd392f-df19-458b-a100-22b4429060ed"
  #prod north
  # subscription_id = "8655985a-2f87-44d7-a541-0be9a8c2779d"  
  tenant_id = "9c7d9dd3-840c-4b3f-818e-552865082e16"

  features {}

}

provider "azuread" {
}

provider "random" {
}

terraform {
  backend "azurerm" {
    storage_account_name = "s118t01tfbackendsa"
    # container_name       = "s118d01tfstate"
    # dev
    container_name = "s118d01devtfstate"
    #test
    # container_name = "s118t01testtfstate"
    # prodwest
    # container_name       = "s118p01westprodtfstate"
    # prod north
    # container_name       = "s118p01northprodtfstate"
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
    # local = {
    #   source  = "hashicorp/local"
    #   version = "=2.1.0"
    # }
    # null = {
    #   source  = "hashicorp/null"
    #   version = "=3.1.0"
    # }
    random = {
      source  = "hashicorp/random"
      version = "=3.1.0"
    }
  }
}

