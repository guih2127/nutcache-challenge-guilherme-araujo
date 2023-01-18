# Employees App

### What it is

This is a Web Application made for the Nutcache technical challenge. There are two projects here: One being the API made with .NET CORE, that provides the backend routes for the employees and its unit tests; and the other being the Single Page application made in ReactJS.

### How to run the application

You need yarn and .Net Core SDK installed on your machine to run the application. With this out of the way, you will need to run the API first. You can do this by entering the API folder (EmployeesAPI/EmployeesAPI) and starting the solution, using the Visual Studio. You can also do this using the .NET CLI, typing the following command:

```
dotnet run
```

You don't need to run migrations or something like that, because the project uses an In-Memory Database, so keep in mind that, everytime that you restart the API, the database will reset.

With this done, now you need to run the front-end. First, enter the folder of the front-end project (EmployeesSPA). After that, it is necessary to configure the .env file. The project comes with a file named .env.example, that you need to RENAME it to .env only. After that, on the variable VITE_API_URL, put the URL that your API is running, for example:

```
VITE_API_URL=https://localhost:7164
```

We are almost there. Now, you need to execute the last two commands to run the front-end application:

```
yarn install
yarn dev
```

You can also check a full verification of the API using Swagger, for that, you need to type in your browser, using you current back-end port:
```
https://localhost:7164/swagger/index.html
```
