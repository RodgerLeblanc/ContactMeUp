﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMeUp.Data
{
    public class ContactAzureStorageConfiguration : IAzureStorageConfiguration
    {
        public string ConnectionStringName => "ContactConnection";
        public string TableName => "Contacts";
        public bool IsReadOnly => false;
    }
}
