using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace TCCon
{
	public class ConnectionWithTableUnidade
	{
		public string
			ValorNomeFaculdade,
			ValorCodigoFaculdade;
		DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
		public string RecebeNomeFaculdade()
		{
			MySqlConnection cn = DatabaseConnectionClass.connect();
			cn.Open();
			MySqlCommand busca_nome_faculdadeCMD = new MySqlCommand();
			busca_nome_faculdadeCMD.CommandText = ("SELECT unidade FROM unidades WHERE cod_unidade = '" + ValorCodigoFaculdade + "';");
			busca_nome_faculdadeCMD.CommandType = CommandType.Text;
			busca_nome_faculdadeCMD.Connection = cn;
			MySqlDataReader busca_nome_faculdadeDR;
			busca_nome_faculdadeDR = busca_nome_faculdadeCMD.ExecuteReader();
			busca_nome_faculdadeDR.Read();
			ValorNomeFaculdade = Convert.ToString(busca_nome_faculdadeDR.GetString(0));			
			cn.Close();
			return ValorNomeFaculdade;
		}
	}
}