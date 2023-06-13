using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json;
using System.Net;
using TorontoCHA.Controllers;
using TorontoCHA.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace TorontoCHA.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration Configuration;

        public AccountController(ILogger<AccountController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateTchaAccount(TorontoCHA.Entity.TchaAccount tchaAccount)
        {
            var createAccountURL = Configuration["API_URL"] + "TchaAccount/CreateTchaAccount";
            using (var client = new HttpClient())
            {
                var response= await client.PostAsJsonAsync<TchaAccount>(createAccountURL, tchaAccount);
                if(response.StatusCode != HttpStatusCode.OK)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    ViewData["UserEmailFlag"] = message;

                    return View(tchaAccount);
                }
            }
            return RedirectToAction("LoginTchaAccount", "Account", null);
        }


        [HttpPost]
        public async Task<IActionResult> LoginTchaAccount(string username, string password)
        {
            
            var loginAccountURL = Configuration["API_URL"]+ "TchaAccount/LoginTchaAccount";
            var urlWithParams = loginAccountURL + "/?username=" + username + "&password=" + password;
            using (var client = new HttpClient())
            {
                var httpMessage = await client.PostAsJsonAsync<TchaAccount>(urlWithParams, null);
                var responseContent = await httpMessage.Content.ReadAsStringAsync();
                var tchaAccount = JsonConvert.DeserializeObject<TchaAccount>(responseContent);


                if (tchaAccount != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, tchaAccount.AccountId.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, tchaAccount.Username),
                        
                        
                        new Claim(ClaimTypes.Role, "Administrator"),
					};

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                    };
                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                    return RedirectToAction("AccountDashboard", "Account", tchaAccount); // ("~/Views/Account/AccountDashboard.cshtml", tchaAccount);
                }
                else
                {
                    ViewData["LoginFlag"] = "Username or Password is Incorrect";
                    ViewBag.username= username; ViewBag.password= password;
                    return View();
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateTchaAccount()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogoutTchaAccount()
        {
            await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", null);
        }

        [HttpGet]
        public async Task<IActionResult> LoginTchaAccount()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AccountDashboard()
        {
            var AccountId = HttpContext.User.Identity.Name;
            var getAccountURL = Configuration["API_URL"] + "TchaAccount/GetAccountById";
            var urlWithParams = getAccountURL + "?accountId=" + AccountId.ToString();

            using (var client = new HttpClient())
            {
                var httpMessage = await client.GetAsync(urlWithParams);
                var responseContent = await httpMessage.Content.ReadAsStringAsync();
                var tchaAccount = JsonConvert.DeserializeObject<TchaAccount>(responseContent);

                return View(tchaAccount);
            }
            
        }

        
        
        
        
        
        
  //      [HttpGet] 
  //      public async Task<TchaAccount> GetAccountAsync(int accountId)
  //      {
  //         var AccountId =  HttpContext.User.Identity.Name;
  //         var getAccountURL = Configuration["API_URL"] + "TchaAccount/GetAccountById" + accountId.ToString();
		//	var urlWithParams = getAccountURL + "?accountId=" + accountId.ToString();

  //          using (var client = new HttpClient())
  //          {
  //              var httpMessage = await client.PostAsJsonAsync<TchaAccount>(urlWithParams, null);

  //              var responseContent = await httpMessage.Content.ReadAsStringAsync();
  //              var tchaAccount = JsonConvert.DeserializeObject<TchaAccount>(responseContent);
  //              return View(tchaAccount);
  //          }
				
		//}

    }
}
