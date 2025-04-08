> [See in spanish/Ver en español](https://github.com/LuisMiSanVe/GeminiPostSQL_ChatBot/blob/main/README.es.md)

<img src="https://github.com/LuisMiSanVe/LuisMiSanVe/blob/main/Resources/GeminiPostSQL/GeminiPostSQLChatBot_banner.png" style="width: 100%; height: auto;" alt="GeminiPostSQL Banner">

# <img src="https://github.com/LuisMiSanVe/GeminiPostSQL_ChatBot/blob/main/ChatBotApp/wwwroot/favicon.png" width="40" alt="GeminiPostSQL Logo"> GeminiPostSQL ChatBot | AI-Assisted Blazor ChatBot for PostgreSQL
[![image](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://dotnet.microsoft.com/en-us/languages/csharp)
[![image](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/en-us/learn/dotnet/what-is-dotnet)
[![image](https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Blazor](https://img.shields.io/badge/blazor-%235C2D91.svg?style=for-the-badge&logo=blazor&logoColor=white)](https://dotnet.microsoft.com/es-es/apps/aspnet/web-apps/blazor)
[![image](https://img.shields.io/badge/json-5E5C5C?style=for-the-badge&logo=json&logoColor=white)](https://www.newtonsoft.com/json)
[![image](https://img.shields.io/badge/Google%20Gemini-8E75B2?style=for-the-badge&logo=googlegemini&logoColor=white)](https://aistudio.google.com/app/apikey)
[![image](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white)](https://visualstudio.microsoft.com/)

>[!NOTE]
> This is the Blazor ChatBot version meant for web client use. There is a [REST API](https://github.com/LuisMiSanVe/GeminiPostSQL_API/tree/main) version meant for servers with Swagger and a [WinForms](https://github.com/LuisMiSanVe/GeminiPostSQL/tree/main) version for Windows Desktop client use.

This ChatBot program uses Google's AI 'Gemini 2.0 Flash' to make queries to PostgreSQL databases.  
The AI interprets natural language into SQL queries, run them, and transform the results into natural language again.

## 📋 Prerequisites
To make this program work, you'll need a PostgreSQL Server and a Gemini API Key.

> [!NOTE]  
> I'll use pgAdmin to build the PostgreSQL Server.

## 🛠️ Setup
If you don't have it, download [pgAdmin 4 from the official website](https://www.pgadmin.org/download/).  
Now, create the PostgreSQL Server and set up a database with a few tables and insert values.

Next, obtain your Gemini API Key by visiting [Google AI Studio](https://aistudio.google.com/app/apikey). Ensure you're logged into your Google account, then press the blue button that says 'Create API key' and follow the steps to set up your Google Cloud Project and retrieve your API key. **Make sure to save it in a safe place**.  
Google allows free use of this API without adding billing information, but there are some limitations.

In Google AI Studio, you can monitor the AI's usage by clicking 'View usage data' in the 'Plan' column where your projects are displayed. I recommend monitoring the 'Quota and system limits' tab and sorting by 'actual usage percentage,' as it provides the most detailed information.

You now have everything needed to make the program work.  
Simply put that data you just got into the code's parameters strings.

## 📖 About the Blazor ChatBot
The natural-sql-natural process methods works like this:

This [method](https://gist.github.com/LuisMiSanVe/2da8e2d932a06ef408b3ee8468d0d820) maps the database structure into a JSON that Gemini analyzes to create an SQL query to fulfill the user's request, which is then run by the PostgreSQL Server, returning the requested data.

Then, this data is processed by Gemini again to summary the results into natural language.

Because the AI needs to process **two requests** ([text to query and result table to text](https://gist.github.com/LuisMiSanVe/b189c8920d2dcedf5fd46485f3403d51)) the token usage is pretty high, thats why the **remembering capability** of the ChatBot is limited to the 5 prior messages, as is not meant to keep a single chat for too long.

The chats are not stored so the ChatBot consists in a volatile single-session chat. Press `F5` to clear the current chat.

## 💻 Technologies Used
- Programming Language: [C#](https://dotnet.microsoft.com/en-us/languages/csharp)
- Framework: [Blazor](https://dotnet.microsoft.com/es-es/apps/aspnet/web-apps/blazor), [.Net](https://dotnet.microsoft.com/en-us/learn/dotnet/what-is-dotnet) 8.0 Framework(13.0.3)
- Other:
  - [PostgreSQL](https://www.postgresql.org/) (16.3)
  - [pgAdmin 4](https://www.pgadmin.org/) (8.9)
  - Gemini API Key (2.0 Flash)
- Recommended IDE: [Visual Studio](https://visualstudio.microsoft.com/) 2022
