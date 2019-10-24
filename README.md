# ContactMeUp

[![Build Status](https://dev.azure.com/RodgerLeblanc/ContactMeUp/_apis/build/status/ContactMeUp20191017083758%20-%20CI?branchName=master)](https://dev.azure.com/RodgerLeblanc/ContactMeUp/_build/latest?definitionId=1&branchName=master)

Blazor application used to collect contact informations at some event.

To use this app, add an `appsettings.json` file and include your Azure Storage ConnectionString.
```csharp
{
  "ConnectionStrings": {
    "DefaultConnection": "<YOUR_CONNECTION_STRING>"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```
