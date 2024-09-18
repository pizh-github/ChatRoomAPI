using Pizh.ChatRoom.Hubs;
using Pizh.ChatRoom.Service;
using SqlSugar;

namespace Pizh.ChatRoom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add SignalR to the container.
            builder.Services.AddSignalR();

            // Add Session Server
            builder.Services.AddSession();

            // Add SqlSugar Services
            builder.Services.AddTransient<ISqlSugarClient>(provider =>
            {
                SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = builder.Configuration.GetSection("DBConnect").Value,
                    DbType = DbType.PostgreSQL,
                    IsAutoCloseConnection = true
                });
                return db;
            });

            // Add UserService
            builder.Services.AddTransient<IUserService, UserService>();

            // Add Swagger
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Use ChatHub
            app.MapHub<ChatHub>("/chatHub");

            // Use Session
            app.UseSession();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Run();
        }
    }
}