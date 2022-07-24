using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCNumberInquiry.Core.Entitiy;

namespace TCNumberInquiry.Core.DataSources
{
    public interface IUserDataSource
    {
        Task<Users> GetByUsersId(int UserId);
        Task<List<Users>> GetAllUsers();

        Task<long> InsertUsers(Users users);
        Task<int> UpdateUsers(Users users);
        Task<int> DeleteUsers(int usersid);
    }
}
