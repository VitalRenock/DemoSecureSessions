using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTools.Patterns.Repositories
{
	/// <summary>
	/// Interface générique pour la création d'un 'Repository' pour les différents services de CRUD.
	/// </summary>
	/// <typeparam name="T">Type d'objet désiré pour les transactions effectuées.</typeparam>
	public interface ICrudRepository<T>
	{
		IEnumerable<T> Get();
		T Get(int id);
		int Add(T item);
		bool Edit(int id, T item);
		bool Delete(int id);
	}
}