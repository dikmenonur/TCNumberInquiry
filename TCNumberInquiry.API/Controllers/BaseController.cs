using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TCNumberInquiry.Module.Model;

namespace TCNumberInquiry.API.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var apiResponse = new ApiResponse();
                apiResponse.ValidationErrors = context.ModelState.Keys.Select(key =>
                    new ValidationErrorMessage
                    {
                        Key = key,
                        Errors = context.ModelState[key].Errors.Select(x => x.ErrorMessage).ToList()
                    }).ToList();
                context.Result = this.StatusCode(400, apiResponse);
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }

        protected IActionResult ApiResponse<T>(T data, string message = null)
        {

            return this.StatusCode(200, new ApiResponse<T>(data, message));
        }

        protected IActionResult ApiErrorResponse(Exception exception = null, string message = null)
        {
            return this.StatusCode(500, new ApiResponse(message));
        }
    }
}
