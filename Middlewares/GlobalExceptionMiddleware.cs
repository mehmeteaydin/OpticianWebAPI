using System.Net;
using System.Text.Json;
using Serilog;

namespace OpticianWebAPI.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // İsteği devam ettir
                await _next(context);
            }
            catch (Exception ex)
            {
                // Hata olursa yakala ve logla
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Loglama İşlemi (Hatanın detaylarını kaydeder)
            Log.Error(exception, "Sunucuda beklenmedik bir hata oluştu! Hata Mesajı: {ErrorMessage}", exception.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new 
            { 
                StatusCode = context.Response.StatusCode, 
                Message = "Sunucu hatası oluştu. Lütfen sistem yöneticisi ile görüşün.",
                Detailed = exception.Message // Geliştirme aşamasında açık bırakabilirsin
            });

            return context.Response.WriteAsync(result);
        }
    }
}