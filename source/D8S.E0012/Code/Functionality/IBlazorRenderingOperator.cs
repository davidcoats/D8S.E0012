using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using R5T.T0132;


namespace D8S.E0012
{
    /// <summary>
    /// Based on <see href="https://andrewlock.net/exploring-the-dotnet-8-preview-rendering-blazor-components-to-a-string/#rendering-components-without-a-di-container"/>
    /// </summary>
    [FunctionalityMarker]
    public partial interface IBlazorRenderingOperator : IFunctionalityMarker
    {
        public Task<string> Render<TComponent>()
            where TComponent : IComponent
            => this.Render<TComponent>(
                ParameterView.Empty);

        public Task<string> Render<TComponent>(
            Dictionary<string, object> pairs)
            where TComponent : IComponent
            => this.Render<TComponent>(
                ParameterView.FromDictionary(pairs));


        public async Task<string> Render<TComponent>(
            ParameterView parameters)
            where TComponent : IComponent
        {
            var services = new ServiceCollection();

            await using var serviceProvider = services.BuildServiceProvider();

            using var loggerFactory = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();

            await using var htmlRenderer = new HtmlRenderer(
                serviceProvider,
                loggerFactory);

            var output = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
            {
                var output = await htmlRenderer.RenderComponentAsync<TComponent>(parameters);
                return output.ToHtmlString();
            });

            return output;
        }
    }
}
