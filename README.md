# Claim Additional Payments for Teaching

## Documentation

Most documentation for the service can be found on the
[project's Confluence wiki](https://dfedigital.atlassian.net/wiki/spaces/TP/pages/2467004479/ECP+DQT+Integration+Useful+Links).
Some app-specific technical documentation can be found in the [docs](docs)
directory.

### First-line support developers

If you�re a developer on first-line support who is new to this project, see the
[support runbook (`docs/first-line-support-developer-runbook.md`)](docs/first-line-support-developer-runbook.md)
for help with common support tasks.

### Service architecture

### Documentation for common developer tasks

- Release process for production:
  [`docs/release-process.md`](docs/release-process.md)

## Prerequisites

Net Core 3.1
microsoft.net.sdk.function 3.0.11

## Project structure

```bash
 ├── claim-additional-payments-for-teaching-qts-api
    ├── azure

    ├── solution
       ├── claim-additional-payments-for-teaching-qts-api
            Azure Function app project containing three azure functions
              - DQTApi : HTTP Triggered function
              - DQTCsvProcessor: Blob Triggered function
              - DQTCsvToBlob: Timer Tiggered funcion
        ├── dqt.datalayer
            Data layer project to read and write data from PostgresSQL
        ├── dqt.domain
            Domain layer project implement business to filter the techer qualification records by TRN number
```

## Setting up the functions locally

TODO: local setup
