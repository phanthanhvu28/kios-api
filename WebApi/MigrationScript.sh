#!/bin/bash

### Required dotnet ef migrations tool
### run cd to source-path, run below cmd

# create migration
# dotnet ef migrations add MigrationName -s WebApi/WebApi.csproj -p Infrastructure/Infrastructure.csproj

# update migration database ### MigrationName, leave empty will update latest version 
# dotnet ef database update -s WebApi/WebApi.csproj -p Infrastructure/Infrastructure.csproj


# migration remove
# dotnet ef migrations remove -s WebApi/WebApi.csproj -p Infrastructure/Infrastructure.csproj

# database drop
# dotnet ef database drop -s WebApi/WebApi.csproj -p Infrastructure/Infrastructure.csproj
