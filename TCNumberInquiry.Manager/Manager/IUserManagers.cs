using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCNumberInquiry.Module.Model;

namespace TCNumberInquiry.Module.Manager
{
    public interface IUserManagers
    {
        Task<User> GetUserById(long userId);
        Task<User> GetUserByIdentyNumber(long identyNumber);
        Task<List<User>> GetAllUsers();
        Task<long> InsertUser(User user);
        Task<bool> CheckTCIndentificationNumber(User user);
        Task<long> DeleteUser(long userId);
        Task<long> UpdateUser(User user);
    }
}
