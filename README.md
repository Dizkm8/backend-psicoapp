## Initial Setup

This is a project powered by .NET 7, to run correctly the App you need to do the following steps:

1. You need to check if you have .NET 7 on your machine. To do that you can execute **dotnet help**. Install if you don´t have it.
2. Open a git bash (or terminal) into a custom folder and clone the repository inside them.
3. after that, go inside the project folder called PsicoAppAPI and run **dotnet restore**.
4. Inside the PsicoAppAPI folder you need to create a new file and name as ".env".
5. The .env file will have 2 records: **GPT_API_KEY=YOUR_API_KEY** and in a new line **TIMEZONE_API_KEY=YOUR_API_KEY**. This Keys are obtained from OpenAI and TimeZoneApi (Google) respectively.
6. Finally, you can start the project executing the command **dotnet run** inside the PsicoAppAPI folder.
