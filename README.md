## Initial Setup

This is a project powered by .NET 7, to run correctly the App you need to do the following steps:

1. You need to check if you have .NET 7 on your machine. To do that you can execute **dotnet help**. Install if you don´t have it.
3. Open a git bash (or terminal) into a custom folder and clone the repository inside them.
4. after that, go inside the project root folder (called PsicoAppAPI) and run **dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 7.0.5**
5. In the same folder, then execute **dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.5**
6. Finally, you can start the project executing the command **dotnet run** in the project root folder. The default port is 5000 and you can test the API behaviour on **http://localhost:5000/swagger**.
