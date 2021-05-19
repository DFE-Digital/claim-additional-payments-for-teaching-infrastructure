# Claim Additional Payments for Teaching

#### Documentation

	Most documentation for the service can be found on the
	[project's Confluence wiki](https://dfedigital.atlassian.net/wiki/spaces/TP/pages/2467004479/ECP+DQT+Integration+Useful+Links).
	Some app-specific technical documentation can be found in the [docs](docs)
	directory.

#### First-line support developers
	TODO:

#### Service architecture
	https://dfedigital.atlassian.net/wiki/spaces/TP/pages/2552528960/DQT+Integration+-+solution

### Documentation for common developer tasks

	Release process:
	  There are 3 environements - Development, Test (Pre-Production) and Production. Deployment to this environements are automated but have to be triggered manually
	  The pipeline for this project can be found here https://dev.azure.com/dfe-ssp/S118-Teacher-Payments-Service/_build?definitionId=1003&_a=summary.
	  Steps to Trigger Deployment
	  1. Open the above link
	  2. Click on Run pipeline
	  3. Select Branch/tag. Usually Master for prodcution and a feature branch for development and test environment
	  4. Select Environment from the dropdown box. options are 
		none - will only build and run untits
		development - will deploy to development environment
		test - will deploy to pre-production environment
		produciton - will deploy to produciton environement
	     by default none will be selectes
			
	  5. Select RunSmokeTests. Options are
	       yes - to run smoke tests
	       no - to run moke tests
	     by default no is selected
	  6. Optionally select Enable system diagnostics
	  7. Leave other options with default values
	  8. Click Run- This will trigget the deployment to the selected environment.
	 


## Project structure

```bash
 ├── claim-additional-payments-for-teaching-qts-api
    ├── azure

    ├── solution
       ├── claim-additional-payments-for-teaching-qts-api
            Azure Function app project containing four azure functions
              - DQTApi : HTTP Triggered function
              - DQTCsvProcessor: Blob Triggered function
              - DQTCsvToBlob: Timer Tiggered funcion
              - QualifiedTeacherStatusService : HTPP Triggered function
        ├── dqt.datalayer
            Data layer project to read and write data from PostgresSQL
        ├── dqt.domain
            Domain layer project implement business to filter the techer qualification records by TRN number
        ├── dqt.integrationtests
        ├── dqt.unittests
```

#### Setting up the functions locally

### Prerequisites
  	 - Net Core 3.1 microsoft.net.sdk.function 3.0.11
     - Azure Datastudio 
       	https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio?view=sql-server-ver15
     - Azure StorageExplorer
      	https://azure.microsoft.com/en-gb/features/storage-explorer/
      	https://go.microsoft.com/fwlink/?linkid=717179&clcid=0x409
     - WinSCP
       	https://winscp.net/eng/index.php
     - VisualStudio(2019) or VisualStudio Code
     - Postman

  ### Steps to run Locally

      1. Setup Azure storage explorer
           - Start Azure storage emulator and then Azure storage explorer
           - Expand Locals & Attached 
           - Expand Storage Accounts
           - Expand Emulator and under Blob Container add container named 'dqt-cont'. This the container name specified in local.settings.json

      2. Setup WinSCP
           - Run WinSCP
           - Login to Dev SFTP Server.
             Dev or Local SFTP Credentials can be found in s118d01-secrets-kv. You will need LocalSFTPHostName, LocalSFTPUserName and LocalSFTPPassword and replace 		    SFTPHostName, SFTPUserName and SFTPPassword respectively
             Once loged in you will see the folder structure as below
                 \Upload
                      \dev
                      \test
                      \production
           dev folder is used for local run.

      3. Update the dqt.datalayer/local.settings.json file
         Set 
              1. AzureWebJobsStorage  value to "UseDevelopmentStorage=true". This will enable Azure function in local run to use local blob storage
                 or get the storage connection string from azure portal from keyvault s118d01-secrets-kv. If the azure storage connection string is used
                 don't need to run Local Azure storage explorer.
              2. "FUNCTIONS_WORKER_RUNTIME": "dotnet",
              3. Update the other config settings from the keyvualt s118d01-secrets-kv.
              4. Set SFTPScheduleTriggerTime to value "*/30 * * * * *". This will trigger 'dqt-csv-to-blob' every 30 seconds.

      4. Debug Application
         1. Open project in visual studio 
         2. Press F5 to start the appliacation in debug mode.
         3. This will open a console window showing azure functions that are running in this application
            Functions:
              dqt-file-transfer-status-api: [GET] http://localhost:7071/api/qualified-teachers/dqt-file-transfer-status
              qualified-teacher-status-api: [GET] http://localhost:7071/api/qualified-teachers/qualified-teaching-status
              dqt-csv-processor: blobTrigger
              dqt-csv-to-blob: timerTrigger
         4. Drop the file in SFTP server in /upload/dev folder.
         5. This file will be picked up by 'dqt-csv-to-blob' function and will be procceesed to save to postgresql.
         4. Using Postman send the request to 
            qualified-teacher-status-api: [GET] http://localhost:7071/api/qualified-teachers/qualified-teaching-status
            Swagger document for api can be found in confluence page below
            https://dfedigital.atlassian.net/wiki/spaces/TP/pages/2552528934/DQT+Integration+Components
 ### Postman Collection
 	Postman Collection can be found in
 	https://dfedigital.atlassian.net/wiki/spaces/TP/pages/2838790145/DQT+API+Postman+Scripts
