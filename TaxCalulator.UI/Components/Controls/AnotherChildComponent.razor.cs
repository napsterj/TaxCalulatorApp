using Microsoft.AspNetCore.Components;

namespace TaxCalulator.UI.Components.Controls
{
    public partial class AnotherChildComponent(ILogger<AnotherChildComponent> logger)
    {
        private readonly ILogger<AnotherChildComponent> _logger = logger;
        
        [Parameter]
        public EventCallback<string> AcknowledgeParent { get; set; }

        [Parameter]
        public string AnotherChildGreeting { get; set; }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            _logger.LogInformation($"{Environment.NewLine} {nameof(AnotherChildComponent)} SetParametersAsync...");
            return base.SetParametersAsync(parameters);
        }

        protected override void OnInitialized()
        {
            _logger.LogInformation($"{nameof(AnotherChildComponent)} OnInitialized..{AnotherChildGreeting}");
        }

        protected override void OnParametersSet()
        {
            _logger.LogInformation($"{nameof(AnotherChildComponent)} OnParametersSet..{AnotherChildGreeting}");            
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _logger.LogInformation($"{nameof(AnotherChildComponent)} OnAfterRenderAsync during firstRender..{AnotherChildGreeting}");
                //this.AnotherChildGreeting = "Thank you so much";
                return;
            }

            //AcknowledgeParent.InvokeAsync($"Thank you parent for the message - {AnotherChildGreeting}");
            _logger.LogInformation($"{nameof(AnotherChildComponent)} OnAfterRender during not firstRender..{AnotherChildGreeting}");
        }

        protected override bool ShouldRender()
        {
            _logger.LogInformation($"{nameof(AnotherChildComponent)} ShouldRender..{AnotherChildGreeting}");
            AcknowledgeParent.InvokeAsync($"{Environment.NewLine} Thank you parent for the message - {AnotherChildGreeting}");
            return true;
        }
    }
}