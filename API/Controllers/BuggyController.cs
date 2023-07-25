using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "secret stuff";
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _context.Products.Find(42);

            if (thing == null)
                return NotFound(new ApiResponse(404));

            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _context.Products.Find(42);

            var thingToReturn = thing.ToString(); //var thing is null becos we dont hv product with id=42, hence it will throw an error

            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400)); //400 response error is generated
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id) //here, will generate validation error by passing a string instead of id
        {
            return Ok();
        }
    }
}
