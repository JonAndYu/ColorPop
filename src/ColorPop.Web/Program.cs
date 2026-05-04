using ColorPop.Application;
using ColorPop.Application.Interface;
using ColorPop.Core.Abstractions;
using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;
using ColorPop.Core.Rules;
using ColorPop.Core.Services;
using ColorPop.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// MudBlazor services
builder.Services.AddMudServices();

// ColorPop application layer
builder.Services.AddScoped<IGameSession, GameSession>();

// ColorPop Engine + core services
builder.Services.AddScoped<IGameEngine, GameEngine>();
builder.Services.AddScoped<IBoardShuffler, BoardShuffler>();

builder.Services.AddScoped<IMoveValidator, MoveValidator>();
builder.Services.AddScoped<IClusterFinder, ClusterFinder>();
builder.Services.AddScoped<IGravityEngine, GravityEngine>();
builder.Services.AddScoped<IJokerResolver, JokerResolver>();
builder.Services.AddScoped<IWinConditionEvaluator, WinConditionEvaluator>();

builder.Services.AddSingleton(new GameSettings(playerCount: 2, seed: 42));


await builder.Build().RunAsync();
