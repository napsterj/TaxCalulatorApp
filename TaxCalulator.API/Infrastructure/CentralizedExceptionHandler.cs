using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TaxCalulator.DAL;
using TaxCalulator.Entities.Entities;

namespace TaxCalulator.API.Infrastructure
{
    public class CentralizedExceptionHandler : IExceptionHandler
    {
        private IConfiguration _configuration = new ConfigurationManager();
               
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
                                              Exception exception, 
                                              CancellationToken cancellationToken)
        {
            if(exception != null && exception is Exception)
            {
                var problem = new ProblemExtension
                {
                    Type = exception.GetType().Name,
                    Detail = exception.Message,                   
                    Title = "Error in completing your request",
                    Path = httpContext.Request.Path
                };

                if(httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    problem.Status = (int)HttpStatusCode.NotFound;
                    problem.Title = nameof(HttpStatusCode.NotFound);
                }

                if (exception is BadHttpRequestException)
                {
                    problem.Status = (int)HttpStatusCode.BadRequest;
                    problem.Title = nameof(HttpStatusCode.BadRequest);
                }

                await httpContext.Response.WriteAsJsonAsync(problem);

                SaveException(problem);
                
                return true;

            }
            
            return false;
        }

        private void SaveException(ProblemExtension pex)
        {            
            var configuration = new ConfigurationManager().AddJsonFile("appsettings.json")
                                                          .Build();
                        
            var optionsBuilder = new DbContextOptionsBuilder<CalculatorDbContext>()
                                            .UseSqlServer(configuration["ConnectionStrings:Default"]);

            var options = optionsBuilder.Options;
            
            using (var context = new CalculatorDbContext(options))
            {
                var error = new Error
                {
                    Path = pex.Path,
                    Details = pex.Detail!,
                    Type = pex.Type!,
                };
                context.Errors.Add(error);
                context.SaveChanges();
            }
           
        }
    }
}
