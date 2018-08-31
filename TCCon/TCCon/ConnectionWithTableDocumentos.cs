using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace TCCon
{
	public class ConnectionWithTableDocumentos
	{
		public string
			ValorCodigoFaculdade,									//Recebe e envia o codigo da faculdade
			ValorCodigoDocumento,                                   //Recebe e envia o codigo do documento
			ValorCodigoCurso,                                       //Recebe e envia o codigo do curso
			RecebePrimaryKeyCursosUnidades,                         //Recebe e envia a primary key do curso com aquela unidade
			ValorNomeTitulo,                                        //Recebe e envia o nome do titulo
			ValorNomeAutorUm,                                       //Recebe e envia o nome do primeiro autor
			ValorNomeAutorDois,                                     //Recebe e envia o nome do segundo autor
			ValorNomeAutorTres,                                     //Recebe e envia o nome do terceiro autor
			ValorNomeAutorQuatro,                                   //Recebe e envia o nome do quarto autor
			ValorPalavraChave,                                      //Recebe e envia as palavras chave 
			ValorAnoProjeto,										//Recebe e envia a data do projeto
			ValorCaminhoPdf,                                        //Recebe e envia o caminho do PDF
			ValorCaminhoRar,                                        //Recebe e envia o caminho do RAR
			ValorResumo,											//Recebe e envia o resumo do projeto
			FiltraListViewAutor;									//Envia o nome do autor digitado na busca em MONOGRAFIA 
		public DataTable VerificaListViewMonografia()
		{
			DataTable dt = new DataTable();
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM cursos_unidades " +
				"INNER JOIN documentos ON cursos_unidades.COD_CURSO_UNIDADE = documentos.FK_CURSO_UNIDADE " +
				"INNER JOIN unidades ON cursos_unidades.FK_UNIDADE = unidades.COD_UNIDADE " +
				"INNER JOIN cursos ON cursos_unidades.FK_CURSO = cursos.COD_CURSO " +
				"ORDER BY documentos.TITULO;", cn);
			cn.Open();
			adp.Fill(dt);
			cn.Close();
			return dt;
		}
		public DataTable VerificaListViewVazia()
		{		
			DataTable dt = new DataTable();
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM cursos_unidades " +
				"INNER JOIN documentos ON cursos_unidades.COD_CURSO_UNIDADE = documentos.FK_CURSO_UNIDADE " +
				"INNER JOIN unidades ON cursos_unidades.FK_UNIDADE = unidades.COD_UNIDADE " +
				"INNER JOIN cursos ON cursos_unidades.FK_CURSO = cursos.COD_CURSO " +
				"WHERE cod_unidade = '" + ValorCodigoFaculdade + "' ORDER BY documentos.COD_DOCUMENTO desc;", cn);
			cn.Open();
			adp.Fill(dt);
			cn.Close();
			return dt;
		}
		public DataTable VerificaDocumentoCursoCadastrado()
		{
			DataTable dt = new DataTable();
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM cursos_unidades " +
				"INNER JOIN documentos ON cursos_unidades.COD_CURSO_UNIDADE = documentos.FK_CURSO_UNIDADE " +
				"INNER JOIN unidades ON unidades.COD_UNIDADE = cursos_unidades.FK_UNIDADE " +
				"INNER JOIN cursos ON cursos_unidades.FK_CURSO = cursos.COD_CURSO " +
				"WHERE COD_CURSO = '" + ValorCodigoCurso + "' AND COD_UNIDADE = '" + ValorCodigoFaculdade + "';", cn);
			cn.Open();
			adp.Fill(dt);
			cn.Close();
			return dt;
		}
		public DataTable FiltraListViewFaculdade()
		{
			DataTable dt = new DataTable();
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM cursos_unidades " +
				"INNER JOIN documentos ON cursos_unidades.COD_CURSO_UNIDADE = documentos.FK_CURSO_UNIDADE " +
				"INNER JOIN unidades ON cursos_unidades.FK_UNIDADE = unidades.COD_UNIDADE " +
				"INNER JOIN cursos ON cursos_unidades.FK_CURSO = cursos.COD_CURSO " +
				"WHERE cod_unidade = '" + ValorCodigoFaculdade + "' ORDER BY documentos.TITULO;", cn);
			cn.Open();
			adp.Fill(dt);
			cn.Close();
			return dt;
		}
		public DataTable FiltraListViewCurso()
		{
			DataTable dt = new DataTable();
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM cursos_unidades " +
				"INNER JOIN documentos ON cursos_unidades.COD_CURSO_UNIDADE = documentos.FK_CURSO_UNIDADE " +
				"INNER JOIN unidades ON cursos_unidades.FK_UNIDADE = unidades.COD_UNIDADE " +
				"INNER JOIN cursos ON cursos_unidades.FK_CURSO = cursos.COD_CURSO " +
				"WHERE cod_unidade = '" + ValorCodigoFaculdade + "' AND cod_curso = '" + ValorCodigoCurso + "' ORDER BY documentos.TITULO;", cn);
			cn.Open();
			adp.Fill(dt);
			cn.Close();
			return dt;
		}
		public DataTable FiltraListViewAutorTituloPalavraChave()
		{
			string palavras = string.Empty;
			string[] colunas = ValorPalavraChave.Split(' ');
			Array.Sort(colunas);
			var i = 0;
			foreach (var item in colunas)
			{
				i++;
				palavras += "PALAVRA_CHAVE LIKE '%" + item + "%'";
				if (i != colunas.Length)
				{
					palavras += " AND ";
				}
			}
			DataTable dt = new DataTable();
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM cursos_unidades " +
				"INNER JOIN documentos ON cursos_unidades.COD_CURSO_UNIDADE = documentos.FK_CURSO_UNIDADE " +
				"INNER JOIN unidades ON cursos_unidades.FK_UNIDADE = unidades.COD_UNIDADE " +
				"INNER JOIN cursos ON cursos_unidades.FK_CURSO = cursos.COD_CURSO " +
				"WHERE(AUTOR_UM LIKE '%" + FiltraListViewAutor + "%' OR AUTOR_DOIS LIKE '%" + FiltraListViewAutor + "%' OR AUTOR_TRES LIKE '%" + FiltraListViewAutor + "%' OR AUTOR_QUATRO LIKE '%" + FiltraListViewAutor + "%')" +
				"AND ("+ palavras + ") " +
				"AND (TITULO LIKE '%" + ValorNomeTitulo + "%') " +
				"ORDER BY documentos.TITULO; ", cn);			
			cn.Open();
			adp.Fill(dt);
			cn.Close();
			return dt;
		}
		public DataTable FiltraListViewFaculdadeAutorTituloPalavraChave()
		{
			string palavras = string.Empty;
			string[] colunas = ValorPalavraChave.Split(' ');
			Array.Sort(colunas);
			var i = 0;
			foreach (var item in colunas)
			{
				i++;
				palavras += "PALAVRA_CHAVE LIKE '%" + item + "%'";
				if (i != colunas.Length)
				{
					palavras += " AND ";
				}
			}
			DataTable dt = new DataTable();
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM cursos_unidades " +
				"INNER JOIN documentos ON cursos_unidades.COD_CURSO_UNIDADE = documentos.FK_CURSO_UNIDADE " +
				"INNER JOIN unidades ON cursos_unidades.FK_UNIDADE = unidades.COD_UNIDADE " +
				"INNER JOIN cursos ON cursos_unidades.FK_CURSO = cursos.COD_CURSO " +
				"WHERE(AUTOR_UM LIKE '%" + FiltraListViewAutor + "%' OR AUTOR_DOIS LIKE '%" + FiltraListViewAutor + "%' OR AUTOR_TRES LIKE '%" + FiltraListViewAutor + "%' OR AUTOR_QUATRO LIKE '%" + FiltraListViewAutor + "%')" +
				"AND (" + palavras + ") " +
				"AND (TITULO LIKE '%" + ValorNomeTitulo + "%') " +
				"AND (COD_UNIDADE = '" + ValorCodigoFaculdade + "')" +
				"ORDER BY documentos.TITULO; ", cn);
			cn.Open();
			adp.Fill(dt);
			cn.Close();
			return dt;
		}
		public DataTable FiltraListViewFaculdadeCursoAutorTituloPalavraChave()
		{
			string palavras = string.Empty;
			string[] colunas = ValorPalavraChave.Split(' ');
			Array.Sort(colunas);
			var i = 0;
			foreach (var item in colunas)
			{
				i++;
				palavras += "PALAVRA_CHAVE LIKE '%" + item + "%'";
				if (i != colunas.Length)
				{
					palavras += " AND ";
				}
			}

			DataTable dt = new DataTable();
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM cursos_unidades " +
				"INNER JOIN documentos ON cursos_unidades.COD_CURSO_UNIDADE = documentos.FK_CURSO_UNIDADE " +
				"INNER JOIN unidades ON cursos_unidades.FK_UNIDADE = unidades.COD_UNIDADE " +
				"INNER JOIN cursos ON cursos_unidades.FK_CURSO = cursos.COD_CURSO " +
				"WHERE(AUTOR_UM LIKE '%" + FiltraListViewAutor + "%' OR AUTOR_DOIS LIKE '%" + FiltraListViewAutor + "%' OR AUTOR_TRES LIKE '%" + FiltraListViewAutor + "%' OR AUTOR_QUATRO LIKE '%" + FiltraListViewAutor + "%')" +
				"AND ("+ palavras +")" +
				"AND (TITULO LIKE '%" + ValorNomeTitulo + "%') " +
				"AND (COD_UNIDADE = '" + ValorCodigoFaculdade + "') " +
				"AND (COD_CURSO = '" + ValorCodigoCurso + "')" +
				"ORDER BY documentos.TITULO; ", cn);
			cn.Open();
			adp.Fill(dt);
			cn.Close();
			return dt;
		}
		public DataTable VerificaTituloPrimeiroAutorDocumentoCadastrar()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "SELECT * FROM cursos_unidades " +
				"INNER JOIN documentos ON cursos_unidades.COD_CURSO_UNIDADE = documentos.FK_CURSO_UNIDADE " +
				"INNER JOIN unidades ON unidades.COD_UNIDADE = cursos_unidades.FK_UNIDADE " +
				"WHERE (AUTOR_UM LIKE '%" + ValorNomeAutorUm + "%' OR AUTOR_DOIS LIKE '%" + ValorNomeAutorUm + "%' OR AUTOR_TRES LIKE '%" + ValorNomeAutorUm + "%' OR AUTOR_QUATRO LIKE '%" + ValorNomeAutorUm + "%') " +
				"AND (documentos.TITULO = '" + ValorNomeTitulo + "') " +
				"AND (unidades.COD_UNIDADE = '" + ValorCodigoFaculdade + "'); ";
			cmd.ExecuteNonQuery();
			DataTable dt = new DataTable();
			MySqlDataAdapter da = new MySqlDataAdapter(cmd);
			da.Fill(dt);
			cn.Close();
			return dt;
		}
		public DataTable VerificaTituloPrimeiroAutorDocumentoAlterar()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "SELECT * FROM cursos_unidades " +
				"INNER JOIN documentos ON cursos_unidades.COD_CURSO_UNIDADE = documentos.FK_CURSO_UNIDADE " +
				"INNER JOIN unidades ON unidades.COD_UNIDADE = cursos_unidades.FK_UNIDADE " +
				"WHERE (AUTOR_UM LIKE '%" + ValorNomeAutorUm + "%' OR AUTOR_DOIS LIKE '%" + ValorNomeAutorUm + "%' OR AUTOR_TRES LIKE '%" + ValorNomeAutorUm + "%' OR AUTOR_QUATRO LIKE '%" + ValorNomeAutorUm + "%') " +
				"AND (documentos.TITULO = '" + ValorNomeTitulo + "') " +
				"AND (unidades.COD_UNIDADE = '" + ValorCodigoFaculdade + "')" +
				"AND (documentos.COD_DOCUMENTO <> '" + ValorCodigoDocumento +"');";
			cmd.ExecuteNonQuery();
			DataTable dt = new DataTable();
			MySqlDataAdapter da = new MySqlDataAdapter(cmd);
			da.Fill(dt);
			cn.Close();
			return dt;
		}
		public void InsereDocumento()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "INSERT INTO documentos VALUES " +
				"('" + 0 + "','" 
				+ ValorNomeTitulo.ToUpper() + "','" 
				+ ValorNomeAutorUm.ToUpper() + "','" 
				+ ValorNomeAutorDois.ToUpper() + "','" 
				+ ValorNomeAutorTres.ToUpper() + "','" 
				+ ValorNomeAutorQuatro.ToUpper() + "','" 
				+ ValorPalavraChave.ToUpper() + "','" 
				+ ValorCaminhoPdf + "','" 
				+ ValorCaminhoRar + "','" 
				+ ValorResumo + "','" 
				+ ValorAnoProjeto + "','"
				+ RecebePrimaryKeyCursosUnidades + "');";
			cmd.ExecuteNonQuery();
			cn.Close();
		}
		public void DeletaDocumento()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "DELETE FROM documentos WHERE COD_DOCUMENTO= '" + ValorCodigoDocumento + "';";
			cmd.ExecuteNonQuery();
			cn.Close();
		}
		public void AlteraDocumento()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "UPDATE documentos SET " +
				"TITULO = '" + ValorNomeTitulo.ToUpper() + "'" +
				",AUTOR_UM = '" + ValorNomeAutorUm.ToUpper() + "'" +
				",AUTOR_DOIS = '" + ValorNomeAutorDois.ToUpper() + "'" +
				",AUTOR_TRES = '" + ValorNomeAutorTres.ToUpper() + "'" +
				",AUTOR_QUATRO = '" + ValorNomeAutorQuatro.ToUpper() + "'" +
				",PALAVRA_CHAVE = '" + ValorPalavraChave.ToUpper() + "'" +
				",ano_projeto = '" + ValorAnoProjeto + "'" +
				",PDF = '" + ValorCaminhoPdf + "'" +
				",RAR = '" + ValorCaminhoRar + "'" +
				",resumo = '" + ValorResumo + "' " +
				",FK_CURSO_UNIDADE = '" + Convert.ToInt32(RecebePrimaryKeyCursosUnidades) +"' "+
				" WHERE cod_documento = '" + ValorCodigoDocumento + "';";
			cmd.ExecuteNonQuery();
			cn.Close();
		}
	}
}