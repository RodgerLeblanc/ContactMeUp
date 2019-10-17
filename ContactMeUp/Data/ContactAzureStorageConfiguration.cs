using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMeUp.Data
{
    public class ContactAzureStorageConfiguration : IAzureStorageConfiguration
    {
        public string ConnectionStringName => "ContactMeUpStorage";
        public string TableName => "Contacts";
    }
}
