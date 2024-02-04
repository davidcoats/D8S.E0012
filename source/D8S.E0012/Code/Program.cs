using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;


namespace D8S.E0012
{
    class Program
    {
        static async Task Main()
        {
            //await Demonstrations.Instance.Render_Component_ToString();
            await Demonstrations.Instance.Render_Component_ToString_UsingBlazorRenderingOperator();
        }
    }
}