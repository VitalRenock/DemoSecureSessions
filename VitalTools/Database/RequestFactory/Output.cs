using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTools.Database.RequestFactory
{
	public class Output
	{
		#region Constructors
		
		public Output(Type outputType, string columnName)
		{
			OutputType = outputType;
			Value = columnName;
		}

		#endregion

		#region Enums
		
		public enum Type
		{
			INSERTED,
			DELETED
		}

		#endregion

		#region Properties
		
		public Type OutputType { get; }
		public string Value { get; }

		#endregion

		#region Public Methods
		
		public string GetOutput()
		{
			return $"OUTPUT {OutputType.ToString()}.{Value}";
		}

		#endregion
	}
}
