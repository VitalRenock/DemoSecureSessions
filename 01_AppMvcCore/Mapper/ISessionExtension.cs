using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


/// <summary>
/// Classe d'extension pour ISession, permettant de sérialiser un objet (User, Movie, Game, Book, ect)
/// </summary>
namespace _01_AppMvcCore.Mapper
{
	public static class ISessionExtension
	{
		/// <summary>
		/// Méthode permettant d'ajouter un objet (qui sera sérialisé) dans la Session (HttpContext.Session)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="session"></param>
		/// <param name="key">Clé pour l'objet</param>
		/// <param name="value">L'objet</param>
		public static void Set<T>(this ISession session, string key, T value)
		{
			session.SetString(key, JsonSerializer.Serialize(value));
		}

		/// <summary>
		/// Méthode permettant de récupérer un objet sérialisé depuis la Session (HttpContext.Session)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="session"></param>
		/// <param name="key">Clé pour l'objet</param>
		/// <returns>L'objet à la clé indiqué</returns>
		public static T Get<T>(this ISession session, string key)
		{
			string value = session.GetString(key);
			return value == null ? default : JsonSerializer.Deserialize<T>(value);
		}
	}
}
