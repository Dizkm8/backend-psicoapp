## Initial Setup

This is a project powered by **.NET 7** and uses **mariaDB** to store data, to run correctly the App you need to do the following steps:

1. You need to check if you have .NET 7 on your machine. To do that you can execute
```cs

dotnet help

```
<p>Install if you don´t have it.</p>

2. Open a git bash (or terminal) into a custom folder and clone the repository inside them.

3. After that, go inside the project folder called PsicoAppAPI and run
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
<p><a href="https://platform.openai.com/account/api-keys" target="_blank">GPT_API_KEY</a></p>
<p><a href="https://developers.google.com/maps/documentation/timezone/get-api-key" target="_blank">TIMEZONE_API_KEY</a></p>
6. After that you need to setup the database, we recommend to use mariaDB in a docker container for simplicity. First of all you need to install docker from the offical page <a href="https://www.docker.com/products/docker-desktop" target="_blank">Docker Desktop</a>
7. Then, you need to create a container with mariaDB image. To achieve that run the following command:
```bash

docker run --detach --name psicoapp-db -p 3306:3306 --env MARIADB_USER=root --env MARIADB_PASSWORD=my-secret
--env MARIADB_ROOT_PASSWORD=my-secret --env MARIADB_DATABASE=psicoapp mariadb:latest

```
<p>Where the <b>MARIADB_USER</b> and <b>MARIADB_PASSWORD</b> are the credentials to access the database and <b>MARIADB_ROOT_PASSWORD</b> is the password to access the root user.</p>
<p>The <b>psicoapp-db</b> is the name of the container, you can change it if you want.</p>
<p>The <b>3306</b> is the port that the container will use to connect to the database, we suggest to keep it as it is.</p>
<p>The <b>psicoapp</b> is the name of the database. We recommend to not change it.</p>

8. After that you need to run the container, so execute the following command:
```bash
docker start psicoapp-db
```
9. In order to match the database credentials with the project you need to create a file called **appsettings.json** inside the PsicoAppAPI folder. Inside them paste this:
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
<p>You need to change the <b>Uid</b> and <b>Pwd</b> to match the credentials that you used to create the container. If you do not changed the credentials in the container, you can leave it as it is.</p>
<p>If you also changed the database name in the docker creating command you need to match with the variable <b>Database</b></p>

10. The last step of database config is open the connect to database, we highly recommend use any IDE like **Datagrip** or **SQL Workbench**, anyways, if you want use CLI you need to follow the mariaDB docs: <a href="https://mariadb.com/kb/en/connecting-to-mariadb/" target="_blank">Connecting to MariaDB</a> The credentials and port are the same you used to create the container.

11. Because our API feed web and mobile clients the **launchSettings.json** need to be updated to your environment. You need to change the value **applicationUrl** to your own machine IP adress. 

12. Finally, you can start the project executing the command
```cs

dotnet run

```
inside the PsicoAppAPI folder.