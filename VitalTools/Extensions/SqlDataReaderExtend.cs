using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTools.Extensions
{
	public static class SqlDataReaderExtend
	{
		public static string ToText(this SqlDataReader reader, string separator)
		{
			string textToDisplay = string.Empty;
			//using (reader)
			//{
				//while (reader.Read())
				//{
					for (int c = 0; c < reader.FieldCount; c++)
					{
						if (c == reader.FieldCount - 1)
							textToDisplay += $"{reader[c]}";
						else
							textToDisplay += $"{reader[c]}{separator}";
					}
					textToDisplay += "\n";
				//}
			//}

			return textToDisplay;
		}
		public static string ToText(this SqlDataReader reader, string prefixToAdd = null, string suffixToAdd = null)
		{
			string textToDisplay = string.Empty;

			while (reader.Read())
			{
				for (int c = 0; c < reader.FieldCount; c++)
					textToDisplay += $"{prefixToAdd ?? string.Empty}{reader[c]}{suffixToAdd ?? string.Empty}";
				
				textToDisplay += "\n";
			}

			return textToDisplay;
		}
	}
}
