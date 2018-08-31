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
	public partial class general : System.Web.UI.Page
	{
		//System.Diagnostics.Debug.WriteLine(NomeDaVariavel); (comando que serve para olhar na console que informacao esta chegando em determinada variavel)
		public string
			ValorCodigoFaculdade;					//Recebe o código da faculdade, exemplo: fatec itaquaquecetuba 155
		private string
			ValorSenha;								//Recebe a senha do usuário para verificar qual fatec a combinação de usuario + senha é
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["usuario"] != null)
			{
				lblUsuario.Text = Session["usuario"].ToString();
				ValorSenha = Session["senha"].ToString();
			}
			else
			{
				Response.Redirect("administracao.aspx");
			}
			if (!IsPostBack)
			{
				FaculdadeData();
				Session["faculdadeID"] = ValorCodigoFaculdade;
				Session["faculdadeNome"] = lblNomeUnidade1024px.Text;
			}	
		}
		private void FaculdadeData()
		{
			try
			{
				ConnectionWithTableLogin ConnectionWithTableLogin = new ConnectionWithTableLogin();
				ConnectionWithTableUnidade ConnectionWithTableUnidade = new ConnectionWithTableUnidade();
				//envia os valores de usuario e senha para a classe ConnectionWithTableLogin
				ConnectionWithTableLogin.ValorLogin = lblUsuario.Text;
				ConnectionWithTableLogin.ValorSenha = ValorSenha;
				//utiliza o metodo RecebeIdFaculdade atraves do usuario e senha para saber qual ID da faculdade que esta acessando 
				//(cada combinacao de USUARIO e SENHA sao unicos no sistema)
				ConnectionWithTableLogin.RecebeIdFaculdade();
				//variavel IdFaculdade recebe o valor do ID DA FACULDADE do metodo
				ValorCodigoFaculdade = ConnectionWithTableLogin.ValorCodigoFaculdade;
				//metodo que recebe o nome da faculdade para exibir para o usuario no sistema
				ConnectionWithTableUnidade.ValorCodigoFaculdade = ValorCodigoFaculdade;
				ConnectionWithTableUnidade.RecebeNomeFaculdade();
				lblNomeUnidade1024px.Text = ConnectionWithTableUnidade.ValorNomeFaculdade;
				lblNomeUnidade1023px.Text = ConnectionWithTableUnidade.ValorNomeFaculdade;
			}
			catch 
			{
				string msg_erro = "Ops! alguma coisa deu errado.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}
		}
		protected void btnLogout_Click(object sender, EventArgs e)
		{
			Session.RemoveAll();
			Response.Redirect("index.aspx");
		}
	}
}