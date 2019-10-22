using Blazored.Modal;
using Blazored.Modal.Services;
using ContactMeUp.Components;
using ContactMeUp.Data;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMeUp.Pages
{
    public abstract class ContactListBase : ComponentBase
    {
        [Inject] protected IModalService ModalService { get; set; }
        [Inject] protected ContactService ContactService { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }

        protected IList<Contact> Contacts { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            Contacts = await ContactService.GetAsync();
        }

        protected void ConfirmDelete(Contact contact)
        {
            ModalService.OnClose += ModalClosed;

            ModalParameters parameters = new ModalParameters();
            parameters.Add("ToDelete", contact);

            ModalService.Show<DeleteContactConfirmation>("Supprimer le contact?", parameters);
        }

        private async void ModalClosed(ModalResult modalResult)
        {
            ModalService.OnClose -= ModalClosed;

            if (!modalResult.Cancelled)
            {
                Contacts = await ContactService.GetAsync();
                StateHasChanged();
            }
        }
    }
}
