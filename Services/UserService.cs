using MavickBackend.Models;
using MavickBackend.Models.Common;
using MavickBackend.Models.Request;
using MavickBackend.Models.Response;
using MavickBackend.Tools;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MavickBackend.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public UserService()
        {
        }

        public UserResponse? Auth(AuthRequest model)
        {

            var userResponse = new UserResponse();
            using (var db = new MavickDBContext())
            {

                string spassword = Encrypt.GetSHA256(model.Password);
                var user = db.Users.Where(d => d.Email == model.Email && d.Password == spassword).FirstOrDefault();
           
                if (user == null)
                {
                    return null;
                }
                userResponse.Email = user.Email;
                userResponse.Token = GetToken(user);
            }
            return userResponse;

        }

        public List<User> Get()
        {
            using var db = new MavickDBContext();
            var lst = db.Users.ToList();
            return lst;
        }

        public User? GetByEmail(string mail)
        {
            using var db = new MavickDBContext();
            var oUser = db.Users.Where(d=> d.Email == mail).FirstOrDefault();

            return oUser;
        }   

        public User? AddUser(UserRequets oModel)
        {
            using var db = new MavickDBContext();
            var aux = db.Users.Where(d => d.Email == oModel.Email).FirstOrDefault();
            var aux2 = db.Users.Where(d => d.Phone == oModel.Phone).FirstOrDefault();

           if(aux==null && aux2 == null)
            {
                User oUser = new()
                {
                    Name = oModel.Name,
                    Email = oModel.Email,
                    Password = Encrypt.GetSHA256(oModel.Password),
                    Lastname = oModel.Lastname,
                    Phone = oModel.Phone,
                    Photo = oModel.Photo,
                    Role = 1
                };
                db.Users.Add(oUser);
                db.SaveChanges();
                return oUser;
            }
            else
            {
                return null;
            }
        }

        public User? UpdateUser(UserRequets oModel)
        {
            using var db = new MavickDBContext();
            var oUser = db.Users.Find(oModel.Id);
            if (oUser == null)
            {
                return null;
            }
            oUser.Name = oModel.Name;
            oUser.Email = oModel.Email;
            oUser.Password = oModel.Password;
            oUser.Lastname = oModel.Lastname;
            oUser.Phone = oModel.Phone;
            oUser.Photo = oModel.Photo;
            oUser.Role = oModel.Role;
            db.Entry(oUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();

            return oUser;
        }

        public User? DeleteUser(long id)
        {
            using var db = new MavickDBContext();
            var oUser = db.Users.Find(id);
            if (oUser == null)
            {
                return null;
            }
            db.Users.Remove(oUser);
            db.SaveChanges();

            return oUser;
        }

        private string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
