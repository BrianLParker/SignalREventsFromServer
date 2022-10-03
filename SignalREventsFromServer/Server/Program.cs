using Microsoft.AspNetCore.ResponseCompression;
using SignalREventsFromServer.Server.Brokers.HubContexts;
using SignalREventsFromServer.Server.Hubs;
using SignalREventsFromServer.Server.Services.Events;

namespace SignalREventsFromServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSignalR();
            builder.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IHubContextBroker, HubContextBroker>();
            builder.Services.AddScoped<IEventsService, EventsService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseResponseCompression();

            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapRazorPages();
            app.MapControllers();
            app.MapHub<ChatHub>(pattern: "/chathub");
            app.MapFallbackToFile(filePath: "index.html");

            app.Run();
        }
    }
}