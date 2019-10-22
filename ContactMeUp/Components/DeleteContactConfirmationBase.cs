using Blazored.Modal;
using Blazored.Modal.Services;
using ContactMeUp.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMeUp.Components
{
    public class DeleteContactConfirmationBase : ComponentBase
    {
        [Inject] protected IModalService ModalService { get; set; }
        [Inject] protected ContactService ContactService { get; set; }

        [CascadingParameter] ModalParameters Parameters { get; set; }

        protected Contact ToDelete { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            ToDelete = Parameters.Get<Contact>("ToDelete");
        }

        protected async Task DeleteAsync()
        {
            int deletedCount = await ContactService.DeleteAsync(ToDelete);
            ModalService.Close(ModalResult.Ok(deletedCount));
        }

        protected void Cancel()
        {
            ModalService.Cancel();
        }
    }
}
