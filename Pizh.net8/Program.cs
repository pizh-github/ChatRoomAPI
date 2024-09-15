
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Pizh.NET8;
using Pizh.NET8.Extension;
using Pizh.NET8.Extentsion;
using Pizh.NET8.IService;
using Pizh.NET8.Repository;
using Pizh.NET8.Repository.Base;
using Pizh.NET8.Service;

namespace Pizh.net8
{
    public class Program
    {   

        public static void Main(string[] args)
        {
            //List<WebSocket> _lists;
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseServiceProviderFactory(factory: new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
            {
                //builder.RegisterModule<>();
                builder.RegisterModule<AutofacModuleRegister>();
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // AutoMapper依赖注入
            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
            AutoMapperConfig.RegisterMappings();

            // services依赖注入
            builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
            builder.Services.AddScoped(typeof(IBseService<,>), typeof(BaseService<,>));

            builder.Services.AddSingleton<ChatService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            // websocket
            app.MapGet("/ws", async (HttpContext context, ChatService chatService) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    await chatService.HandleWebSocketConnection(webSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Expected a WebSocket request");
                }
            });

            app.UseWebSockets();

            app.Run();

            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        ///// <summary>
        ///// 注册服务
        ///// </summary>
        ///// <param name="services"></param>
        //public void ConfigureServices(IServiceCollection services, IMapper mapper)
        //{
        //    ///身份验证
        //    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //    .AddCookie(options =>
        //    {
        //        options.Cookie.Name = "LogonApp.Cookie";// 设置认证Cookie的名称
        //        options.Cookie.HttpOnly = true;//确保Cookie只能通过HTTP传输，不能通过客户端脚本访问
        //        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);//设置认证Cookie的过期时间为60分钟
        //        options.LoginPath = "/Home/Login"; // 登录页面路径
        //        options.AccessDeniedPath = "/Home/AccessDenied"; // 访问被拒绝页面路径
        //    });

        //    services.AddControllersWithViews();//启用MVC

        //    services.AddSingleton<PGSql>();//单例，数据库访问
        //    services.AddSingleton<UserService>(provider =>
        //    {
        //        var context = provider.GetService<PGSql>();
        //        return new UserService(new UserRepo(), mapper);
        //    });
        //}
    }
}
