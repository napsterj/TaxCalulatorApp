using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TaxCalulator.API.Infrastructure
{
    public class ProblemExtension : ProblemDetails
    {
        public string Path { get; set; } = string.Empty;
    }
}
