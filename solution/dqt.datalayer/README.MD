## Database Migration

# Puprpose
 - Contains the commands needs to create an EF Core code-first Migration using the Visual Studio package manager.

# Migration Steps 
 - Open the package manger console
 - Enter the below commands one at a time.
    - Add Migration Script
		- EntityFrameworkCore\Add-Migration 'MigrationNameHere'
	- Update the Databse
		- EntityFrameworkCore\Update-Database