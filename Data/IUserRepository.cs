using AuthTestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTestApi.Data
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetEmail(string email);

        User GetUserId(int id);
    }
}
