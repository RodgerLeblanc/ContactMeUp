using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMeUp.Data
{
    public class ContactService : BaseAzureStorageService<Contact>
    {
        public ContactService(ContactAzureStorageConfiguration contactConfiguration, IConfiguration configuration) : 
            base(contactConfiguration, configuration)
        {
        }
    }
}
