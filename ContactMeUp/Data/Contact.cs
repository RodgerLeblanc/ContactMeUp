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
        [Required(ErrorMessage = "Le nom est requis.")]
        public string Name { get; set; }

        [Phone(ErrorMessage = "Le numéro de téléphone n'est pas valide.")]
        public string SMS { get; set; }

        [EmailAddress(ErrorMessage = "Le courriel n'est pas valide.")]
        public string Email { get; set; }

        public string Other { get; set; }
    }
}
