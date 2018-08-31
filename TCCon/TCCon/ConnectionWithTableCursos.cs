using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;

namespace TCCon
{
	public class ConnectionWithTableCursos
	{
		public string
			ValorCodigoFaculdade,               //O numero do codigo das faculdades, exemplo: fatec itaquaquecetuba codigo 155
			RecebeUltimoCodigoCurso = "",       //Recebe o ultimo codigo cadastrado no banco de dados
			ValorCodigoCurso,                   //Recebe o codigo do curso que é digitado manualmente, exemplo: 78, 66, 80
			ValorCodigoCursoAntigo,             //Recebe o codigo do curso atual que será trocado
			ValorNomeCurso,                     //Recebe o nome do curso
			ValorTipoCurso,                     //Recebe o tipo de projeto daquele curso
			RecebeCodigoPrimaryKey;	             //Recebe o codigo do curso que é primary key, exemplo: 1,2,3,4,5...
			
		public DataTable VerificaGridViewVazia()
		{
			DataTable dt = new DataTable();
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			MySqlDataAdapter adp = new MySqlDataAdapter("SELECT cursos.cod_curso, cursos.ID_CURSOS_IGUAL, cursos.curso, cursos.tipo_projeto " +
				"FROM cursos_unidades " +
				"INNER JOIN cursos ON (cursos_unidades.fk_curso = cursos.cod_curso) " +
				"INNER JOIN unidades ON (cursos_unidades.fk_unidade = unidades.cod_unidade) " +
				"WHERE cod_unidade = '" + ValorCodigoFaculdade + "' " +
				"ORDER BY cursos.ID_CURSOS_IGUAL ;", cn);
			cn.Open();
			adp.Fill(dt);
			cn.Close();
			return dt;
		}
		public DataTable VerificaCursoIdCadastrado()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "SELECT cursos.ID_CURSOS_IGUAL, unidades.COD_UNIDADE " +
				"FROM cursos_unidades " +
				"INNER JOIN cursos ON cursos_unidades.FK_CURSO = cursos.COD_CURSO " +
				"INNER JOIN unidades ON cursos_unidades.FK_UNIDADE = unidades.COD_UNIDADE " +
				"WHERE ID_CURSOS_IGUAL = '" + ValorCodigoCurso + "' AND cod_unidade = '" + ValorCodigoFaculdade + "';";
			cmd.ExecuteNonQuery();
			DataTable dt = new DataTable();
			MySqlDataAdapter da = new MySqlDataAdapter(cmd);
			da.Fill(dt);
			cn.Close();
			return dt;
		}
		public DataTable VerificaCursoNomeCadastrado()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "SELECT cursos.CURSO, unidades.COD_UNIDADE " +
				"FROM cursos_unidades " +
				"INNER JOIN cursos ON cursos_unidades.FK_CURSO = cursos.COD_CURSO " +
				"INNER JOIN unidades ON cursos_unidades.FK_UNIDADE = unidades.COD_UNIDADE " +
				"WHERE COD_UNIDADE = '" + ValorCodigoFaculdade + "' AND CURSO = '" + ValorNomeCurso.ToUpper() + "';";
			cmd.ExecuteNonQuery();
			DataTable dt = new DataTable();
			MySqlDataAdapter da = new MySqlDataAdapter(cmd);
			da.Fill(dt);
			cn.Close();
			return dt;
		}
		public string RecebeUltimoCursoCadastrado()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = ("SELECT max(cod_curso) FROM cursos WHERE " +
				"ID_CURSOS_IGUAL = '"+ ValorCodigoCurso + "' " +
				"AND CURSO = '"+ ValorNomeCurso + "' " +
				"AND TIPO_PROJETO = '"+ ValorTipoCurso + "';");
			cmd.CommandType = CommandType.Text;
			cmd.Connection = cn;
			MySqlDataReader dr;
			dr = cmd.ExecuteReader();
			dr.Read();
			RecebeUltimoCodigoCurso = Convert.ToString(dr.GetInt32(0));
			cn.Close();
			return RecebeUltimoCodigoCurso;		
		}
		public string RecebeCodigoCursoSelecionadoPrimaryKey()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = ("SELECT cursos.cod_curso " +
				"FROM cursos_unidades " +
				"INNER JOIN cursos ON cursos_unidades.FK_CURSO = cursos.COD_CURSO " +
				"INNER JOIN unidades ON cursos_unidades.FK_UNIDADE = unidades.COD_UNIDADE " +
				"WHERE ID_CURSOS_IGUAL = '" + ValorCodigoCursoAntigo + "' AND COD_UNIDADE ='" + ValorCodigoFaculdade + "';");
			cmd.CommandType = CommandType.Text;
			cmd.Connection = cn;
			cn.Open();
			MySqlDataReader dr;
			dr = cmd.ExecuteReader();
			dr.Read();
			RecebeCodigoPrimaryKey = Convert.ToString(dr.GetInt32(0));
			cn.Close();
			return RecebeCodigoPrimaryKey;
		}
		public void InsereCurso()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "INSERT INTO cursos VALUES ('" + 0 + "','" + ValorCodigoCurso + "','" + ValorNomeCurso + "','" + ValorTipoCurso + "');";
			cmd.ExecuteNonQuery();
			cn.Close();
		}
		public void AlteraCurso()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "UPDATE cursos SET id_cursos_igual ='" + ValorCodigoCurso + "',curso = '" + ValorNomeCurso.ToUpper() + "',tipo_projeto = '" + ValorTipoCurso + "' WHERE cod_curso = '" + RecebeCodigoPrimaryKey + "';";
			cmd.ExecuteNonQuery();
			cn.Close();
		}
		public void DeletaCurso()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "DELETE FROM cursos WHERE cod_curso= '" + RecebeCodigoPrimaryKey + "';";
			cmd.ExecuteNonQuery();
			cn.Close();
		}
	}
}