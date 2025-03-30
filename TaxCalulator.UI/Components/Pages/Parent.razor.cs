using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata.Ecma335;

namespace TaxCalulator.UI.Components.Pages
{
    public partial class Parent(ILogger<Parent> logger)
    {
        private readonly ILogger<Parent> _logger = logger;

        [Parameter]
        public string greeting { get; set; }
        public string AnotherChildGreeting { get; set; }
        
        public void OnGreetingChange(string value)
        {
            greeting = value ?? "Abhishek";
        }

        public void OnAnotherChildGreetingChange(string value)
        {
            AnotherChildGreeting = value;
        }

        public void DisplayAnotherChildMessage(string acknowledge)
        {
            _logger.LogInformation($"{nameof(DisplayAnotherChildMessage)}... {acknowledge}");
        }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            _logger.LogInformation("SetParametersAsync...");
            return base.SetParametersAsync(parameters);
        }

        protected override void OnInitialized()
        {
            _logger.LogInformation("OnInitialized..");
        }

        protected override void OnParametersSet()
        {
            _logger.LogInformation($"OnParametersSet..{greeting}");
        }



        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _logger.LogInformation($"OnAfterRenderAsync during firstRender..{greeting}");
                //this.greeting = "Thank you so much";
                return;
            }

            _logger.LogInformation($"OnAfterRender during not firstRender..{greeting}");
        }

        protected override bool ShouldRender()
        {
            _logger.LogInformation($"ShouldRender..{greeting}");
            return true;
        }
    }
}