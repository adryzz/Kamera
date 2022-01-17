using Alphacloud.MessagePack.AspNetCore.Formatters;
using Kamera;

namespace Kamera;

public static class Program
{
    public static Device Reader;
    public static void Main(string[] args)
    {
        Reader = new Device(Configuration.Port);
        Reader.Start();
        
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMessagePack();
        builder.Services.AddSingleton<ImageGenerator>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
