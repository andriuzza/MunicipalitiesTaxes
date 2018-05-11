# MunicipalitiesTaxes

An application which manages taxes applied in different municipalities.
The taxes are scheduled in time. Application provides the user an ability to get taxes applied in
certain municipality at the given day.

- It has its own database, MSSQL. I'm using ORM Entityframework(Code first) who helps to map a data from db to objects.
- Taxes have ability to be scheduled.
- Application have ability to import municipalities data from json file, who is located in web directory.
- Application have ability to insert new records for municipality taxes. For this achievment in the first page we use  Swagger to check and make requests to webapi.
- Solution is split up to different projects - web, services, repository, contract and selfhost projects.
- User can ask for a specific municipality tax by entering municipality name and date.
- Errors are being handled with global exception handler who inherits ExceptionHandler. So no try catch blocks needed there.
- Application is deployed as a self-hosted windows service/ There I'm using Owin in a SelfHost project, where we deploy a host through a command line project.
- Update record functionality is exposed via API - if I understood correctly, I had created method in web api controller who allows to change taxes record fields with HttpPut by sending request to API.
- Dependancy injection wasn't neccessary but for my project structure I had to use them (Unity Container)
- In migrations configuration class I use some mocked data who automatically being added to a database when we run migrations

*** for the main task - to return tax size. I'm not only returning the size of it, but  all tax model information, like Id, taxType ir etc.
