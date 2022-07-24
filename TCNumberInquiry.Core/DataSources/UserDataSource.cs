using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TCNumberInquiry.Core.Entitiy;

namespace TCNumberInquiry.Core.DataSources
{
    public class UserDataSource : IUserDataSource
    {
        private UserDbContext UserDbContent;

        public UserDataSource(UserDbContext userDbContent)
        {
            this.UserDbContent = userDbContent;
        }

        public async Task<Users> GetByUsersId(int UserId)
        {
            return await this.UserDbContent.Users.FirstOrDefaultAsync(t => t.Id == UserId);
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await this.UserDbContent.Users.ToListAsync();
        }

        public async Task<long> InsertUsers(Users users)
        {
            this.UserDbContent.Add<Users>(users);
            await this.UserDbContent.SaveChangesAsync();
            return users.Id;
        }

        public async Task<int> UpdateUsers(Users users)
        {
            this.UserDbContent.Update<Users>(users);
            var requestAsync = await this.UserDbContent.SaveChangesAsync();
            return requestAsync;
        }

        public async Task<int> DeleteUsers(int usersid)
        {
            var entity = this.UserDbContent.Users.FirstOrDefaultAsync(a => a.Id == usersid);
            this.UserDbContent.Remove(entity);
            return await this.UserDbContent.SaveChangesAsync();
        }
    }
}
