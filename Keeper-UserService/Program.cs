using Keeper_UserService.Db;
using Keeper_UserService.Repositories.Implementations;
using Keeper_UserService.Repositories.Interfaces;
using Keeper_UserService.Services.Implementations;
using Keeper_UserService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Keeper_UserService
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

            // Configuration
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailService"));

            // Db
            string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection));

            // Repos
            builder.Services.AddScoped<IRolesRepository, RolesRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IActivationPasswordsRepository, ActivationPasswordsRepository>();

            // Services
            builder.Services.AddScoped<IRolesService, RolesService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IActivationPasswordService, ActivationPasswordsService>();
            builder.Services.AddScoped<IEmailService, EmailService>();


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
