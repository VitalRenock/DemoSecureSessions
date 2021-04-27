using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTools.Extensions
{
	public static class DataTableExtend
	{
		/// <summary>
		/// Retourne les valeurs du 'DataTable' dans un texte avec une mise en forme simple.
		/// Chaque valeur sera séparée par la valeur de 'separator'.
		/// </summary>
		/// <param name="dataTable">Table ciblée.</param>
		/// <param name="separator">Caractère qui séparera les différentes valeurs.</param>
		/// <returns>Texte mise en forme.</returns>
		public static string ToText(this DataTable dataTable, string separator)
		{
			string textToDisplay = string.Empty;

			for (int r = 0; r < dataTable.Rows.Count; r++)
			{
				for (int c = 0; c < dataTable.Columns.Count; c++)
				{
					if (c == dataTable.Columns.Count - 1)
						textToDisplay += $"{dataTable.Rows[r][dataTable.Columns[c]]}";
					else
						textToDisplay += $"{dataTable.Rows[r][dataTable.Columns[c]]}{separator}";
				}
				textToDisplay += "\n";
			}

			return textToDisplay;
		}
		/// <summary>
		/// Retourne les valeurs du 'DataTable' dans un texte avec une mise en forme simple.
		/// </summary>
		/// <param name="dataTable">Table ciblée.</param>
		/// <param name="prefixToAdd">Caractère qui sera ajouté en début de valeur. (Facultatif)</param>
		/// <param name="suffixToAdd">Caractère qui sera ajouté en fin de valeur. (Facultatif)</param>
		/// <returns>Texte mise en forme.</returns>
		public static string ToText(this DataTable dataTable, string prefixToAdd = null, string suffixToAdd = null)
		{
			string textToDisplay = string.Empty;

			foreach (DataRow row in dataTable.Rows)
			{
				foreach (DataColumn column in dataTable.Columns)
					textToDisplay += $"{prefixToAdd ?? string.Empty}{row[column]}{suffixToAdd ?? string.Empty}";
				
				textToDisplay += "\n";
			}

			return textToDisplay;
		}
	}
}
