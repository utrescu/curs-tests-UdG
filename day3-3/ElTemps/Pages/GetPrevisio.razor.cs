using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using ElTemps;
using ElTemps.Shared;
using ElTemps.Data;

namespace ElTemps.Pages
{
    public partial class GetPrevisio
    {
        [Parameter]
        public string Poble { get; set; }

        private Previsio[] previsions;
        protected override async Task OnInitializedAsync()
        {
            previsions = await PrevisioService.GetPrevisioSetmana(Poble);
        }
    }
}