using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// SELECT * FROM Section
// SELECT ID, LastName, FirstName FROM V_Student
// SELECT CONVERT(FLOAT, AVG(YearResult)) FROM Student
// SELECT * FROM Student WHERE LastName = 'Brigode' AND FirstName = 'Renaud'

// INSERT INTO Student (FirstName, LastName, BirthDate, YearResult, SectionID) output inserted.ID VALUES ('{student.FirstName}', '{student.LastName}', '{student.BirthDate.Year}-{student.BirthDate.Month}-{student.BirthDate.Day}', {student.YearResult}, {student.SectionId})
// INSERT INTO Student (FirstName, LastName, BirthDate, YearResult, SectionID) output inserted.ID VALUES (@firstName, @lastName, @birthDate, @yearResult, @sectionID)

// EXEC UpdateStudent @StudentId = @idStudent, @SectionID = @idSection, @YearResult = @resultYear
// EXEC SM_DeleteStudent @StudentId = @idStudent

namespace VitalTools.Database.RequestFactory
{
	public static class RequestBuilder
	{
		#region Private Methods

		private static string ConcatTable(string tableName)
		{
			return $" {tableName.Trim()} ";
		}

		private static string ConcatColumns(string[] columnsName)
		{
			string queryPart = string.Empty;

			for (int i = 0; i < columnsName.Length; i++)
				if (i == 0)
					queryPart += $"[{columnsName[i].Trim()}],";
				else if (i > 0 && i < columnsName.Length - 1)
					queryPart += $" [{columnsName[i].Trim()}],";
				else
					queryPart += $" [{columnsName[i].Trim()}]";

			return $" {queryPart} ";
		}

		private static string ConcatOutput(Output output)
		{
			return $" {output.GetOutput()} ";
		}

		private static string ConcatValues(string[] values)
		{
			string queryPart = string.Empty;

			queryPart += " VALUES (";
			for (int i = 0; i < values.Length; i++)
			{
				if (i == 0)
					queryPart += $"@{values[i].Trim().ToLower()},";
				else if (i > 0 && i < values.Length - 1)
					queryPart += $" @{values[i].Trim().ToLower()},";
				else
					queryPart += $" @{values[i].Trim().ToLower()}";
			}
			queryPart += ") ";

			return queryPart;
		}

		private static string ConcatWhere(Where where)
		{
			string queryPart = string.Empty;
			queryPart += where.GetExpression();
			return queryPart;
		}

		#endregion

		#region Public Methods

		public static string SelectBuilder(string table, string[] columnsName = null, Where where = null)
		{
			string query = "SELECT";
			if (columnsName != null && columnsName.Length > 0)
				query += ConcatColumns(columnsName);
			else
				query += " * ";
			query += "FROM";
			query += ConcatTable(table);
			if (where != null)
				query += ConcatWhere(where);
			return query;
		}

		public static string InsertIntoBuilder(string targetTable, string[] parametersName, Output output = null)
		{
			string query = "INSERT INTO";
			query += ConcatTable(targetTable);
			query += $"({ConcatColumns(parametersName)})";
			if (output != null)
				query += ConcatOutput(output);
			query += ConcatValues(parametersName);
			return query;
		}

		#endregion
	}
}
