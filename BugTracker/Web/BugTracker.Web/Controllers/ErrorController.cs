namespace BugTracker.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [HttpGet("/error")]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                this.HttpContext.Response.StatusCode = statusCode.Value;
            }

            if (statusCode.Value == 500)
            {
                return this.View("AppError");
            }
            else
            {
                return this.View("PageNotFound");
            }
        }

        public IActionResult AppError()
        {
            return this.View();
        }

        public IActionResult PageNotFound()
        {
            return this.View();
        }
    }
}
