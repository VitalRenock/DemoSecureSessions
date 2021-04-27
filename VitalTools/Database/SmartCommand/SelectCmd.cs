using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VitalTools.Database.RequestFactory;

// SELECT*
// FROM table
// WHERE condition
// GROUP BY expression
// HAVING condition
// { UNION | INTERSECT | EXCEPT }
// ORDER BY expression
// LIMIT count
// OFFSET start

namespace VitalTools.Database.SmartCommand
{
	public class SelectCmd : BaseCmd
	{
		#region Constructors

		/// <summary>
		/// Détails:
		/// - À la création d'une nouvelle commande, on appelle le constructeur 'parent'.
		/// - On construit la requête en appellant le 'RequestBuilder' avec les paramètres fournis.
		/// - On ajoute aux 'Parameters' de la commande le 'SqlParameter' disponible dans la classe 'Where' fournie.
		///   (L'utilisation du SqlParameter crée par la classe 'Where' assure la même nomenclature entre les
		///   paramètres de la requête et le nom des paramètres du 'SqlCommand')
		/// </summary>
		/// <param name="targetTable"></param>
		/// <param name="columnsName"></param>
		/// <param name="where"></param>
		public SelectCmd(string targetTable, string[]columnsName = null, Where where = null) : base(targetTable)
		{
			Query = RequestBuilder.SelectBuilder(targetTable, columnsName, where);

			if (where != null)
				foreach (SqlParameter parameter in where.SqlParameters)
					SqlCommand.Parameters.Add(parameter);
		}

		#endregion
	}
}
