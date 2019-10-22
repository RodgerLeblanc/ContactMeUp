using ContactMeUp.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactMeUp.Validators
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        private static readonly Regex _phoneRegex = DefaultPhoneRegex();

        public ContactValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Le nom est requis.");

            RuleFor(c => c.SMS)
                .NotEmpty()
                .When(c => string.IsNullOrEmpty(c.Email))
                .When(c => string.IsNullOrEmpty(c.Other))
                .WithMessage("Un numéro de téléphone, un courriel ou autre doit être entré.");

            RuleFor(c => c.SMS)
                .Matches(_phoneRegex)
                .When(c => !string.IsNullOrEmpty(c.SMS))
                .WithMessage("Le numéro de téléphone n'est pas valide.");

            RuleFor(c => c.Email)
                .EmailAddress()
                .When(c => !string.IsNullOrEmpty(c.Email))
                .WithMessage("Le courriel n'est pas valide.");
        }

        private static Regex DefaultPhoneRegex()
        {
            //Code from : https://referencesource.microsoft.com/#System.ComponentModel.DataAnnotations/DataAnnotations/PhoneAttribute.cs,b548844fd09fbeb1

            const string pattern = @"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$";
            const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

            // Set explicit regex match timeout, sufficient enough for phone parsing
            // Unless the global REGEX_DEFAULT_MATCH_TIMEOUT is already set
            TimeSpan matchTimeout = TimeSpan.FromSeconds(2);

            try
            {
                if (AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") == null)
                {
                    return new Regex(pattern, options, matchTimeout);
                }
            }
            catch
            {
                // Fallback on error
            }

            // Legacy fallback (without explicit match timeout)
            return new Regex(pattern, options);
        }
    }
}
