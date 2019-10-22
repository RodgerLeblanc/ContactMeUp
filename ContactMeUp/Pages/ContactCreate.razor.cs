using Blazored.Modal;
using Blazored.Modal.Services;
using ContactMeUp.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMeUp.Pages
{
    public abstract class ContactCreateBase : ContactEditBase
    {
        protected override void OnInitialized()
        {
            Contact = new Contact();
        }

        protected override async Task<Contact> ValidSubmitAsync()
        {
            Contact result = await base.ValidSubmitAsync();

            Contact = new Contact();
            StateHasChanged();

            return result;
        }

        protected override void Cancel()
        {
            Contact = new Contact();
            StateHasChanged();
        }
    }
}
