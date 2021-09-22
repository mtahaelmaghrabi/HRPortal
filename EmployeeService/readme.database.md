## Employee Service (Public API)
## Database setup

### Managing Database connections


### Managing the Environments (Development / Production)

1. Create two json files to hold the connection strings for both.
    - appsettings.Development.json
        - connection goes to the local SQL Server
    - appsettings.Production.json
        - connection goes to the installed SQL-Container on AKS Cluster
    - Add the configuration to read the Right connection according to the current context
        - ConfigureServices & CreateInitialDatabase methods.

2. Install [dotnet ef] to be used for Migration 
```
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialMigration --project EmployeeService
```
3. Test the local connection & databases

4. Build & Push the Docker Image then rollout the aks deployment
```
docker build -t mtahaelmaghrabi/employeeservice:latest .
docker image push mtahaelmaghrabi/employeeservice:latest
kubectl rollout restart deployment employee-deployment
```