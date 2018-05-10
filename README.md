# MunicipalitiesTaxes

An application which manages taxes applied in different municipalities.
The taxes are scheduled in time. Application provides the user an ability to get taxes applied in
certain municipality at the given day.

- It has its own database, MSSQL. There we use ORM Entityframework(Code first) who helps to map a data to objects.
- Taxes have ability to be scheduled.
- Application have ability to import municipalities data from json file, who is located in web directory.
- Application have ability to insert new records for municipality taxes. For this achievment in the first page we use  Swagger to check and make requests to webapi.
- All solution has different projects - web, services, repository, contract and selfhost projects.
- User can ask for a specific municipality tax by entering municipality name and date.
- Errors are being handled with global exception handler who inherits ExceptionHandler. So no try catch blocks needed there.
- Application is deployed as a self-hosted windows service - is done, Project use Owin in a SelfHost project, where we deploy a host with a commandline.
- Update record functionality is exposed via API - if I understood correctly, I had created in web api controller who allows to change taxes record fields with HttpPut
- Dependancy injection wasn't neccesary but for my projects structure I had to use them
- In migrations configuration class I use some mocked data who automatically being added to database when we run migrations
