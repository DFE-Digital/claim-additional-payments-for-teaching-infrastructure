# Claim Additional Payments for Teaching

Contains the terraform code and pipelines required to build the base infrastructure for [Claim additional payments for teaching](https://github.com/DFE-Digital/claim-additional-payments-for-teaching). The build and deployment of the application are done from the main repository.

## Documentation

Most documentation for the service can be found on the [project's Confluence wiki](https://dfedigital.atlassian.net/wiki/spaces/TP/pages/2467004479/ECP+DQT+Integration+Useful+Links) and in the [main repository](https://github.com/DFE-Digital/claim-additional-payments-for-teaching).

## Operations

### Requirements
- [Azure CIP](https://technical-guidance.education.gov.uk/infrastructure/hosting/azure-cip/) account in the s118 subscriptions
- [PIM requests](https://technical-guidance.education.gov.uk/infrastructure/hosting/azure-cip/#privileged-identity-management-pim-requests) for test and production subscriptions
- [az cli](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
- [terraform](https://www.terraform.io/) (see version in .tool-versions)

### Deployment
Use Makefile commands to simplify manual operations. The commands are run for a single environment like "review", "development", or "production".
See the Makefile for the available environments.

Login to Azure via the az cli:

```
az login
```

Review what is about to be deployed:

```
make <environment> terraform-plan
```

Deploy:

```
make <environment> terraform-apply
```
