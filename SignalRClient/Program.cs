// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;

var url = "http://localhost:7071/api";

var connection = new HubConnectionBuilder()
    .WithUrl(url)
    .Build();

await connection.StartAsync();
var responseFromServer = await connection.InvokeAsync<string>("SendMessage", "Hello from client");
Console.WriteLine(responseFromServer ?? "(Receive null response)");
Console.ReadLine();
await connection.StopAsync();
