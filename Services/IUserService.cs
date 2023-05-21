using MavickBackend.Models;
using MavickBackend.Models.Request;
using MavickBackend.Models.Response;

namespace MavickBackend.Services
{
    public interface IUserService
    {
        public UserResponse? Auth(AuthRequest model);
        public List<User> Get();
        public User? GetByEmail(string mail);
        public User? AddUser(UserRequets oModel);
        public User? UpdateUser(UserRequets oModel);
        public User? DeleteUser(long id);

    }
}
