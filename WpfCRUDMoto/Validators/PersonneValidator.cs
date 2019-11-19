using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRUDMoto.Validators
{
    class PersonneValidator : AbstractValidator<Personne>
    {
        public PersonneValidator()
        {
            RuleFor(p => p.Nom)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Length(2,50).WithMessage("{PropertyName} doit contenir entre 2 et 50 caractères ({TotalLength})")
                .NotEmpty().WithMessage("{PropertyName} ne peux pas être vide")
                .Must(BeAValideName).WithMessage("{PropertyName} contient des caractères invalides");
                
        }

        protected bool BeAValideName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(Char.IsLetter);
        }
    }
}
