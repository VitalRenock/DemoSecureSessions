using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VitalTools.Database.SmartCommand;

namespace VitalTools.Database
{
	public class Connection : IDisposable
	{
		#region Properties

		private SqlConnection sqlConnection;

		#endregion

		#region Constructors

		/// <summary>
		/// Add description...
		/// </summary>
		/// <param name="connectionString"></param>
		public Connection(string connectionString)
		{
			#region Vérifications de la 'connectionString'.

			if (string.IsNullOrWhiteSpace(connectionString))
				throw new ArgumentNullException("La chaine de connexion ne peut être 'null', vide ou être composé uniquement d'espaces blancs.");

			#endregion

			#region Test puis assignation de la connexion

			using (SqlConnection testConnection = new SqlConnection(connectionString))
			{
				testConnection.Open();
			}
			this.sqlConnection = new SqlConnection(connectionString);

			#endregion
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Paramètre et retourne un objet 'SqlCommand' en fonction
		/// de la commande ('Command') et connexion ('SqlConnection') fournies.
		/// </summary>
		/// <param name="command">Commande paramètrée à transformée en objet 'SqlCommand'.</param>
		/// <param name="sqlConnection">Objet de connexion à la DB</param>
		/// <returns>Objet de type 'SqlCommand' paramètré.</returns>
		private SqlCommand CreateCommand(BaseCmd command, SqlConnection sqlConnection)
		{
			// Add Verif Command

			// Création d'une nouvelle commande
			SqlCommand cmd = sqlConnection.CreateCommand();

			// Affectation de la requête
			cmd.CommandText = command.Query;

			//// Gestion du cas d'une procédure stockée
			//cmd.CommandType = command.IsStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;

			#region Transmission des éventuels paramètres

			if (command.Parameters != null)
				foreach (KeyValuePair<string, object> kvp in command.Parameters)
				{
					// Création d'un nouveau 'SqlParameter' et ajout de celui-çi dans la commande.
					SqlParameter parameter = new SqlParameter(kvp.Key.Trim().ToLower(), kvp.Value);
					cmd.Parameters.Add(parameter);
				}

			#endregion

			return cmd;
		}


		/// <summary>
		/// Vérifie si la commande fournie n'est pas 'null', sinon remonte une erreur.
		/// </summary>
		/// <param name="commandToCheck">Commande à vérifier.</param>
		private void CheckNullCommand(BaseCmd commandToCheck)
		{
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
		public object ExecuteScalar(BaseCmd command)
		{
			CheckNullCommand(command);

			// Création d'une nouvelle connexion,
			using (sqlConnection)
			{
				// Création d'une nouvelle commande,
				using (SqlCommand newSqlCommand = CreateCommand(command, sqlConnection))
				{
					// Ouverture de la connexion
					sqlConnection.Open();

					// Execution de la commande
					object result = newSqlCommand.ExecuteScalar();

					// Retour du résultat, si le résultat est de type 'DBNull', on retourne un 'null'.
					return (result is DBNull) ? null : result;
				}
			}
		}


		/// <summary>
		/// Méthode qui retourne une TABLE de données de type 'IEnumerable<TResult>'.
		/// (Mode connecté)
		/// ??? Pour les Select?
		/// </summary>
		/// <typeparam name="TResult">Type d'objet désiré</typeparam>
		/// <param name="command">Commande déjà paramètrée</param>
		/// <param name="selector"></param>
		/// <returns>Retourne une collection de type IEnumerable<TResult> représentant la TABLE de données</returns>
		public IEnumerable<TResult> ExecuteReader<TResult>(BaseCmd command, Func<IDataRecord, TResult> selector)
		{
			CheckNullCommand(command);

			if (selector is null)
				throw new ArgumentNullException(nameof(selector));

			using (sqlConnection)
			{
				using (SqlCommand newSqlCommand = CreateCommand(command, sqlConnection))
				{
					// Ouverture de la connexion
					sqlConnection.Open();

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
		public SqlDataReader ExecuteReader(BaseCmd command)
		{
			CheckNullCommand(command);

			using (sqlConnection)
			{
				using (SqlCommand newSqlCommand = CreateCommand(command, sqlConnection))
				{
					sqlConnection.Open();
					return newSqlCommand.ExecuteReader();
				}
			}
		}


		/// <summary>
		/// Méthode qui retourne une DATATABLE.
		/// (Mode déconnecté)
		/// ??? Pour les Select?
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		public DataTable GetDataTable(BaseCmd command)
		{
			CheckNullCommand(command);

			using (sqlConnection)
			{
				using (SqlCommand newSqlCommand = CreateCommand(command, sqlConnection))
				{
					// Création d'un nouveau 'SqlDataAdapter' avec le 'SqlCommand' à executer.
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(newSqlCommand);

					// Création d'une nouvelle 'DataTable'
					DataTable datatableToReturn = new DataTable();

					// Exécution de la méthode Fill() du 'SqlDataAdapter' sur la 'DataTable'
					sqlDataAdapter.Fill(datatableToReturn);

					return datatableToReturn;
				}
			}
		}


		/// <summary>
		/// Méthode qui execute la commande et retourne le nombre de lignes affectées.
		/// ??? Pour les Insert, Update et Delete?
		/// </summary>
		/// <param name="command">Commande à executer.</param>
		/// <returns>Retourne le nombre de lignes affectées.</returns>
		public int ExecuteNonQuery(BaseCmd command)
		{
			CheckNullCommand(command);

			using (sqlConnection)
			{
				using (SqlCommand newSqlCommand = CreateCommand(command, sqlConnection))
				{
					sqlConnection.Open();
					return newSqlCommand.ExecuteNonQuery();
				}
			}
		}

		public void Dispose()
		{
			sqlConnection.Close();
			sqlConnection.Dispose();
		}

		#endregion
	}
}