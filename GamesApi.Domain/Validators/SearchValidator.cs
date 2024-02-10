using FluentValidation;
using Games.Domain.Models;

namespace Games.Domain.Validators
{
    public class SearchValidator: AbstractValidator<Search>
    {
        public SearchValidator(IList<string> searchOptions)
        {
            RuleFor(x => x.Query).NotEmpty().WithMessage("q parameter is required");
            When(x => !string.IsNullOrEmpty(x.Sort), () =>
                {
                    RuleFor(x => x.Sort)
                    .Must(x => searchOptions.Contains(x))
                    .WithMessage("Sort parameter is invalid");
                }) ;
        }
    }
}
