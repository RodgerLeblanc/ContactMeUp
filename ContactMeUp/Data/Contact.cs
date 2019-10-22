using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMeUp.Data
{
    public class Contact : TableEntity
    {
        public string Name { get; set; }
        public string SMS { get; set; }
        public string Email { get; set; }
        public string Other { get; set; }
    }
}
