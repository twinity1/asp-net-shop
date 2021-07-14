using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using DyShop.Data;
using DyShop.Data.Entities;
using DyShop.Data.Repositories;
using DyShop.Data.Repositories.Cart;
using DyShop.Helpers.MapperProfiles;
using DyShop.Services;
using DyShop.Services.Breadcrumbs;
using DyShop.Services.Mail;
using GridMvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp.Web.DependencyInjection;
using Vereyon.Web;

namespace DyShop
{
    public class Startup
    {
        public const string AdminArea = "admin";
        public const string ShopArea = "shop";
        
        private readonly IWebHostEnvironment _environment;
         
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                options.UseLoggerFactory(LoggerFactory.Create(b => b.AddConsole()));
            });

            services.AddImageSharp();
            services.AddWebOptimizer();
            services.AddGridMvc();
            services.AddFlashMessage();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddAutoMapper(typeof(Startup));

            services.Configure<SmtpConfig>(Configuration.GetSection("SMTP"));

            services
                .AddScoped<MailService>()
                .AddScoped<ProductParameterAdminService>()
                .AddScoped<ProductParameterRepository>()
                .AddScoped<OrderAdminService>()
                .AddScoped<BreadcrumbsService>()
                .AddScoped<ShopProfile>()
                .AddScoped<AdminProfile>()
                .AddScoped<OrderRepository>()
                .AddScoped<CheckoutService>()
                .AddScoped<PaymentRepository>()
                .AddScoped<DeliveryRepository>()
                .AddScoped<CartRepository>()
                .AddScoped<ProductListingService>()
                .AddScoped<ProductRepository>()
                .AddScoped<ProductAdminService>()
                .AddScoped<ProductCategoryRepository>()
                .AddScoped<ProductCategoryAdminService>()
                .AddScoped<SlugService>()
                .AddScoped<FileHandlerService>();

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            var mvcBuilder = services
                .AddControllersWithViews()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                })
                .AddRazorOptions(options =>
                {
                    options.ViewLocationFormats.Add("/Areas/Shop/{0}.cshtml");
                });

            if (_environment.IsDevelopment())
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }
            
            //needs to be the last
            services.AddRazorTemplating();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext, MailService mailService)
        {
            dbContext.Database.Migrate();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // app.UseHttpsRedirection();

            app.UseWebOptimizer();
            app.UseImageSharp();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("product", "p/{slug}", new
                {
                    area = ShopArea,
                    controller = "Product",
                    action = "Detail",
                });
                
                endpoints.MapControllerRoute("product", "produkty", new
                {
                    area = ShopArea,
                    controller = "Product",
                    action = "Index",
                });
                
                endpoints.MapAreaControllerRoute(AdminArea, AdminArea, "Admin/{controller=Product}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(ShopArea, ShopArea, "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapRazorPages();
            });
        }
    }
}