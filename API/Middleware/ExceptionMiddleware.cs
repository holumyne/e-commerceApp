using API.Errors;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)//RequestDelegate is a function that can process http request
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) //its an http request delegate
        {
            try
            {
                await _next(context); //if the is no exception this middleware should move to the next middleware
            }
            catch (Exception ex) // if there is exception catch it here
            {
                _logger.LogError(ex, ex.Message); //the error(ex) and the error message(ex.Message) will be output to our logging system which is the console
                context.Response.ContentType = "application/json";  //responses from the context in json
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; //converting to camel case

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
