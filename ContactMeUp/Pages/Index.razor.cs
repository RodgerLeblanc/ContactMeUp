using ContactMeUp.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMeUp.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IReferenceHandler ReferenceHandler { get; set; }
        [Parameter] public string ReferenceName { get; set; }
        protected Reference Reference { get; private set; }

        protected override void OnInitialized()
        {
            if (!string.IsNullOrEmpty(ReferenceName))
            {
                Reference = ReferenceHandler.References.FirstOrDefault(r => r.RowKey == ReferenceName);
                ReferenceHandler.CurrentReference = Reference;
            }

            base.OnInitialized();
        }
    }
}
