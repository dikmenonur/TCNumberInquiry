using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TCNumberInquiry.Core.Entities;

namespace TCNumberInquiry.Core.DataSources
{
    public class UserDataSource : IUserDataSource
    {
        private UserDbContext UserDbContent;

        public UserDataSource(UserDbContext userDbContent)
        {
            this.UserDbContent = userDbContent;
        }

        public async Task<Users> GetByUsersId(long userId)
        {
            return await this.UserDbContent.Users.FirstOrDefaultAsync(t => t.Id == userId && t.IsActive);
        }

        public async Task<Users> GetUserByIdentyNumber(long identyNumber)
        {
            return await this.UserDbContent.Users.FirstOrDefaultAsync(t => t.IdentyNumber == identyNumber);
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await this.UserDbContent.Users.Where(t => t.IsActive).ToListAsync();
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

        public async Task<int> DeleteUsers(long userId)
        {
            var entity = await this.UserDbContent.Users.FirstOrDefaultAsync(a => a.Id == userId);
            entity.IsActive = false;
            this.UserDbContent.Update<Users>(entity);
            return await this.UserDbContent.SaveChangesAsync();
        }
    }
}
