using System.Runtime.CompilerServices;
using TaxCalulator.UI.IServices;
using TaxCalulator.UI.IServices.Interface;

namespace TaxCalulator.UI.Extensions
{
    public static class ServiceRegistration
    {
        public static WebApplicationBuilder RegisterDependencies(this WebApplicationBuilder builder) 
        { 
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();
            builder.Services.AddHttpClient<IBaseService,BaseService>();
            builder.Services.AddHttpClient<ITaxService,TaxService>();

            builder.Services.AddScoped<IBaseService, BaseService>();
            builder.Services.AddScoped<ITaxService, TaxService>();
            
            return builder;
        }
    }
}
