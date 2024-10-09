using Microsoft.AspNetCore.Diagnostics;
using System.Net.Mime;

namespace RengieExApi.Extensions
{
    public static class WebAppExceptionHandlerExtension
    {
        public static IApplicationBuilder UseWebAppExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    context.Response.ContentType = MediaTypeNames.Text.Plain;

                    await context.Response.WriteAsync("An exception was thrown.");

                    //custom exception handling
                });
            });

            return app;
        }
    }
}
