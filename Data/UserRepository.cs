using AuthTestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTestApi.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;
        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }
        public User Create(User user)
        {
            _userContext.Add(user);
            user.Id = _userContext.SaveChanges();
            return user;
        }

        public User GetEmail(string email)
        {
            return _userContext.Users.FirstOrDefault(e => e.Email == email);
        }

        public User GetUserId(int id)
        {
            return _userContext.Users.FirstOrDefault(e => e.Id == id);
        }
    }
}
