using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCNumberInquiry.Web.Model;
using TCNumberInquiry.Web.Services;

namespace TCNumberInquiry.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserServices _userService;
        public IndexModel(ILogger<IndexModel> logger, UserServices userService)
        {
            _logger = logger;
            this._userService = userService;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            var userModelData = await this._userService.GetAllUsersAsync();
            return userModelData.UserModel;
        }

        public async Task<UserModel> GetUserByIdAsync(long userId)
        {
            return await this._userService.GetUserByIdAsync(userId);
        }
    }
}
