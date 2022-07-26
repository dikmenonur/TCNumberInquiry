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
    public class CreateModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserServices _userService;
        public CreateModel(ILogger<IndexModel> logger, UserServices userService)
        {
            _logger = logger;
            this._userService = userService;
        }

        [BindProperty]
        public UserModel UserModel { get; set; }


        public async Task<IActionResult> OnPostAsync(UserModel userModel)
        {
            var apiResponse = await this._userService.InsertUserAsync(userModel);

            if (apiResponse == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
