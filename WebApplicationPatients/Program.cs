using Microsoft.EntityFrameworkCore;
using WebApplicationPatient.Interfaces;
using WebApplicationPatient.Models;
using WebApplicationPatient.Repositories;
using WebApplicationPatient.Services;
using WebApplicationPatients.Context;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //connection database
        builder.Services.AddDbContext<DefaultContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        //repositories
        builder.Services.AddScoped<IPatientRepository, PatientRepository>();

        //services
        builder.Services.AddTransient<PatientValidator>();
        builder.Services.AddTransient<IPatientService, PatientService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}


