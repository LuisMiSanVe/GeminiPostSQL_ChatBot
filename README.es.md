> [Ver en ingles/See in english](https://github.com/LuisMiSanVe/GeminiPostSQL_ChatBot/tree/main)

<img src="https://github.com/LuisMiSanVe/LuisMiSanVe/blob/main/Resources/GeminiPostSQL/GeminiPostSQLChatBot_banner.png" style="width: 100%; height: auto;" alt="GeminiPostSQL Banner">

# <img src="https://github.com/LuisMiSanVe/GeminiPostSQL_ChatBot/blob/main/ChatBotApp/wwwroot/favicon.png" width="40" alt="Logo de GeminiPostSQL"> GeminiPostSQL ChatBot | ChatBot de Blazor Asistido por IA para PostgreSQL
[![image](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://dotnet.microsoft.com/en-us/languages/csharp)
[![image](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/en-us/learn/dotnet/what-is-dotnet)
[![image](https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Blazor](https://img.shields.io/badge/blazor-%235C2D91.svg?style=for-the-badge&logo=blazor&logoColor=white)](https://dotnet.microsoft.com/es-es/apps/aspnet/web-apps/blazor)
[![image](https://img.shields.io/badge/json-5E5C5C?style=for-the-badge&logo=json&logoColor=white)](https://www.newtonsoft.com/json)
[![image](https://img.shields.io/badge/Google%20Gemini-8E75B2?style=for-the-badge&logo=googlegemini&logoColor=white)](https://aistudio.google.com/app/apikey)
[![image](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white)](https://visualstudio.microsoft.com/)

>[!NOTE]
> Esta es la versión de ChatBot en Blazor pensada para ser usada como cliente web. Hay una versión de [REST API](https://github.com/LuisMiSanVe/GeminiPostSQL_API/blob/main/README.es.md) pensada para su uso en servidores o clientes con la interfaz de Swagger y otra en [WinForms](https://github.com/LuisMiSanVe/GeminiPostSQL/tree/main) para su uso en Escritorio de Windows.

Este ChatBot usa la IA de Google 'Gemini 2.0 Flash' para generar consultas a bases de datos PostgreSQL.  
La IA convierte lenguaje natural a consultas SQL, estas se ejecutan y los resultados son transformados a lenguaje natural de nuevo.

## 📋 Prerequisitos

Para que el programa funcione, necesiatarás un servidor PostgreSQL y una clave de la API de Gemini.

> [!NOTE]  
> Yo usaré pgAdmin para montar el servidor PostgreSQL.

## 🛠️ Instalación

Si no lo tienes, descarga [pgAdmin 4 desde su página ofical](https://www.pgadmin.org/download/).  
Ahora, crea el servidor y monta la base de datos con algunas tablas y valores.

Después, obten tu clave de la API de Gemini yendo aqui: [Google AI Studio](https://aistudio.google.com/app/apikey). Asegurate de tener tu sesión de Google abierta, y encontes dale al botón que dice 'Crear clave de API' y sigue los pasos para crear tu proyecto de Google Cloud y conseguir tu clave de API. **Guardala en algún sitio seguro**.  
Google permite el uso gratuito de esta API sin añadir ninguna forma de pago, pero con algunas limitaciones.

En Google AI Studio, puedes monitorizar el uso de la IA haciendo clic en 'Ver datos de uso' en la columna de 'Plan' en la tabla con todos tus proyectos. Recomiendo monitorizarla desde la pestaña de 'Cuota y límites del sistema' y ordenando por 'Porcentaje de uso actual', ya que es donde más información obtienes.

Ya tienes todo lo que necesitas para hacer funcionar el programa.  
Simplemente pon los datos que acabas de conseguir en las pantallas de configuración del programa.

## 💻 Tecnologías usadas
- Lenguaje de programación: [C#](https://dotnet.microsoft.com/en-us/languages/csharp)
- Framework: [Blazor](https://dotnet.microsoft.com/es-es/apps/aspnet/web-apps/blazor), [.Net](https://dotnet.microsoft.com/en-us/learn/dotnet/what-is-dotnet) 8.0 Framework
- Otros:
  - [PostgreSQL](https://www.postgresql.org/) (16.3)
  - [pgAdmin 4](https://www.pgadmin.org/) (8.9)
  - Gemini API Key (2.0 Flash)
- IDE Recomendado: [Visual Studio](https://visualstudio.microsoft.com/) 2022
