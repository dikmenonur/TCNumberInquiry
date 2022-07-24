using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCNumberInquiry.Module.Model;

namespace TCNumberInquiry.Module.Manager
{
    public class UserManagers : IUserManagers
    {
        public UserManagers()
        {

        }

        public Task<User> GetUserById(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<long> InsertUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<long> DeleteUser(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<long> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
