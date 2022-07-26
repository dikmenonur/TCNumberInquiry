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

        [BindProperty]
        public List<UserModel> UserModelList { get; set; }
        public async Task OnGetAsync()
        {
            var userModelData = await this._userService.GetAllUsersAsync();
            this.UserModelList = userModelData.UserModel;
        }

        public async Task<IActionResult> OnGetEditAsync(long userId)
        {
             await this._userService.GetUserByIdAsync(userId);
             return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await this._userService.DeleteUserAsync(id);
            return RedirectToPage();
        }
    }
}
