using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTools.Database.RequestFactory
{
	public class Where
	{
		// Properties
		public List<SqlParameter> SqlParameters = new List<SqlParameter>();

		// Constructors
		public Where(string columnTarget, object value) => SqlParameters.Add(new SqlParameter(columnTarget, value));
		public Where(SqlParameter sqlParameter) => SqlParameters.Add(sqlParameter);
		public Where(List<SqlParameter> sqlParameters) => SqlParameters = sqlParameters;

		// Internal Methods 
		internal string GetExpression()
		{
			string queryPart = " WHERE ";

			for (int i = 0; i < SqlParameters.Count; i++)
				if (i == 0)
					queryPart += $"{SqlParameters[i].ParameterName} = @{SqlParameters[i].ParameterName.Trim().ToLower()}";
				else if (i == SqlParameters.Count - 1)
					queryPart += $" AND {SqlParameters[i].ParameterName} = @{SqlParameters[i].ParameterName.Trim().ToLower()}";
				else
					queryPart += $" AND {SqlParameters[i].ParameterName} = @{SqlParameters[i].ParameterName.Trim().ToLower()}";

			return queryPart;
		} 
	}
}
