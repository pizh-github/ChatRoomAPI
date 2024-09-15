using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Pizh.NET8.Service;
using System.Security.Claims;
using Pizh.NET8.Model;

namespace Pizh.NET8.Controllers
{
    public class UserController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _userService.Login(username, password);//查询用户

            if (user != null)
            {
                // 创建用户的声明信息
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                // 创建身份信息
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // 设置认证Cookie的属性
                var authProperties = new AuthenticationProperties
                {

                };

                // 使用HttpContext.SignInAsync方法设置用户的认证信息
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("chatroom");//跳转页面
            }

            ModelState.AddModelError(string.Empty, "登录失败，请检查用户名和密码！");

            return View();
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(UserVo userVo)
        {
            try
            {
                _userService.Register(userVo);
            }
            catch (Exception ex) {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // 清除当前用户的身份认证信息
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // 清除任何之前登录时设置的临时信息
            TempData.Clear();

            return RedirectToAction("Login", "Account");
        }
    }
}
