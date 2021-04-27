using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using _01_AppMvcCore.Models;

namespace _01_AppMvcCore.Mapper
{
	public static class Mapper
	{
		public static User ToUser(this IDataRecord record)
		{
			return new User()
			{
				Id = (int)record["Id"],
				LastName =	(string)record["LastName"],
				FirstName = (string)record["FirstName"],
				Email =		(string)record["Email"],
				//Password = (string)record["Password"]
			};
		}
	}
}
