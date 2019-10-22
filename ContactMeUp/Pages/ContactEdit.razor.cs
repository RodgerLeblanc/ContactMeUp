using Blazored.Modal.Services;
using ContactMeUp.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMeUp.Pages
{
    public abstract class ContactEditBase : ComponentBase
    {
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected ContactService ContactService { get; set; }

        [Parameter]
        public Guid RowKey { get; set; }

        protected bool IsNewContact => RowKey == default;

        protected Contact Contact { get; set; } = new Contact();

        protected override async Task OnInitializedAsync()
        {
            if (RowKey != default)
            {
                Contact = await ContactService.GetAsync(RowKey);
                StateHasChanged();
            }
        }

        protected async Task ValidSubmitAsync()
        {
            await ContactService.CreateOrUpdateAsync(Contact);

            if (IsNewContact)
            {
                Contact = new Contact();
                StateHasChanged();
            }
            else
            {
                BackToList();
            }
        }

        protected void Cancel()
        {
            BackToList();
        }

        protected void BackToList()
        {
            NavigationManager.NavigateTo("/contacts");
        }
    }
}
