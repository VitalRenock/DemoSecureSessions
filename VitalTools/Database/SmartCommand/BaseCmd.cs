using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VitalTools.Database.RequestFactory;

namespace VitalTools.Database.SmartCommand
{
	public abstract class BaseCmd
	{
		#region Properties

		protected string Table { get; set; }
		public IDictionary<string, object> Parameters { get; set; }
		protected Output Output { get; set; }

		private string query;
		public string Query
		{
			get 
			{ 
				return query; 
			}
			protected set 
			{ 
				query = value;
				SqlCommand = CreateSqlCommand();
			}
		}


		public SqlCommand SqlCommand { get; protected set; }

		#endregion

		#region Constructors

		public BaseCmd(string targetTable)
		{
			CheckTableName(targetTable);
		}

		#endregion

		#region Private Methods

		private SqlCommand CreateSqlCommand()
		{
			#region Création d'une nouvelle 'SqlCommand' avec la 'Query'

			SqlCommand newSqlCommand = new SqlCommand(Query);

			#endregion

			#region (Ajout des paramètres)

			if (Parameters != null && Parameters.Count > 0)
			{
				foreach (KeyValuePair<string, object> keyValue in Parameters)
					newSqlCommand.Parameters.Add(new SqlParameter($"{keyValue.Key.Trim()}", keyValue.Value)); 
			}

			#endregion

			return newSqlCommand;
		}

		private void CheckTableName(string tableName)
		{
			if (tableName == null)
				throw new ArgumentNullException("'targetTable' ne peut être 'null'");
			else if (tableName == string.Empty)
				throw new ArgumentException("'targetTable' ne peut être vide");
			else
				Table = tableName;
		}

		#endregion
	}
}
