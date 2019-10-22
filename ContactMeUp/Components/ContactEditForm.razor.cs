using ContactMeUp.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMeUp.Components
{
    public abstract class ContactEditFormBase : ComponentBase
    {
        [CascadingParameter]
        protected Contact Contact { get; set; }

        [ParameterAttribute]
        public bool ShowCancelButton { get; set; }

        [Parameter]
        public EventCallback OnValidSubmit { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
