using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VitalTools.Database.RequestFactory;

namespace VitalTools.Database.SmartCommand
{
	public class InsertCmd : BaseCmd
	{
		#region Constructors

		public InsertCmd(string targetTable, Dictionary<string, object> parameters, Output output = null) : base(targetTable)
		{
			#region Assignation + vérification de 'Parameters'
			
			if (parameters == null)
				throw new ArgumentOutOfRangeException("'parameters' ne peut être 'null'.");
			else if (parameters.Count == 0)
				throw new ArgumentOutOfRangeException("'parameters' doit contenir au moins 1 entrée.");
			else
				Parameters = parameters;

			#endregion

			#region Assignation de 'Output'
			
			Output = output;

			#endregion

			#region Construction et assignation de la requête
			
			Query = RequestBuilder.InsertIntoBuilder(Table, Parameters.Keys.ToArray(), Output);

			#endregion

			//#region Assignation de 'SqlCommand'

			//SqlCommand = CreateSqlCommand();

			//#endregion
		}

		#endregion
	}
}
