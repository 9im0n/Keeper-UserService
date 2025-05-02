using Keeper_UserService.Db;
using Keeper_UserService.Repositories.Implementations;
using Keeper_UserService.Repositories.Interfaces;
using Keeper_UserService.Services.Implementations;
using Keeper_UserService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Middelwares;

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

            // Auth
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration.GetSection("JwtSettings:ValidIssuer").Value,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration.GetSection("JwtSettings:ValidAudience").Value,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                            builder.Configuration.GetSection("JwtSettings:IssuerSigningKey").Value
                            )),
                        ValidAlgorithms = new string[] { SecurityAlgorithms.HmacSha256 },
                    };
                });

            // Db
            string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection));

            // Repos
            builder.Services.AddScoped<IBaseRepository<Permission>, BaseRepository<Permission>>();
            builder.Services.AddScoped<IRolesRepository, RolesRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

            // Services
            builder.Services.AddScoped<IPermissionsService, PermissionService>();
            builder.Services.AddScoped<IRolesService, RolesService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProfileService, ProfileService>();
            builder.Services.AddScoped<IDTOMapper, DTOMapper>();


            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

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
