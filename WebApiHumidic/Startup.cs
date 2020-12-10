using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Azure;

namespace WebApiHumidic
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                // 1 st policy --> allow specific origin
                options.AddPolicy("AllowSpecificOrigins",
                builder =>
                {
                    builder.WithOrigins("https://azure.microsoft.com/").AllowAnyHeader().AllowAnyMethod();
                });

                // 2nd Policy --> allow any origins -- Now you have to public your services
                options.AddPolicy("AllowAnyOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });

                // 3rd Policy --> allow only Get Put Method 
                options.AddPolicy("AllowAnyOriginsGetPUT",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET", "POST");
                });
            });
            //      services.AddDbContext<TodoContext>(opt =>
            //opt.UseInMemoryDatabase("TodoList"));
            services.AddControllers();
            services.AddSwaggerGen();
            
                services.AddAzureClients(builder =>
                {
                    builder.AddBlobServiceClient(Configuration["ConnectionStrings:conn"]);
                });
           
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors("AllowAnyOrigins");
            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
