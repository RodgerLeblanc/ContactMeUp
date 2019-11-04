using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMeUp.Data
{
    public class ReferenceHandler : IReferenceHandler
    {
        public ReadOnlyCollection<Reference> References { get; } =
            new ReadOnlyCollection<Reference>(
                new List<Reference>
                {
                    new Reference("Marc-André Paillé", "marcandre"),
                    new Reference("Martin Descoteaux", "martin"),
                    new Reference("Roger Leblanc", "roger"),
                    new Reference("Stéphane Poirier", "stephane"),
                    new Reference("Steve Lavoie", "stevel"),
                    new Reference("Steve Riley", "stever"),
                });

        public Reference CurrentReference { get; set; }
    }
}
