# Real Time Tasks

An application to manage tasks and their status of completion with the users getting updated in real-time
through web sockets. Built in C#, React, Entity Framework, and SignalR.

# Installation & Setup

* Click 'Code' and then 'Download ZIP'
* Make sure to have [SQL Express](https://www.microsoft.com/en-us/download/details.aspx?id=55994) installed
* Modify the connection string in the [appsettings.json](https://github.com/YochevedWaj/RealTimeTasks/blob/master/RealTimeTasks.Web/appsettings.json) to reflect your database environment
* Add migrations to the data project `dotnet ef migrations add initial`
* Update your database `dotnet ef database update`
