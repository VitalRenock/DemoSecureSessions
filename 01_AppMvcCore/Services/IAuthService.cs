using _01_AppMvcCore.Models;

namespace _01_AppMvcCore.Services
{
	public interface IAuthService
	{
		User Login(string email, string password);
		bool Register(User user);
	}
}