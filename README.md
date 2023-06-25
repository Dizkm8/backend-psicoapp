## Initial Setup

This is a project powered by **.NET 7** and uses **mariaDB** to store data, to run correctly the App you need to do the following steps:

1. You need to check if you have .NET 7 on your machine. To do that you can execute
```cs
dotnet help
```
Install if you don´t have it.

2. Open a git bash (or terminal) into a custom folder and clone the repository inside them.

3. after that, go inside the project folder called PsicoAppAPI and run
```cs
dotnet restore
```

4. Inside the PsicoAppAPI folder you need to create a new file and name as ".env".
5. Inside the .env file copy this:
```bash
GPT_API_KEY=YOUR_API_KEY
TIMEZONE_API_KEY=YOUR_API_KEY
```
Where YOUR_API_KEY is the key that you can get from the following links:
<p><a href="https://platform.openai.com/account/api-keys">GPT_API_KEY</a></p>
<p><a href="https://developers.google.com/maps/documentation/timezone/get-api-key">TIMEZONE_API_KEY</a></p>
6. After that you need to setup the database, we recommend to use mariaDB in a docker container for simplicity. To achieve that you need to create a new container with the following command:
```bash
docker run --detach --name psicoapp-db -p 3306:3306 --env MARIADB_USER=root --env MARIADB_PASSWORD=my-secret --env MARIADB_ROOT_PASSWORD=my-secret --env MARIADB_DATABASE=psicoapp mariadb:latest
```
Where the **MARIADB_USER** and **MARIADB_PASSWORD** are the credentials to access the database and **MARIADB_ROOT_PASSWORD** is the password to access the root user.
The **psicoapp-db** is the name of the container, you can change it if you want.
The **3306** is the port that the container will use to connect to the database, we suggest to keep it as it is.
The **psicoapp** is the name of the database. We recommend to not change it.

7. After that you need to run the container, so execute the following command:
```bash
docker start psicoapp-db
```
8. In order to match the database credentials with the project you need to create a file called **appsettings.json** inside the PsicoAppAPI folder. Inside them paste this:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "JwtSettings": {
    "Secret": "60ds7fhuif490nfjiods0237yrpaw3278df8ggh98g"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=psicoapp;Uid=root;Pwd=my-secret;"
  }
}
```
You need to change the **Uid** and **Pwd** to match the credentials that you used to create the container. If you do not changed the credentials in the container, you can leave it as it is.

9.  Finally, you can start the project executing the command
```cs
dotnet run
```
inside the PsicoAppAPI folder.