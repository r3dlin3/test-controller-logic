using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApiSample.DataAccess;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<WebApiSample.Api.Startup>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Create a new service provider.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Add a database context (ApplicationDbContext) using an in-memory 
            // database for testing.
            services.AddDbContext<ProductContext>(opt =>
            {
                opt.UseInMemoryDatabase("ProductInventory");
                opt.UseInternalServiceProvider(serviceProvider);
            });


            // Build the service provider.
            var sp = services.BuildServiceProvider();

            #region snippet_SetCompatibilityVersion
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            #endregion
            // Create a scope to obtain a reference to the database
            // context (ApplicationDbContext).
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ProductContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();


                // Ensure the database is created.
                db.Database.EnsureCreated();

            }
        });
    }
}