using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using TCNumberInquiry.Core;
using TCNumberInquiry.Core.DataSources;
using TCNumberInquiry.Core.Entities;
using TCNumberInquiry.Module.Model;
namespace TCNumberInquiry.Module.Manager
{
    public class UserManagers : IUserManagers
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IUserDataSource _userDataSource;
        private readonly IKpsDataSource _kpsDataSource;
        public UserManagers(IDistributedCache distributedCache, IUserDataSource userDataSource, IKpsDataSource kpsDataSource)
        {
            this._distributedCache = distributedCache;
            this._userDataSource = userDataSource;
            this._kpsDataSource = kpsDataSource;
        }

        public async Task<User> GetUserById(long userId)
        {
            string cacheKey = $"GetUserById_{userId}";

            return await this._distributedCache.GetOrSetAsync(cacheKey, CacheExpiryStrategy.FavorRead, async () =>
            {
                var userModel = await this._userDataSource.GetByUsersId(userId);
                User user = new User(userModel.Id, userModel.IdentyNumber, userModel.FirstName, userModel.LastName, userModel.BirthDate);
                return user;
            });
        }

        public async Task<User> GetUserByIdentyNumber(long identyNumber)
        {
            string cacheKey = $"GetUserById_{identyNumber}";

            return await this._distributedCache.GetOrSetAsync(cacheKey, CacheExpiryStrategy.FavorRead, async () =>
            {
                var userModel = await this._userDataSource.GetUserByIdentyNumber(identyNumber);
                User user = new User(userModel.Id, userModel.IdentyNumber, userModel.FirstName, userModel.LastName, userModel.BirthDate);
                return user;
            });
        }

        public async Task<List<User>> GetAllUsers()
        {
            string cacheKey = $"GetAllUser_Data";

            return await this._distributedCache.GetOrSetAsync<List<User>>(cacheKey, CacheExpiryStrategy.FavorRead, async () =>
            {
                var userModel = await this._userDataSource.GetAllUsers();
                List<User> userList = new List<User>();
                foreach (var uModel in userModel)
                {
                    User user = new User(uModel.Id, uModel.IdentyNumber, uModel.FirstName, uModel.LastName, uModel.BirthDate);
                    userList.Add(user);
                }

                return userList;
            });
        }

        public async Task<bool> CheckTCIndentificationNumber(User numberRequest)
        {
            IdentificationNumberRequest identificationNumberRequest = new IdentificationNumberRequest();
            identificationNumberRequest.BirthDate = numberRequest.BirthDate.Year;
            identificationNumberRequest.FirstName = numberRequest.FirstName.ToUpper();
            identificationNumberRequest.LastName = numberRequest.LastName.ToUpper();
            identificationNumberRequest.IdentificationNumber = numberRequest.IdentyNumber;
            var userModel = await this._kpsDataSource.GetIdentificationNumberCheckAsync(identificationNumberRequest);

            return userModel.Body.TCKimlikNoDogrulaResult;
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
