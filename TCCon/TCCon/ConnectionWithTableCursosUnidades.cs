using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace TCCon
{
	public class ConnectionWithTableCursosUnidades
	{
		public string
			ValorCodigoCurso,                           //Recebe o código do curso manual, exemplo: 78, 66, 55.
			ValorCodigoFaculdade,                       //Recebe o código da faculdade, exemplo: fatec itaquaquecetuba 155.
			RecebeCodigoPrimaryKey,                     //Recebe o código primário do curso, exemplo: 1,2,3,4,5
			RecebePrimaryKeyCursosUnidades;

		public string RecebeCodigoPrimaryKeyCursosUnidades()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = ("select COD_CURSO_UNIDADE from cursos_unidades where FK_CURSO = '" + ValorCodigoCurso + "' and FK_UNIDADE = '" + ValorCodigoFaculdade + "';");
			cmd.CommandType = CommandType.Text;
			cmd.Connection = cn;
			MySqlDataReader dr;
			dr = cmd.ExecuteReader();
			dr.Read();
			RecebePrimaryKeyCursosUnidades = Convert.ToString(dr.GetInt32(0));
			cn.Close();
			return RecebePrimaryKeyCursosUnidades;
		}

		public void InsereCursosUnidades()
		{
			//System.Diagnostics.Debug.WriteLine(CodigoDoCurso);
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "INSERT INTO cursos_unidades VALUES ('" + 0 + "','" + ValorCodigoCurso + "','" + ValorCodigoFaculdade + "');";
			cmd.ExecuteNonQuery();
			cn.Close();
		}

		public void DeleteCursosUnidades()
		{		
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			string delete_cursos_unidades = "DELETE FROM cursos_unidades WHERE fk_curso= '" + RecebeCodigoPrimaryKey + "' and fk_unidade = '" + ValorCodigoFaculdade + "';";
			MySqlCommand delete_cursos_unidadesCMD = new MySqlCommand(delete_cursos_unidades, cn);
			cn.Open();
			delete_cursos_unidadesCMD.ExecuteNonQuery();
			cn.Close();
		}
	}
}