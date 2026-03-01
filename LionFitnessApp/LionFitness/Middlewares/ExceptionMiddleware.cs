using System.Net;
using LionFitness.Application.Exceptions;


namespace LionFitness.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (DuplicateCpfException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Conflict; // 409
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    error = "duplicate_cpf",
                    message = ex.Message
                });
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    error = "internal_error",
                    message = "An unexpected error occurred."
                });
            }
        }
    }
}
