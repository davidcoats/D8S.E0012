using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using R5T.T0141;


namespace D8S.E0012
{
    [DemonstrationsMarker]
    public partial interface IDemonstrations : IDemonstrationsMarker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Uses code adapted from: <see href="https://andrewlock.net/exploring-the-dotnet-8-preview-rendering-blazor-components-to-a-string/#rendering-components-without-a-di-container"/>
        /// </remarks>
        public async Task Render_Component_ToString_UsingBlazorRenderingOperator()
        {
            var pairs = new Dictionary<string, object>
            {
                { "Message", "Hello from the Render Message component!" }
            };

            var output = await Instances.BlazorRenderingOperator.Render<RenderMessage>(
                pairs);

            Console.WriteLine(output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Code from: <see href="https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-components-outside-of-aspnetcore?view=aspnetcore-8.0"/>
        /// </remarks>
        public async Task Render_Component_ToString()
        {
            IServiceCollection services = new ServiceCollection();
            //services.AddLogging();

            await using ServiceProvider serviceProvider = services.BuildServiceProvider();
            //ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            using var loggerFactory = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();

            await using var htmlRenderer = new HtmlRenderer(serviceProvider, loggerFactory);

            var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
            {
                var dictionary = new Dictionary<string, object>
                {
                    { "Message", "Hello from the Render Message component!" }
                };

                var parameters = ParameterView.FromDictionary(dictionary);
                var output = await htmlRenderer.RenderComponentAsync<RenderMessage>(parameters);

                return output.ToHtmlString();
            });

            Console.WriteLine(html);
        }
    }
}
