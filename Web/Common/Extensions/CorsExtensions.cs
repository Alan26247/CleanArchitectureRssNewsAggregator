namespace Web.Common.Extensions;

public static class CorsExtensions
{
    public static void AddCorsPolitics(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllPolicy",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();

                    //builder
                    //.AllowAnyMethod()
                    //.AllowAnyHeader()
                    //.SetIsOriginAllowed(origin => true)
                    //.AllowAnyOrigin();
                    //.AllowCredentials();
                });
        });
    }
}
