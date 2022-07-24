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
        [ProducesResponseType(typeof(ApiResponse<List<User>>), 200)]
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


    }
}
