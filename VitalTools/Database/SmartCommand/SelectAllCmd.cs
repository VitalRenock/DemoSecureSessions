using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VitalTools.Database.RequestFactory;

namespace VitalTools.Database.SmartCommand
{
	public class SelectAllCmd : BaseCmd
	{
		#region Constructors
		
		public SelectAllCmd(string targetTable) : base(targetTable)
		{
			Query = RequestBuilder.SelectBuilder(targetTable);
		} 

		#endregion
	}
}
