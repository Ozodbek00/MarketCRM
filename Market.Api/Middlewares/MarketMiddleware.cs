using Market.Service.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Market.Api.Middlewares
{
    public class MarketMiddleware
    {
        public RequestDelegate next;
        private readonly ILogger<MarketMiddleware> logger;

        public MarketMiddleware(RequestDelegate next, ILogger<MarketMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }


            catch (CustomException ex)
            {
                throw new CustomException(404, "Not found!");
            }

            catch(Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            logger.LogError(ex.ToString());
            var errorMessageObject = new { Message = ex.Message, Code = "system_error" };

            var errorMessage = JsonConvert.SerializeObject(errorMessageObject);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(errorMessage);
        }
    }
}
