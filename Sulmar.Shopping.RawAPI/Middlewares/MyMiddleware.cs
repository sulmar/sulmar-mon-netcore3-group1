using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Sulmar.Shopping.RawAPI.Middlewares
{
    public class MyMiddleware
    {
        private readonly RequestDelegate next;

        private readonly IFoo foo;

        public MyMiddleware(RequestDelegate next, IFoo foo)
        {
            this.next = next;
            this.foo = foo;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync(foo.Get());
            await next.Invoke(context);
        }
    }
}
