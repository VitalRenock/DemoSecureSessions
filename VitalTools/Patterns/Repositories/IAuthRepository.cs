using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTools.Patterns.Repositories
{
	/// <summary>
	/// Interface générique pour la création d'un 'Repository' pour les différents services d'Authentification.
	/// </summary>
	/// <typeparam name="T">Type d'utilisateur désiré pour les transactions effectuées.</typeparam>
	public interface IAuthRepository<TUser>
	{
		bool Register(TUser user);
		TUser Login(string email, string password);
	}
}
