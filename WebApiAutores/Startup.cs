using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace WebApiAutores
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureService(IServiceCollection services) {
            // Add services to the container.
            // Ignore infinite loops in entities (ReferenceHandler.IgnoreCycles) 

            services.AddControllers().AddJsonOptions(options => 
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            //Sql server database configuration
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { 
                endpoints.MapControllers();
            });

        }
    }
}
