using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace TCCon
{
	public class ConnectionWithTableLogin
	{
		public string
			ValorLogin,							//Recebe o usuário digitado
			ValorSenha,							//Recebe a senha digitada
			ValorCodigoFaculdade;				//Recebe o valor do código da faculdade resultante da combinação de usuário e senha digitado
		public DataTable VerificaLoginSenha()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "SELECT * FROM login WHERE usuario= '" + ValorLogin + "' AND BINARY senha= '" + ValorSenha + "';";
			cmd.ExecuteNonQuery();
			DataTable dt = new DataTable();
			MySqlDataAdapter da = new MySqlDataAdapter(cmd);
			da.Fill(dt);
			cn.Close();
			return dt;			
		}
		public string RecebeIdFaculdade()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand busca_cod_faculdadeCMD = new MySqlCommand();
			busca_cod_faculdadeCMD.CommandText = ("SELECT fk_unidade_login FROM login WHERE usuario = '" + ValorLogin + "' AND senha = '" + ValorSenha + "';");
			busca_cod_faculdadeCMD.CommandType = CommandType.Text;
			busca_cod_faculdadeCMD.Connection = cn;
			MySqlDataReader busca_cod_faculdadeDR;
			busca_cod_faculdadeDR = busca_cod_faculdadeCMD.ExecuteReader();
			busca_cod_faculdadeDR.Read();
			ValorCodigoFaculdade = Convert.ToString(busca_cod_faculdadeDR.GetInt32(0));
			cn.Close();
			return ValorCodigoFaculdade;
		}
	}
}