using JWT.Data;
using JWT.Models;
using JWT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Interface
{
	public interface IUserRepository
	{
		Task<JsonResult> CreateUser(User user);
		Task<User> GetUserByEmail(string email);
	}
}
