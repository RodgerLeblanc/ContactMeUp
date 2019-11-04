using System.Collections.ObjectModel;

namespace ContactMeUp.Data
{
    public interface IReferenceHandler
    {
        ReadOnlyCollection<Reference> References { get; }
        Reference CurrentReference { get; set; }
    }
}