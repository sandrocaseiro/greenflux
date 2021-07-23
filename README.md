# GreenFlux Smart Charging

Simple ASP.NET 5 API with SQLite database and Cucumber tests.

## Description

A test database is included in the repository (```greenflux.db```) and the scripts for creation/drop are included in the ```Scripts/``` folder of the API project.

## Tests
The test project is using xUnit runner and dynamic creates/destroys the database between each scenario execution. Currently, the test data should be included in the ```testdata.sql``` file, inside the ```Scripts/``` folder of the API project.

Run all tests via **Test Explorer** in Visual Studio.


### Remarks
- Missing tests for some operations, but the testing logic should be clear.
- Tests doesn't support parallel execution.
- Must implement a UnitOfWork to use transactions for db operations.
- Add active column to perform logical delete only.
- Share common model validations and use validations at Service level.
- Can improve Swagger data.
- Can improve shared tests steps, to be more generic.
- Better database data validations.
