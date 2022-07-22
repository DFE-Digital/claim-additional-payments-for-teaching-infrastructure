review:
	$(eval AZ_SUBSCRIPTION=s118-teacherpaymentsservice-development)
	$(eval RESOURCE_GROUP_NAME=s118d02-tfbackend)
	$(eval STORAGE_ACCOUNT_NAME=s118d02tfbackendsa)
	$(eval CONTAINER_NAME=s118d02reviewtfstate)
	$(eval DEPLOY_ENV=review)

dev:
	$(eval AZ_SUBSCRIPTION=s118-teacherpaymentsservice-development)
	$(eval RESOURCE_GROUP_NAME=s118d01-tfbackend)
	$(eval STORAGE_ACCOUNT_NAME=s118d01tfbackendsa)
	$(eval CONTAINER_NAME=s118d01devtfstate)
	$(eval DEPLOY_ENV=development)

test:
	$(eval AZ_SUBSCRIPTION=s118-teacherpaymentsservice-test)
	$(eval RESOURCE_GROUP_NAME=s118t01-tfbackend)
	$(eval STORAGE_ACCOUNT_NAME=s118t01tfbackendsa)
	$(eval CONTAINER_NAME=s118t01testtfstate)
	$(eval DEPLOY_ENV=test)

production:
	$(eval AZ_SUBSCRIPTION=s118-teacherpaymentsservice-production)
	$(eval RESOURCE_GROUP_NAME=s118p01-tfbackend)
	$(eval STORAGE_ACCOUNT_NAME=s118p01tfbackendsa)
	$(eval CONTAINER_NAME=s118p01westprodtfstate)
	$(eval DEPLOY_ENV=production)

set-azure-account:
	az account set -s ${AZ_SUBSCRIPTION}

terraform-init: set-azure-account
	terraform -chdir=azure/infra/terraform init -reconfigure \
		-backend-config=resource_group_name=${RESOURCE_GROUP_NAME} \
		-backend-config=storage_account_name=${STORAGE_ACCOUNT_NAME} \
		-backend-config=container_name=${CONTAINER_NAME} \
		${BACKEND_KEY}

terraform-plan: terraform-init
	terraform -chdir=azure/infra/terraform plan -var="input_region=westeurope" \
		-var-file workspace_variables/${DEPLOY_ENV}.tfvars.json

terraform-apply: terraform-init
	terraform -chdir=azure/infra/terraform apply -var="input_region=westeurope" \
		-var-file workspace_variables/${DEPLOY_ENV}.tfvars.json
