using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMeUp.Data
{
    public class ReferenceService : BaseAzureStorageService<Reference>
    {
        public ReferenceService(ReferenceAzureStorageConfiguration referenceConfiguration, IConfiguration configuration) : 
            base(referenceConfiguration, configuration)
        {
        }
    }
}
