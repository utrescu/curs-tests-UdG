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
    public partial class Index
    {
        private string[] Pobles;
        private string poble;
        string Poble
        {
            get => poble;
            set
            {
                poble = value;
                NavigationManager.NavigateTo("/previsio/" + poble, false);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            Pobles = await PrevisioService.GetPobles();
        }
    }
}