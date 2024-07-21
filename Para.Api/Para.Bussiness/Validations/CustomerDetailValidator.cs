using FluentValidation;
using Para.Data.Domain;

namespace Para.Bussiness.Validations
{
    public class CustomerDetailValidator : AbstractValidator<CustomerDetail>
    {
        public CustomerDetailValidator()
        {
            RuleFor(x => x.FatherName)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.MotherName)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.MonthlyIncome)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(x => x.Occupation)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
