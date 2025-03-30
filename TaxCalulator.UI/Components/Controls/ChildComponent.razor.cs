using Microsoft.AspNetCore.Components;

namespace TaxCalulator.UI.Components.Controls
{
    public partial class ChildComponent(ILogger<ChildComponent> logger)
    {
        private readonly ILogger<ChildComponent> _logger = logger;

        [CascadingParameter(Name ="ChildGreeting")]
        public string ChildGreeting { get; set; } = "empty";
        

        public override Task SetParametersAsync(ParameterView parameters)
        {
            _logger.LogInformation($"{nameof(ChildComponent)} SetParametersAsync...");
            return base.SetParametersAsync(parameters);
        }

        protected override void OnInitialized()
        {
            _logger.LogInformation($"{nameof(ChildComponent) } OnInitialized..{ChildGreeting}");
        }

        protected override void OnParametersSet()
        {
            _logger.LogInformation($"{nameof(ChildComponent)} OnParametersSet..{ChildGreeting}");
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _logger.LogInformation($"{nameof(ChildComponent)} OnAfterRenderAsync during firstRender..{ChildGreeting}");
                //this.ChildGreeting = "Thank you so much";
                return;
            }

            _logger.LogInformation($"{nameof(ChildComponent)} OnAfterRender during not firstRender..{ChildGreeting}");
        }

        protected override bool ShouldRender()
        {
            _logger.LogInformation($"{nameof(ChildComponent)} ShouldRender..{ChildGreeting}");
            return true;
        }
    }
}