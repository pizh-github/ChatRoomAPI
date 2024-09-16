using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Pizh.NET8.Repository.Base;
using Pizh.NET8.Repository;
using Pizh.NET8.Service;

namespace Pizh.NET8
{
    public class Startup
    {
        private readonly IMapper _mapper;
        /// <summary>
        /// 读取配置信息
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            _mapper = mapper;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            ///身份验证
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "LogonApp.Cookie";// 设置认证Cookie的名称
                options.Cookie.HttpOnly = true;//确保Cookie只能通过HTTP传输，不能通过客户端脚本访问
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);//设置认证Cookie的过期时间为60分钟
                options.LoginPath = "/api/User/Login"; // 登录页面路径
                options.AccessDeniedPath = "/Home/AccessDenied"; // 访问被拒绝页面路径
            });

            services.AddControllersWithViews();//启用MVC

            services.AddSingleton<PGSql>();//单例，数据库访问
            services.AddSingleton<UserService>(provider =>
            {
                var context = provider.GetService<PGSql>();
                return new UserService(new UserRepo(), _mapper);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //异常处理
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //路由
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=UserControlller}/{action=Login}/{id?}");
            });
        }
    }
}
