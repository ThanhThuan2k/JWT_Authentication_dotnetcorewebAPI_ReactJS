using JWT.Interface;
using JWT.Models;
using JWT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Controllers
{
	[Route(template: "api")]
	[ApiController]
	public class AuthController : Controller
	{
		private readonly IUserRepository _IUserRepository;
		public AuthController(IUserRepository userrepository)
		{
			_IUserRepository = userrepository;
		}

		[HttpPost(template: "register")]
		public async Task<IActionResult> Hello(UserRegisterViewModel newUser)
		{
			var user = new User()
			{
				Name = newUser.Name,
				Email = newUser.Email,
				Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password)
			};
			return Created(uri: "success", value: await _IUserRepository.CreateUser(user));
		}

		[HttpPost(template: "login")]
		public async Task<IActionResult> Login(LoginViewModel login)
		{
			var user = await _IUserRepository.GetUserByEmail(login.Email);
			if(user == null)
			{
				return Ok(new JWT.Data.JsonResult()
				{
					isSuccess = false,
					payload = login.Email,
					error = new
					{
						errorCode = 111,
						errorMessage = "Tài khoản không tồn tại"
					}
				});
			}
			else
			{
				if(BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
				{
					return Ok(new JWT.Data.JsonResult()
					{
						isSuccess = true,
						payload = user.Name,
						error = null
					});
				}
				else
				{
					return Ok(new JWT.Data.JsonResult()
					{
						isSuccess = false,
						payload = null,
						error = new
						{
							errorCode = 123,
							errorMessage = "Sai mật khẩu"
						}
					});
				}
			}
		}
	}
}
