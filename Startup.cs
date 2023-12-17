using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using webapi;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Konfiguracja połączenia do bazy danych z pliku appsettings.json
        var connectionString = "Server=DESKTOP-1PMAN7K\\SQLEXPRESS;Database=webapi;Trusted_Connection=True;TrustServerCertificate=True;"; // Dodaj prawdziwe połączenie do bazy danych

        // Dodaj DbContext do kontenera zależności
        services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Dodaj obsługę Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
        });

        services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
            c.RoutePrefix = string.Empty; // Swagger UI dostępne pod główną ścieżką
        });
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}
}
