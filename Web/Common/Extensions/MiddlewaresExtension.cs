using Web.Common.Middlewares;

namespace Web.Common.Extensions
{
    public static class MiddlewaresExtension
    {
        public static void UseMiddlewares(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseCors("AllowAllPolicy");

            app.UseSwagger();
            app.UseAppSwagger();

            app.UseMiddleware<InterceptorExceptionsMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
