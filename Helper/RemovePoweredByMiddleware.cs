using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InterviewTest.Helper
{
    public class RemovePoweredByMiddleware
    {
        private readonly RequestDelegate _next;

        public RemovePoweredByMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Remove("X-Powered-By");
            await _next(context);
        }
    }
    public static class RemovePoweredByExtensions
    {
        public static IApplicationBuilder UseRemovePoweredBy(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RemovePoweredByMiddleware>();
        }
    }
}
