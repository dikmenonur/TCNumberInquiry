using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using TCNumberInquiry.Core.DataSources;
using TCNumberInquiry.Module.Model;

namespace TCNumberInquiry.Module.Manager
{
    public class UserManagers : IUserManagers
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IUserDataSource _userDataSource;
        public UserManagers(IDistributedCache distributedCache, IUserDataSource userDataSource)
        {
            this._distributedCache = distributedCache;
            this._userDataSource = userDataSource;
        }

        public async Task<User> GetUserById(long userId)
        {
            var userModel = await this._userDataSource.GetByUsersId(userId);
            User user = new User(userModel.Id, userModel.IdentyNumber, userModel.FirstName, userModel.LastName, userModel.BirthDate);
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var userModel = await this._userDataSource.GetAllUsers();
            List<User> userList = new List<User>();
            foreach (var uModel in userModel)
            {
                User user = new User(uModel.Id, uModel.IdentyNumber, uModel.FirstName, uModel.LastName, uModel.BirthDate);
                userList.Add(user);
            }

            return userList;
        }

        public async Task<long> InsertUser(User user)
        {
            var userModel = await this._userDataSource.InsertUsers(user.GetUsers());
            return userModel;
        }

        public async Task<long> DeleteUser(long userId)
        {
            var userModel = await this._userDataSource.DeleteUsers(userId);
            return userModel;
        }

        public async Task<long> UpdateUser(User user)
        {
            var userModel = await this._userDataSource.UpdateUsers(user.GetUsers());
            return userModel;
        }
    }
}
