using log4net;
using Market.Service.Exceptions;

namespace Market.Api.Middlewares
{
    public class MarketMiddleware
    {
        public RequestDelegate next;
        private readonly ILog logger = LogManager.GetLogger(typeof(MarketMiddleware));

        public MarketMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }


            catch (CustomException ex)
            {
                await HandleException(context, ex.Code, ex.Message);
            }

            catch(Exception ex)
            {
                logger.Error(ex.ToString());
                await HandleException(context, 500, ex.Message);
            }
        }

        private async Task HandleException(HttpContext context, int code, string message)
        {
            context.Response.StatusCode = code;

            await context.Response.WriteAsJsonAsync(new
            {
                Code = code,
                Message = message
            });
        }
    }
}
