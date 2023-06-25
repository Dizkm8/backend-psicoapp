## Initial Setup

This is a project powered by .NET 7, to run correctly the App you need to do the following steps:

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
```env
GPT_API_KEY=YOUR_API_KEY
TIMEZONE_API_KEY=YOUR_API_KEY
```
Where YOUR_API_KEY is the key that you can get from the following links:
<a href="https://platform.openai.com/account/api-keys">GPT_API_KEY</a>
<a href="https://developers.google.com/maps/documentation/timezone/get-api-key">TIMEZONE_API_KEY</a>

6. Finally, you can start the project executing the command
```cs
dotnet run
```
inside the PsicoAppAPI folder.