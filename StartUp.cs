using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mtg_app.Data;
using Library.Seeders;

namespace mtg_app
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => {
                    options.SignIn.RequireConfirmedAccount = true;
                    
                    options.Password.RequireDigit = true; 
                    options.Password.RequiredLength = 8; 
                    options.Password.RequireNonAlphanumeric = false; 
                    options.Password.RequireUppercase = true; 
                    options.Password.RequireLowercase = false; 
                    options.Password.RequiredUniqueChars = 6;
                })
                
                .AddRoles<IdentityRole>() 
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
           services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            RunSeeders(serviceProvider);
            
        }
        private void RunSeeders(IServiceProvider serviceProvider){
            //  -- AuthSeeder
            AuthSeeder authSeeder = new AuthSeeder(serviceProvider);
            authSeeder.Run();
        }        
    }
}

