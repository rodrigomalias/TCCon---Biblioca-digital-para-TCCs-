using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace TCCon
{
	public partial class administracao : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		protected void btnLogin_Click(object sender, EventArgs e)
		{		
			try
			{
				//faz a conexão com a tabela de login e faz a validação do usuário e senha
				ConnectionWithTableLogin ConnectionWithTableLogin = new ConnectionWithTableLogin();
				ConnectionWithTableLogin.ValorLogin = txtLogin.Text;
				ConnectionWithTableLogin.ValorSenha = txtSenha.Text;
				DataTable dt = ConnectionWithTableLogin.VerificaLoginSenha();
				if (dt.Rows.Count.ToString() == "1")
				{
					Session["usuario"] = txtLogin.Text;
					Session["senha"] = txtSenha.Text;
					Response.Redirect("general.aspx");
				}
				else
				{
					lblAviso.Text = " Usuário ou Senha estão incorretos. ";
					txtSenha.Text = "";
					txtLogin.Focus();
				}
			}
			catch
			{
				string msg_erro = "Ops! alguma coisa deu errado.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}
		}	
	}
}
