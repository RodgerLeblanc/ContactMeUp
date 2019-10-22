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
        [Inject] 
        protected IModalService ModalService { get; set; }
        
        [Inject] 
        protected ContactService ContactService { get; set; }
        
        [Inject] 
        protected NavigationManager NavigationManager { get; set; }

        protected IList<Contact> Contacts { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            Contacts = await ContactService.GetAsync();
        }

        protected void ConfirmDelete(Contact contact)
        {
            ModalService.OnClose += ConfirmDeleteModalClosed;

            ModalParameters parameters = new ModalParameters();
            parameters.Add("ToDelete", contact);

            ModalService.Show<DeleteContactConfirmation>("Supprimer le contact?", parameters);
        }

        private async void ConfirmDeleteModalClosed(ModalResult modalResult)
        {
            ModalService.OnClose -= ConfirmDeleteModalClosed;

            if (!modalResult.Cancelled)
            {
                Contacts = await ContactService.GetAsync();
                StateHasChanged();
            }
        }

        protected void Update(Contact contact)
        {
            ModalService.OnClose += UpdateModalClosed;

            ModalParameters parameters = new ModalParameters();
            parameters.Add("ToUpdate", contact);

            ModalService.Show<ContactUpdate>("Modifier le contact", parameters);
        }

        private async void UpdateModalClosed(ModalResult modalResult)
        {
            ModalService.OnClose -= UpdateModalClosed;

            if (!modalResult.Cancelled)
            {
                Contacts = await ContactService.GetAsync();
                StateHasChanged();
            }
        }
    }
}
