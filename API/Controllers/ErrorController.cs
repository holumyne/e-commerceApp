using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("errors/{code}")] //we use this  to override the default placeholder "/api", while "code" is the error code.
    [ApiExplorerSettings(IgnoreApi = true)] //this will enable swagger to ignore the error generated as a result of this controller. Also, it will not display in swagger interface when we launch the app
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
