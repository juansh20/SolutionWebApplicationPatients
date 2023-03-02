using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplicationPatient.Interfaces;
using WebApplicationPatient.Models;
using WebApplicationPatient.Repositories;
using WebApplicationPatient.Services;
using WebApplicationPatients.Context;
using WebApplicationPatients.Utils;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        var appSettingsSection = configuration.GetSection("AppSettings");
        builder.Services.Configure<AppSettings>(appSettingsSection);

        // Add services to the container.
        var appSettings = appSettingsSection.Get<AppSettings>();
        var key = Encoding.ASCII.GetBytes(appSettings.Secret);

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                    var userId = int.Parse(context.Principal.Identity.Name.Split("|")[0]);
                    var user = userService.GetUserById(userId);
                    if (user == null)
                    {
                        // return unauthorized if user no longer exists
                        context.Fail("Unauthorized");
                    }
                    return Task.CompletedTask;
                }
            };
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true, // true para validar el firmante del token boxmis.api
                ValidIssuer = appSettings.Issuer, //  valida quien genera el token boxmis.api
                ValidateAudience = false
            };
        });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //connection database
        builder.Services.AddDbContext<DefaultContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        //repositories
        builder.Services.AddScoped<IPatientRepository, PatientRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        //services
        builder.Services.AddTransient<PatientValidator>();
        builder.Services.AddTransient<IPatientService, PatientService>();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<HttpClient>();
        builder.Services.AddMemoryCache();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        //using (var scope = app.Services.CreateScope())
        //{
        //    var services = scope.ServiceProvider;

        //    try
        //    {
        //        var context = services.GetRequiredService<DefaultContext>();
        //        PatientSeeder.Seed(context, 100);
        //    }
        //    catch (Exception ex)
        //    {
        //        var logger = services.GetRequiredService<ILogger<Program>>();
        //        logger.LogError(ex, "An error occurred seeding the database.");
        //    }
        //}


        app.Run();
    }
}


