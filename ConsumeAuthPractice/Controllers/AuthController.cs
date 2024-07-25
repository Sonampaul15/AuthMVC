using ConsumeAuthPractice.DTO;
using ConsumeAuthPractice.Repository;
using ConsumeAuthPractice.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ConsumeAuthPractice.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthRepo Repo;
        private readonly ITokenProvider TokenProvider;
        public AuthController(IAuthRepo repo, ITokenProvider tokenProvider)
        {
            Repo = repo;
            TokenProvider = tokenProvider;
        }

        [HttpGet]
        public async Task<IActionResult> RegisterUser()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text =StaticData.RoleAdmin,
                    Value = StaticData.RoleAdmin,
                },
                new SelectListItem
                {
                    Text =StaticData.RoleAccount,
                    Value = StaticData.RoleAccount,
                },
                new SelectListItem
                {
                    Text =StaticData.RoleCustomer,
                    Value = StaticData.RoleCustomer,
                },
                new SelectListItem
                {

                    Text =StaticData.RoleOperator,
                    Value = StaticData.RoleOperator,
                },
                new SelectListItem
                {
                    Text = StaticData.RoleTransport,
                    Value = StaticData.RoleTransport,

                },
            };
            ViewBag.RoleList = roleList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegiResponseDto regi)
        {
            ResponseDto responseDto = await Repo.RegisterByNameAsync(regi);
            ResponseDto responseRoleApi;
            if (responseDto != null && responseDto.Success)
            {
                if(string.IsNullOrEmpty(regi.Role))
                {
                    regi.Role = StaticData.RoleCustomer;
                }
                responseRoleApi=await Repo.AssignRoleAsync(regi);
                if (responseRoleApi != null && responseRoleApi.Success)
                {
                    TempData["success"] = "user registr successfully";
                    return RedirectToAction("Index","Home");
                }  
            }

            else
            {
                TempData["error"] = "error in registration";
            }

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text =StaticData.RoleAdmin,
                    Value = StaticData.RoleAdmin,
                },
                new SelectListItem
                {
                    Text =StaticData.RoleAccount,
                    Value = StaticData.RoleAccount,
                },
                new SelectListItem
                {
                    Text =StaticData.RoleCustomer,
                    Value = StaticData.RoleCustomer,
                },
                new SelectListItem
                {

                    Text =StaticData.RoleOperator,
                    Value = StaticData.RoleOperator,
                },
                new SelectListItem
                {
                    Text = StaticData.RoleTransport,
                    Value = StaticData.RoleTransport,

                },
            };
            ViewBag.RoleList = roleList;
            return View(regi);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogineRequestDto logineRequest)
        {
            ResponseDto responseDto = await Repo.LoginByNameAsync(logineRequest);
             if(responseDto != null && responseDto.Success)
             {
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
                SignInUser(loginResponseDto);
                TokenProvider.SetToken(loginResponseDto.Token);
                TempData["success"] = "user login successfully";
                return RedirectToAction("Index","Home");

             }
            else
            {
                TempData["error"] = "error in login";
                return View (logineRequest);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            TokenProvider.ClearToken();
            return RedirectToAction("Login","Auth");
        }

        private async Task SignInUser(LoginResponseDto loginResponse)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(loginResponse.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(x=> x.Type==JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(x=> x.Type== JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(x => x.ValueType == "role").Value));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);

        }
    }
}
