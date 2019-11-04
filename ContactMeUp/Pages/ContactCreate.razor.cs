using ContactMeUp.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace ContactMeUp.Pages
{
    public abstract class ContactCreateBase : ContactEditBase
    {
        [Parameter] public Reference Reference { get; set; }
        [Inject] public IReferenceHandler ReferenceHandler { get; set; }

        protected override void OnInitialized()
        {
            Contact = new Contact();
        }

        protected override async Task<Contact> ValidSubmitAsync()
        {
            Contact.Reference = ReferenceHandler.CurrentReference?.Name;
            Contact.CreationTime = DateTime.Now;

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
