using JWT.Data;
using JWT.Interface;
using JWT.Models;
using JWT.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace JWT.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly JWTDbContext db;
		public UserRepository(JWTDbContext dbContext)
		{
			db = dbContext;
		}

		public async Task<JsonResult> CreateUser(User user)
		{
			try
			{
				await db.Users.AddAsync(user);
				int x = await db.SaveChangesAsync();
				return new JsonResult() 
				{
					isSuccess = true,
					payload = user
				};
			}
			catch (Exception ex)
			{
				return new JsonResult()
				{
					isSuccess = false,
					payload = ex.InnerException
				};
			}
		}

		public async Task<User> GetUserByEmail(string email)
		{
			return await db.Users.SingleOrDefaultAsync(x => x.Email == email);
		}
	}
}
