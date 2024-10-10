using System.Net.Mime;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using RengieExApi.Validators;
using RengieExModels.Requests;
using RengieExServices;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;
using RengieExApi.Extensions;
using RengieExServices.Wrappers;

namespace RengieExApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            // Get all generated assembly xml so that every needed model will be display with accurate details
            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var xmlFile in xmlFiles)
            {
                c.IncludeXmlComments(xmlFile);
            }

            //Allow swagger to inject examples as default values
            c.ExampleFilters();
        });

        // Add swagger filters by scanning the appropriate assembly to get examples.
        builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

        //Validation registers.
        //auto validate before action
        //builder.Services.AddFluentValidationAutoValidation();

        //validation is left to the developer discretion.
        builder.Services.AddFluentValidationClientsideAdapters();

        builder.Services.AddSingleton<IValidator<RegexRequest>, RegexRequestValidator>();

        //Services registers.
        builder.Services.AddSingleton<IRegexService, RegexService>();
        builder.Services.AddSingleton<IRegexProvider, RegexProvider>();



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment()) (removed so that swagger is always available even on production)
        //{
            app.UseSwagger();
            app.UseSwaggerUI();
        //}

        //only for unpredictable exceptions
        app.UseWebAppExceptionHandler();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}