using ContactMeUp.Data;
using Microsoft.AspNetCore.Components;
using Sparks.Components.Blazor;
using Sparks.Components.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMeUp.Pages
{
    public abstract class ContactUpdateBase : ContactEditBase
    {
        [Inject] protected IModalService ModalService { get; set; }

        [CascadingParameter] ModalParameters Parameters { get; set; }

        protected override void OnInitialized()
        {
            Contact = new Contact();

            if (Parameters != null && Parameters.Get<Contact>("ToUpdate") is Contact contact)
            {
                Contact = contact;
                StateHasChanged();
            }
        }

        protected override async Task<Contact> ValidSubmitAsync()
        {
            Contact result = await base.ValidSubmitAsync();

            ModalService.Close(ModalResult.Ok(result));

            return result;
        }

        protected override void Cancel()
        {
            ModalService.Cancel();
        }
    }
}
