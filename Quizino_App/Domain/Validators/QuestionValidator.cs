using Domain.Interfaces;
using FluentValidation;

namespace Domain.Validators
{
    public class QuestionValidator : AbstractValidator<IQuestion>
    {
        public QuestionValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(x => x.Answer).GreaterThanOrEqualTo(0).WithMessage("Answer must be greater than or equal to 0");
            RuleFor(x => x.ApplicableCategories).NotEmpty().WithMessage("Category cannot be empty");
            RuleFor(x => x.Text).NotEmpty().WithMessage("Text cannot be empty");
            RuleFor(x => x.OptionA).NotEmpty().WithMessage("Option A cannot be empty");
            RuleFor(x => x.OptionB).NotEmpty().WithMessage("Option B cannot be empty");
            RuleFor(x => x.OptionC).NotEmpty().WithMessage("Option C cannot be empty");
            RuleFor(x => x.OptionD).NotEmpty().WithMessage("Option D cannot be empty");
            RuleFor(x => x.Answer).Must(ValidateAnswerRange).WithMessage("Answer must be between 0 to 3");
        }

        private bool ValidateAnswerRange(int answer) => answer >= 0 && answer < 4;        
    }
}
