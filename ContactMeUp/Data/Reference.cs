using ContactMeUp.Extensions;
using Microsoft.WindowsAzure.Storage.Table;
using System.Text.RegularExpressions;

namespace ContactMeUp.Data
{
    public class Reference : TableEntity
    {
        public Reference(string name, string relativeUrl)
        {
            Name = name;
            RowKey = relativeUrl;
        }

        public string Name { get; }
    }
}