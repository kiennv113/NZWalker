using FluentValidation;

namespace NZWalks.API.Validators
{
    public class UpdateWalkDifficultyValidator : AbstractValidator<Models.DTO.WalkDifficultyVM>
    {
        public UpdateWalkDifficultyValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
