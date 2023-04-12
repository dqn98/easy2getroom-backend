using BE.Application.Helpers;
using BE.Application.Implementations;
using BE.Application.Interfaces;
using BE.Data.EF;
using BE.Data.EF.Repositories;
using BE.Data.Entities;
using BE.Data.IRepositories;
using BE.Helpers;
using BE.Hubs;
using BE.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;
using System.Text;

namespace BE
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
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddMvc();
            services.AddCors(o => o.AddPolicy("E2GRPolicy", builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                       .WithOrigins("http://localhost:4200", "http://localhost:8200");
            }));

            services.AddSignalR();
            //Config identity
            services.Configure<IdentityOptions>(options =>
            {
                //Password setting
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                //Lockout setting
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                //User setting
                options.User.RequireUniqueEmail = true;
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.Configure<GoogleAuthenticationSetting>(Configuration.GetSection("Authentication").GetSection("Google"));

            //Jwt Authentication

            var key = Encoding.UTF8.GetBytes(Configuration["JWTSetting:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsPrincipalFactory>();

            services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddTransient(typeof(IRepository<,>), typeof(EFRepository<,>));

            services.AddScoped<UserManager<User>, UserManager<User>>();
            services.AddScoped<RoleManager<Role>, RoleManager<Role>>();

            services.AddTransient<DbInitializer>();

            //Declare Repository
            services.AddScoped<IPropertyCategoryRepository, PropertyCategoryRepository>();
            services.AddTransient<IFunctionRepository, FunctionRepository>();
            services.AddTransient<IPropertyRepository, PropertyRepository>();
            services.AddTransient<IRentalTypeRepository, RentalTypeRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IDistrictRepository, DistrictRepository>();
            services.AddTransient<IWardsRepository, WardsRepository>();
            services.AddTransient<IPropertyImageRepository, PropertyImageRepository>();
            services.AddTransient<INewsRepository, NewsRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IMessageImageRepository, MessageImageRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IFeatureRepository, FeatureRepository>();
            services.AddTransient<IFavoriteRepository, FavoriteRepository>();
            services.AddTransient<IRatingRepository, RatingRepository>();
            services.AddTransient<IPropertyFeatureRepository, PropertyFeatureRepository>();
            services.AddTransient<IAnnouncementRepository, AnnouncementRepository>();
            services.AddTransient<ILoggingRepository, LoggingRepository>();
            services.AddTransient<ILogTypeRepository, LogTypeRepository>();
            //
            //Delcare Service
            services.AddTransient<IPropertyCategoryService, PropertyCategoryService>();
            services.AddTransient<IFunctionService, FunctionService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IRentalTypeService, RentalTypeService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IDistrictService, DistrictService>();
            services.AddTransient<IWardsService, WardsService>();
            services.AddTransient<IPropertyImageService, PropertyImageService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IChartService, ChartService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IFeatureService, FeatureService>();
            services.AddTransient<IClientPropertyService, ClientPropertyService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IClientUserService, ClientUserService>();
            services.AddTransient<IAnnouncementService, AnnouncementService>();
            services.AddTransient<ILoggingService, LoggingService>();
            services.AddTransient<ILogTypeService, LogTypeService>();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }
            app.UseHttpsRedirection();
            app.UseCors("E2GRPolicy");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<LoggingHub>("/LoggingHub");
                endpoints.MapHub<ChatHub>("/ChatHub");
                endpoints.MapHub<NotifyHub>("/NotifyHub");
            });
        }
    }
}