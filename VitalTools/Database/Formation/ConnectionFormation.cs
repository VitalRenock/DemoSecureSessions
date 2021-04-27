using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalTools.Database.Formation
{
	/// <summary>
	/// Classe qui facilite la connexion à une base de données SQL Server.
	/// Utilise les classes 'SqlConnection' et 'SqlCommand' de 'System.Data.SqlClient'.
	/// </summary>
	public class ConnectionFormation
	{
		#region Properties

		private string connectionString { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Add description...
		/// </summary>
		/// <param name="connectionString"></param>
		public ConnectionFormation(string connectionString)
		{
			#region Vérifications
			
			if (string.IsNullOrWhiteSpace(connectionString))
				throw new ArgumentNullException("La chaine de connexion ne peut être 'null', vide ou être composé uniquement d'espaces blancs.");

			#endregion

			this.connectionString = connectionString;

			#region Test de la connexion
			
			using (SqlConnection connection = CreateConnection())
			{
				connection.Open();
			}

			#endregion
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Retourne un objet 'SqlConnection' avec la 'ConnectionString' définie.
		/// </summary>
		/// <returns>Objet de type 'SqlConnection'</returns>
		private SqlConnection CreateConnection()
		{
			return new SqlConnection(connectionString);
		}


		/// <summary>
		/// Paramètre et retourne un objet 'SqlCommand' en fonction
		/// de la commande ('Command') et connexion ('SqlConnection') fournies.
		/// </summary>
		/// <param name="command">Commande paramètrée à transformée en objet 'SqlCommand'.</param>
		/// <param name="sqlConnection">Objet de connexion à la DB</param>
		/// <returns>Objet de type 'SqlCommand' paramètré.</returns>
		private SqlCommand CreateCommand(CommandFormation command, SqlConnection sqlConnection)
		{
			#region Création d'une nouvelle commande

			SqlCommand cmd = sqlConnection.CreateCommand();

			#endregion

			#region Affectation de la requête

			cmd.CommandText = command.Query;

			#endregion

			#region Gestion du cas d'une procédure stockée

			cmd.CommandType = command.IsStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;

			#endregion

			#region Transmission des éventuels paramètres

			if (command.ParametersList != null)
			{
				foreach (KeyValuePair<string, object> kvp in command.ParametersList)
				{
					// Création d'un nouveau paramètre de type 'SqlParameter'.
					SqlParameter parameter = new SqlParameter();

					// Affectation de la clé et de la valeur dans le nouveau 'SqlParameter'.
					parameter.ParameterName = kvp.Key;
					parameter.Value = kvp.Value;

					// Ajout du nouveau 'paramètre' dans la collection de 'paramètres'
					// de l'objet 'SqlCommand' à retourner
					cmd.Parameters.Add(parameter);
				}
			}

			#endregion

			return cmd;
		}


		/// <summary>
		/// Vérifie si la commande fournie n'est pas 'null', sinon remonte une erreur.
		/// </summary>
		/// <param name="commandToCheck">Commande à vérifier.</param>
		private void CheckNullCommand(CommandFormation commandToCheck)
		{
			// Si 'commandToCheck' est null, on remonte une erreur.
			if (commandToCheck is null)
				throw new ArgumentNullException(nameof(commandToCheck));
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Retourne UN seul résultat du type de la colonne ciblée ou de l'output demandé dans la requête fournie.
		/// (Mode connecté)
		/// </summary>
		/// <param name="command">Commande paramètrée à executée.</param>
		/// <returns>Le résultat du type de la colonne ciblée ou de l'output demandé.</returns>
		public object ExecuteScalar(CommandFormation command)
		{
			#region Vérifications

			CheckNullCommand(command);

			#endregion

			using (SqlConnection newSqlConnection = CreateConnection())
			{
				using (SqlCommand newSqlCommand = CreateCommand(command, newSqlConnection))
				{
					#region Ouverture de la connexion
					
					newSqlConnection.Open();

					#endregion

					object result = newSqlCommand.ExecuteScalar();

					return (result is DBNull) ? null : result;
				}
			}
		}


		// ??? Pour les Select?
		/// <summary>
		/// Méthode qui retourne une TABLE de données de type 'IEnumerable<TResult>'.
		/// (Mode connecté)
		/// </summary>
		/// <typeparam name="TResult">Type d'objet désiré</typeparam>
		/// <param name="command">Commande déjà paramètrée</param>
		/// <param name="selector"></param>
		/// <returns>Retourne une collection de type IEnumerable<TResult> représentant la TABLE de données</returns>
		public IEnumerable<TResult> ExecuteReader<TResult>(CommandFormation command, Func<IDataRecord, TResult> selector)
		{
			#region Vérifications

			CheckNullCommand(command);

			// ??? Que représente 'selector'
			if (selector is null)
				throw new ArgumentNullException(nameof(selector));

			#endregion

			using (SqlConnection newSqlConnection = CreateConnection())
			{
				using (SqlCommand newSqlCommand = CreateCommand(command, newSqlConnection))
				{
					#region Ouverture de la connexion
					
					newSqlConnection.Open();

					#endregion

					#region Exécution la méthode ExecuteReader sur l'object de type SqlCommand
					
					using (SqlDataReader reader = newSqlCommand.ExecuteReader())
					{
						// Consommation de l'objet de type 'SqlDataReader'.
						while (reader.Read())
							yield return selector(reader);
					}

					#endregion
				}
			}
		}


		// ??? Pour les Select?
		/// <summary>
		/// Méthode qui retourne une DATATABLE.
		/// (Mode déconnecté)
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		public DataTable GetDataTable(CommandFormation command)
		{
			#region Vérifications

			CheckNullCommand(command);

			#endregion

			using (SqlConnection newSqlConnection = CreateConnection())
			{
				using (SqlCommand newSqlCommand = CreateCommand(command, newSqlConnection))
				{
					#region Création d'un nouveau 'SqlDataAdapter'

					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

					#endregion

					#region Assignation de la commande dans le 'SqlDataAdaptater'

					sqlDataAdapter.SelectCommand = newSqlCommand;

					#endregion

					#region Création d'une nouvelle 'DataTable'
						
					DataTable datatableToReturn = new DataTable();

					#endregion

					#region Exécution de la méthode Fill() du 'SqlDataAdapter' sur la 'DataTable'

					sqlDataAdapter.Fill(datatableToReturn);

					#endregion

					return datatableToReturn;
				}
			}
		}


		// ??? Pour les Insert, Update et Delete?
		/// <summary>
		/// Méthode qui execute la commande et retourne le nombre de lignes affectées.
		/// </summary>
		/// <param name="command">Commande à executer.</param>
		/// <returns>Retourne le nombre de lignes affectées.</returns>
		public int ExecuteNonQuery(CommandFormation command)
		{
			#region Vérifications

			CheckNullCommand(command);

			#endregion

			using (SqlConnection newSqlConnection = CreateConnection())
			{
				using (SqlCommand newSqlCommand = CreateCommand(command, newSqlConnection))
				{
					#region Ouverture de la connexion
					
					newSqlConnection.Open();

					#endregion

					int result = newSqlCommand.ExecuteNonQuery();

					return result;
				}
			}
		}

		#endregion
	}
}
