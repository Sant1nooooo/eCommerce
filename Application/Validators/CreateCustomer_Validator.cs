
using FluentValidation;
using Server.Application.Command_Operations;
using Server.Application.Interfaces;

namespace Server.Application.Validators
{
    public class CreateCustomer_Validator : AbstractValidator<CreateCustomer_Command>
    {
        public CreateCustomer_Validator(IUsersRepository _usersRepository)
        {
            RuleFor(customer => customer.Email)
                .MustAsync(async (Email, _) => {
                    return !await _usersRepository.IsEmailExisting(Email!);
                })
                .WithMessage($"WARNING: Email is already existing!");
        }
    }
}
