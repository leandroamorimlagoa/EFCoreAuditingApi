using AuditingApi.Auditings.Repositories;
using AuditingApi.Auditings.Services;
using AuditingApi.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AuditingApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<BusinessContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "AuditingApi");
            }, ServiceLifetime.Singleton);

            builder.Services.AddSingleton<AuditingService>();
            builder.Services.AddSingleton<AuditingDatabase>();

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
}
