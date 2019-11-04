using System.Collections.ObjectModel;

namespace ContactMeUp.Data
{
    public interface IReferenceHandler
    {
        ReadOnlyCollection<Reference> References { get; set; }
        Reference CurrentReference { get; set; }
    }
}