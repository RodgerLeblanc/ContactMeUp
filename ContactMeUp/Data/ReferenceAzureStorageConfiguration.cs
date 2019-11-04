using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMeUp.Data
{
    public class ReferenceAzureStorageConfiguration : IAzureStorageConfiguration
    {
        public string ConnectionStringName => "ReferenceConnection";
        public string TableName => "References";
        public bool IsReadOnly => true;
    }
}
