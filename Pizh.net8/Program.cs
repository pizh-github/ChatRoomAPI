
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

            // AutoMapper����ע��
            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
            AutoMapperConfig.RegisterMappings();

            // services����ע��
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
        ///// ע�����
        ///// </summary>
        ///// <param name="services"></param>
        //public void ConfigureServices(IServiceCollection services, IMapper mapper)
        //{
        //    ///�����֤
        //    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //    .AddCookie(options =>
        //    {
        //        options.Cookie.Name = "LogonApp.Cookie";// ������֤Cookie������
        //        options.Cookie.HttpOnly = true;//ȷ��Cookieֻ��ͨ��HTTP���䣬����ͨ���ͻ��˽ű�����
        //        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);//������֤Cookie�Ĺ���ʱ��Ϊ60����
        //        options.LoginPath = "/Home/Login"; // ��¼ҳ��·��
        //        options.AccessDeniedPath = "/Home/AccessDenied"; // ���ʱ��ܾ�ҳ��·��
        //    });

        //    services.AddControllersWithViews();//����MVC

        //    services.AddSingleton<PGSql>();//���������ݿ����
        //    services.AddSingleton<UserService>(provider =>
        //    {
        //        var context = provider.GetService<PGSql>();
        //        return new UserService(new UserRepo(), mapper);
        //    });
        //}
    }
}
