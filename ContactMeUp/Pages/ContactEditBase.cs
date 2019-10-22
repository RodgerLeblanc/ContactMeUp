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
        [Inject]
        protected ContactService ContactService { get; set; }

        protected Contact Contact { get; set; }

        protected virtual async Task<Contact> ValidSubmitAsync()
        {
            return await ContactService.CreateOrUpdateAsync(Contact);
        }

        protected abstract void Cancel();
    }
}
