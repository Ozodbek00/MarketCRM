using Market.Service.Exceptions;

namespace Market.Api.Middlewares
{
    public class MarketMiddleware
    {
        private readonly RequestDelegate next;

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
                throw new CustomException(404, "Not found!");
            }
        }
    }
}
