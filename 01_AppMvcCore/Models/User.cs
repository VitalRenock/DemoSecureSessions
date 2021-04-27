using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01_AppMvcCore.Models
{
	public class User
	{
		#region Properties

		public int Id { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; } 

		#endregion

		#region Constructors
		
		public User()
		{
		}

		public User(string lastName, string firstName, string email, string password)
		{
			LastName = lastName;
			FirstName = firstName;
			Email = email;
			Password = password;
		}

		public User(int id, string lastName, string firstName, string email, string password)
		{
			Id = id;
			LastName = lastName;
			FirstName = firstName;
			Email = email;
			Password = password;
		}

		#endregion
	}
}