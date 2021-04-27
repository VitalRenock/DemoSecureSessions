using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using _01_AppMvcCore.Mapper;
using _01_AppMvcCore.Models;
using VitalTools.Database.Formation;
using VitalTools.Database.SmartCommand;

namespace _01_AppMvcCore.Services
{
	public class AuthService : IAuthService
	{
		private ConnectionFormation _connection;
		private static string connectionStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FantomesDB;Integrated Security=True;";

		public AuthService()
		{
			_connection = new ConnectionFormation(connectionStr);
		}


		public bool Register(User user)
		{
			CommandFormation commandFormation = new CommandFormation("FSP_Register", true);
			commandFormation.AddParameter("LastName", user.LastName);
			commandFormation.AddParameter("FirstName", user.FirstName);
			commandFormation.AddParameter("Email", user.Email);
			commandFormation.AddParameter("Password", user.Password);

			return _connection.ExecuteNonQuery(commandFormation) == 1;
		}

		public User Login(string email, string password)
		{
			CommandFormation commandFormation = new CommandFormation("FSP_Login", true);
			commandFormation.AddParameter("Email", email);
			commandFormation.AddParameter("Password", password);

			return _connection.ExecuteReader(commandFormation, dr => dr.ToUser()).SingleOrDefault();
		}
	}
}
