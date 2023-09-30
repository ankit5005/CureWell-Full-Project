using SOTI.CureWell.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.CureWell.DAL.Interfaces
{
    public interface IUser
    {
        List<User> GetAllUsers();

        bool AddUser(User user);
        User GetUserById(int userId);
        Task<User> ValidateUserAsync(string emailId, string password);

    }
}
