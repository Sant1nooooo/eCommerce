using Server.Application.Models;
using Server.Application.Interfaces;
using MediatR;
namespace Server.Application.Query_Operations.Users
{
    public class GetAdmin_Query : IRequest<User?>
    {
        public int Id { get; set; }
    }
    public class GetAdmin_QueryHandler : IRequestHandler<GetAdmin_Query, User?>
    {
        private readonly IUsersRepository _userRepository;
        public GetAdmin_QueryHandler(IUsersRepository usersRepository)
        {
            _userRepository = usersRepository;
        }
        public async Task<User?> Handle(GetAdmin_Query request, CancellationToken ct)
        {
            User? searchedAdmin = await _userRepository.GetAdminAsync(request.Id);
            if (searchedAdmin == null || searchedAdmin!.Authentication!.Equals("admin", StringComparison.CurrentCultureIgnoreCase)) return null;
            return searchedAdmin;
        }

    }
}
