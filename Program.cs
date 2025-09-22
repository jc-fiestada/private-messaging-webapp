using PrivateChat.Models;
using PrivateChat.Connection;
using PrivateChat.Endpoints;
using Microsoft.AspNetCore.SignalR;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSignalR();

var app = builder.Build();
app.UseRouting();
app.UseStaticFiles();
app.UseSession();

app.MapHub<ChatHub>("/chat-hub");

app.MapPost("/user-signin", (User user) =>
{
    
});

app.Run();
