# Overview
This is a repo containing the code to repro the issue that function return data is not returned by SignalR trigger as HTTP trigger. One project is a function app, which contains two functions. The `negotiate` function returns a url and an access token for SignalR client to connect to the SignalR service. The `sendMessage` function in `SignalRTrigger.cs` file handles the SignalR client invocation named `sendMessage`, returning a string to the SignalR client. The other project is a csharp SignalR client and it invokes `sendMessage` after connected.

## Prerequisites
1. A SignalR service instance. You might want to read the doc for [how to create an SignalR Service instance](https://learn.microsoft.com/azure/azure-signalr/signalr-quickstart-azure-functions-csharp?tabs=isolated-process#create-an-azure-signalr-service-instance). Please make sure select **Serverless** for **Service mode** when you create the resource. Copy and paste the connection string.
2. An Azure Relay resource and a tool [tunnelrelay](https://github.com/OfficeDev/microsoft-teams-tunnelrelay) to expose your local function endpoint to the internet if you want to run the function app locally.

## Option 1: Run the function app on Azure
### Function App
1. Go to the **FunctionApp** folder.
1. Publish the function app to Azure.
1. Add the SignalR connection string to the app setting `AzureSignalRConnectionString`.
1. Configure SignalR upstream settings so that SignalR service knows where the function app endpoint is. See https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-signalr-service-trigger?tabs=in-process&pivots=programming-language-csharp#signalr-service-integration

### SignalR client
1. Go to the **SignalRClient** folder.
1. Open **Program.cs** file.
1. Update the url to `https://{YourFunctionAppName}/api`.
1. Run the project. You should be able to see that the result of `var responseFromServer = await connection.InvokeAsync<string>("SendMessage", "Hello from client");` is null.

## Option 2: Run the function app locally
1. Set up Azure Relay tunnel. In the GUI of tunnel relay, set the URL under "Redirecting incoming requests to" as "http://localhost:7071".
2. Configure SignalR upstream settings so that SignalR service knows where the function app endpoint is. [See here for how to configure the SignalR upstream](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-signalr-service-trigger?tabs=in-process&pivots=programming-language-csharp#signalr-service-integration). Please note that the upstream URL is like  `https://{YourAzureRelayResourceName}.servicebus.windows.net:443/{YourRelayConnectionName}/runtime/webhooks/signalr` .
1. Go to the **FunctionApp** folder.
1. Add the SignalR connection string to the app setting `AzureSignalRConnectionString`.
1. Start the function app.
1. Go to the **SignalRClient** folder.
1. Run the project.  You should be able to see that the result of `var responseFromServer = await connection.InvokeAsync<string>("SendMessage", "Hello from client");` is null.




