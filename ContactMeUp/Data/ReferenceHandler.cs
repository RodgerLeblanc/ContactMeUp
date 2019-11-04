using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMeUp.Data
{
    public class ReferenceHandler : IReferenceHandler
    {
        public ReadOnlyCollection<Reference> References { get; set; } 
        public Reference CurrentReference { get; set; }
    }
}
