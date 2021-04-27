using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTools.Database.Formation
{
	/// <summary>
	/// Add description...
	/// </summary>
	public class CommandFormation
	{
		#region Properties

		private Dictionary<string, object> parametersList;
		public Dictionary<string, object> ParametersList
		{
			get { return parametersList; }
		}

		private bool isStoredProcedure;
		public bool IsStoredProcedure
		{
			get { return isStoredProcedure; }
		}

		private string query;
		public string Query
		{
			get { return query; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Add description...
		/// </summary>
		/// <param name="query"></param>
		/// <param name="isStoredProcedure"></param>
		public CommandFormation(string query, bool isStoredProcedure = false)
		{
			if (string.IsNullOrWhiteSpace(query))
				throw new ArgumentNullException("Une requête ne peut être 'null', vide ou être composé uniquement d'espaces blancs.");

			this.query = query;
			this.isStoredProcedure = isStoredProcedure;
			parametersList = new Dictionary<string, object>();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Add description...
		/// </summary>
		/// <param name="parameterName"></param>
		/// <param name="value"></param>
		public void AddParameter(string parameterName, object value)
		{
			#region Vérifications

			if (string.IsNullOrWhiteSpace(parameterName))
				throw new ArgumentNullException("Un paramètre de requête ne peut être 'null', vide ou être composé uniquement d'espaces blancs.");

			if (parametersList.ContainsKey(parameterName))
				throw new MissingMemberException("Le paramètre de requête {0} existe déjà.", parameterName);

			#endregion

			#region Ajout du paramètre dans la liste avec gestion des valeurs 'null'.

			parametersList.Add(parameterName, value ?? DBNull.Value);

			#endregion
		}

		#endregion
	}
}
