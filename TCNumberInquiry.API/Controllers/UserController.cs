using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCNumberInquiry.Module.Manager;
using TCNumberInquiry.Module.Model;

namespace TCNumberInquiry.API.Controllers
{
    [ApiController]
    [Route("/api/public/v1/users")]
    public class UserController : BaseController
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserManagers _userManagers;
        public UserController(ILogger<UserController> logger, IUserManagers userManagers)
        {
            _logger = logger;
            this._userManagers = userManagers;
        }

        [Route("GetAllUser")]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<User>>), 200)]
        public async Task<IActionResult> GetAllUserModel()
        {
            try
            {
                var users = await this._userManagers.GetAllUsers();
                return this.ApiResponse<List<User>>(users);

            }
            catch (Exception ex)
            {
                return this.ApiErrorResponse(ex, "Beklenmedik hata meydana geldi.");
            }
        }

        [Route("GetUserById")]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<User>), 200)]
        public async Task<IActionResult> GetUserByIdModel(long userId)
        {
            try
            {
                var users = await this._userManagers.GetUserById(userId);
                return this.ApiResponse<User>(users);

            }
            catch (Exception ex)
            {
                return this.ApiErrorResponse(ex, "Beklenmedik hata meydana geldi.");
            }
        }

        [Route("InsertUser")]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<long>), 200)]
        public async Task<IActionResult> InsertUser(User user)
        {
            try
            {
                var usersCheck = await this._userManagers.CheckTCIndentificationNumber(user);

                if (!usersCheck)
                {
                    return this.ApiResponse("Doğru bir TC Numarası giriniz. Böyle bir numara bulunamadı.");
                }

                var usersIdentyNumberCheck = await this._userManagers.GetUserByIdentyNumber(user.IdentyNumber);
                
                if (usersIdentyNumberCheck != null)
                {
                    return this.ApiResponse(usersIdentyNumberCheck, "Sistemde böyle bir kullanıcı mevcuttur.");
                }

                var users = await this._userManagers.InsertUser(user);
                return this.ApiResponse(users);


            }
            catch (Exception ex)
            {
                return this.ApiErrorResponse(ex, "Beklenmedik hata meydana geldi.");
            }
        }

        [Route("DeleteUser")]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<long>), 200)]
        public async Task<IActionResult> DeleteUser(long userId)
        {
            try
            {
                var userFound = await this._userManagers.GetUserById(userId);

                if (userFound != null)
                {
                    return this.ApiResponse(userFound, "Sistemde böyle bir kullanıcı mevcut değildir.");
                }

                var users = await this._userManagers.DeleteUser(userId);
                return this.ApiResponse(users);


            }
            catch (Exception ex)
            {
                return this.ApiErrorResponse(ex, "Beklenmedik hata meydana geldi.");
            }
        }

        [Route("CheckTCIndentificationNumber")]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<long>), 200)]
        public async Task<IActionResult> CheckTCIndentificationNumber(User user)
        {
            try
            {
                var users = await this._userManagers.CheckTCIndentificationNumber(user);
                return this.ApiResponse(users);

            }
            catch (Exception ex)
            {
                return this.ApiErrorResponse(ex, "Beklenmedik hata meydana geldi.");
            }
        }


    }
}
