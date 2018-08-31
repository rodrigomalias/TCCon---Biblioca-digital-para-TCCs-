using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Net;

namespace TCCon
{
	public partial class monografias : System.Web.UI.Page
	{
		public string
			EnviaNomeAutor,								//Envia o nome do autor para fazer a pesquisa no BD
			EnviaNomeTitulo,							//Envia o nome do titulo para fazer a pesquisa no BD
			EnviaPalavraChave;							//Envia as palavras chave para fazer a pesquisa no BD
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				lbl_cod_unidade.Text = "0";             //Declara que a 'variavel' lbl_cod_unidade terá valor 0
				lbl_cod_curso.Text = "0";               //Declara que a 'variavel' lbl_cod_curso terá valor 0
				cod_faculdade();
				cod_curso();
				list_view();
			}
		}
		//metodo que busca todas as UNIDADES cadastradas no BD e disponibiliza na drop down list
		private void cod_faculdade()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			try
			{
				using (MySqlCommand cmd = new MySqlCommand("SELECT cod_unidade,unidade FROM unidades ORDER BY unidade;"))
				{
					cmd.CommandType = CommandType.Text;
					cmd.Connection = cn;
					cn.Open();
					ddl_faculdade.DataSource = cmd.ExecuteReader();
					ddl_faculdade.DataTextField = "unidade";
					ddl_faculdade.DataValueField = "cod_unidade";
					ddl_faculdade.DataBind();
					cn.Close();
				}
				ddl_faculdade.Items.Insert(0, new ListItem("Selecione uma FATEC", "0"));
			}
			catch
			{
				string msg_erro = "Ops! alguma coisa deu errado.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}
		}
		//metodo que busca todos os cursos cadastrados na UNIDADE escolhida no BD e disponibiliza na drop down list
		private void cod_curso()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			try
			{
				using (MySqlCommand cmd = new MySqlCommand("SELECT cursos.curso, cursos.cod_curso " +
					"FROM cursos_unidades " +
					"INNER JOIN cursos ON (cursos_unidades.fk_curso = cursos.cod_curso) " +
					"INNER JOIN unidades ON (cursos_unidades.fk_unidade = unidades.cod_unidade) " +
					"WHERE cod_unidade = '" + lbl_cod_unidade.Text + "' ORDER BY cursos.curso ;"))
				{
					cmd.CommandType = CommandType.Text;
					cmd.Connection = cn;
					cn.Open();
					ddl_curso.DataSource = cmd.ExecuteReader();
					ddl_curso.DataTextField = "curso";
					ddl_curso.DataValueField = "cod_curso";
					ddl_curso.DataBind();
					cn.Close();
				}
				ddl_curso.Items.Insert(0, new ListItem("Selecione uma FATEC primeiro.", "0"));
			}
			catch
			{
				string msg_erro = "Ops! alguma coisa deu errado.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}
		}
		//metodo que carrega todas as informações de documentos que existir no BD e exibe na list view
		private void list_view()
		{
			try
			{
				ConnectionWithTableDocumentos ConnectionWithTableDocumentos = new ConnectionWithTableDocumentos();
				DataTable dt = ConnectionWithTableDocumentos.VerificaListViewMonografia();
				if (dt.Rows.Count != 0)
				{
					lv_titulos_alunos.DataSource = dt;
					lv_titulos_alunos.DataBind();
					VerificaDownloadListView();
					AvisaErroP.Attributes["class"] = "LabelRemoveMargin";
				}
				else
				{
					//chama o metodo que fará o tratamento dos elses
					LabelAvisaErroElse();
				}
			}
			catch
			{
				string msg_erro = "Ops! alguma coisa deu errado.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}
		}
		//metodo que verifica se existe .RAR disponivel para download, se existir irá exibir o botão de fazer download, caso contrario, deixará ele escondido
		private void VerificaDownloadListView()
		{
			foreach (ListViewItem item in lv_titulos_alunos.Items)
			{
				Label Rar = (Label)item.FindControl("LabelRar");
				LinkButton LinkButtonRar = (LinkButton)item.FindControl("LinkButtonBaixarRar");
				if (Rar.Text.Length == 0)
				{
					LinkButtonRar.Visible = false;
				}
				else
				{
					LinkButtonRar.Visible = true;
				}
			}
		}
		//programação da drop down list de unidades
		private void DdlFaculdadeProgramacao()
		{
			ConnectionWithTableDocumentos ConnectionWithTableDocumentos = new ConnectionWithTableDocumentos();
			//caso a lbl_cod_unidade for igual a 0, a lbl_cod_curso tbm sera 0 e nao exibira nenhum curso
			if (lbl_cod_unidade.Text == "0")
			{
				lbl_cod_curso.Text = "0";
				list_view();
			}
			else
			{
				//se foi selecionado alguma UNIDADE, será chamado o metodo FiltraListViewFaculdade() que pegará todos os cursos desta UNIDADE em especifico
				ConnectionWithTableDocumentos.ValorCodigoFaculdade = lbl_cod_unidade.Text;
				DataTable dt = ConnectionWithTableDocumentos.FiltraListViewFaculdade();
				if (dt.Rows.Count != 0)
				{
					lv_titulos_alunos.DataSource = dt;
					lv_titulos_alunos.DataBind();
					VerificaDownloadListView();
					LabelAvisaErro.Text = "";
					AvisaErroP.Attributes["class"] = "LabelRemoveMargin";
				}
				else
				{
					//chama o metodo que fará o tratamento dos elses
					LabelAvisaErroElse();
				}
			}
		}
		//programação da drop down list de cursos
		private void DdlCursosProgramacao()
		{
			ConnectionWithTableDocumentos ConnectionWithTableDocumentos = new ConnectionWithTableDocumentos();
			//verifica se foi selecionado algum curso
			if (lbl_cod_curso.Text.Length != 0 && lbl_cod_curso.Text != "0")
			{
				//faz a busca pela UNIDADE e CURSO e vai exibir todos os documentos deste curso em especifico nesta unidade
				ConnectionWithTableDocumentos.ValorCodigoFaculdade = lbl_cod_unidade.Text;
				ConnectionWithTableDocumentos.ValorCodigoCurso = lbl_cod_curso.Text;
				DataTable dt = ConnectionWithTableDocumentos.FiltraListViewCurso();
				if (dt.Rows.Count != 0)
				{
					lv_titulos_alunos.DataSource = dt;
					lv_titulos_alunos.DataBind();
					VerificaDownloadListView();
					LabelAvisaErro.Text = "";
					AvisaErroP.Attributes["class"] = "LabelRemoveMargin";
				}
				else
				{
					//chama o metodo que fará o tratamento dos elses
					LabelAvisaErroElse();
				}
			}
			else if (lbl_cod_unidade.Text != "0")
			{
				//se a DdlCursos for selecionada em "0", irá verificar se existe alguma UNIDADE selecionada, 
				//se existir irá fazer a busca por todos os documentos desta UNIDADE
				ConnectionWithTableDocumentos.ValorCodigoFaculdade = lbl_cod_unidade.Text;
				DataTable dt = ConnectionWithTableDocumentos.FiltraListViewFaculdade();
				if (dt.Rows.Count != 0)
				{
					lv_titulos_alunos.DataSource = dt;
					lv_titulos_alunos.DataBind();
					VerificaDownloadListView();
					LabelAvisaErro.Text = "";
					AvisaErroP.Attributes["class"] = "LabelRemoveMargin";
				}
				else
				{
					//chama o metodo que fará o tratamento dos elses
					LabelAvisaErroElse();
				}
			}
			else
			{
				//foi selecionado "0" em ambos, trará a listview de todos os documentos
				list_view();
			}
		}
		private void LabelAvisaErroElse()
		{
			LabelAvisaErro.Text = "Nenhum informação encontrada.";
			//Chama a class no CSS  LabelAvisaErro que colocará a label 30% da margem do topo do site para a mensagem ficar mais evidente
			AvisaErroP.Attributes["class"] = "LabelAvisaErro";
			lv_titulos_alunos.DataBind();
		}
		private void BotaoBuscarProgramacao()
		{
			ConnectionWithTableDocumentos ConnectionWithTableDocumentos = new ConnectionWithTableDocumentos();
			//verifica se o usuário está fazendo a busca utilizando as opções de unidade e curso
			//tanto faz se os 3 estão com dados ou nao, envia para a classe ConnectionWithTableDocumentos e faz a busca
			ConnectionWithTableDocumentos.FiltraListViewAutor = EnviaNomeAutor;
			ConnectionWithTableDocumentos.ValorNomeTitulo = EnviaNomeTitulo;
			ConnectionWithTableDocumentos.ValorPalavraChave = EnviaPalavraChave;
			if (lbl_cod_unidade.Text == "0" && lbl_cod_curso.Text == "0")
			{
				//verifica se os 3 campos disponíveis estão com algum tipo de texto
				if (TxtNomeAutor.Text.Length != 0 || TxtNomeTitulo.Text.Length != 0 || TxtPalavrasChave.Text.Length != 0)
				{					
					DataTable dt = ConnectionWithTableDocumentos.FiltraListViewAutorTituloPalavraChave();
					if (dt.Rows.Count != 0)
					{
						lv_titulos_alunos.DataSource = dt;
						lv_titulos_alunos.DataBind();
						VerificaDownloadListView();
						LabelAvisaErro.Text = "";
						//linha abaixo criada somente para remover a class no CSS do P de LabelAvisaErro caso tenha sido utilizada antes
						AvisaErroP.Attributes["class"] = "LabelRemoveMargin";
					}
					else
					{
						//chama o metodo que fará o tratamento dos elses
						LabelAvisaErroElse();
					}
				}
				else
				{
					//se o usuário não digitou nada nos campos fará a busca sem nenhuma informação
					DataTable dt = ConnectionWithTableDocumentos.FiltraListViewAutorTituloPalavraChave();
					if (dt.Rows.Count != 0)
					{
						lv_titulos_alunos.DataSource = dt;
						lv_titulos_alunos.DataBind();
						VerificaDownloadListView();
						LabelAvisaErro.Text = "";
						//linha abaixo criada somente para remover a class no CSS do P de LabelAvisaErro caso tenha sido utilizada antes
						AvisaErroP.Attributes["class"] = "LabelRemoveMargin";
					}
					else
					{
						//chama o metodo que fará o tratamento dos elses
						LabelAvisaErroElse();
					}
				}
			}
			//foi selecionado somente a drop down list de UNIDADE
			else if (lbl_cod_unidade.Text != "0" && lbl_cod_curso.Text == "0")
			{
				//verifica se os 3 campos disponíveis estão com algum tipo de texto
				if (TxtNomeAutor.Text.Length != 0 || TxtNomeTitulo.Text.Length != 0 || TxtPalavrasChave.Text.Length != 0)
				{
					//tanto faz se os 3 estão com dados ou nao, envia para a classe ConnectionWithTableDocumentos e faz a busca pela UNIDADE tbm
					ConnectionWithTableDocumentos.ValorCodigoFaculdade = lbl_cod_unidade.Text;
					DataTable dt = ConnectionWithTableDocumentos.FiltraListViewFaculdadeAutorTituloPalavraChave();
					if (dt.Rows.Count != 0)
					{
						lv_titulos_alunos.DataSource = dt;
						lv_titulos_alunos.DataBind();
						VerificaDownloadListView();
						LabelAvisaErro.Text = "";
						//linha abaixo criada somente para remover a class no CSS do P de LabelAvisaErro caso tenha sido utilizada antes
						AvisaErroP.Attributes["class"] = "LabelRemoveMargin";
					}
					else
					{
						//chama o metodo que fará o tratamento dos elses
						LabelAvisaErroElse();
					}
				}
				else
				{
					//se o usuário não digitou nada nos campos fará a busca sem nenhuma informação, somente procurando pela unidade
					ConnectionWithTableDocumentos.ValorCodigoFaculdade = lbl_cod_unidade.Text;
					DataTable dt = ConnectionWithTableDocumentos.FiltraListViewFaculdadeAutorTituloPalavraChave();
					if (dt.Rows.Count != 0)
					{
						lv_titulos_alunos.DataSource = dt;
						lv_titulos_alunos.DataBind();
						VerificaDownloadListView();
						LabelAvisaErro.Text = "";
						//linha abaixo criada somente para remover a class no CSS do P de LabelAvisaErro caso tenha sido utilizada antes
						AvisaErroP.Attributes["class"] = "LabelRemoveMargin";
					}
					else
					{
						//chama o metodo que fará o tratamento dos elses
						LabelAvisaErroElse();
					}
				}
			}
			//ambas as drop down list estao selecionadas 
			else if (lbl_cod_unidade.Text != "0" && lbl_cod_curso.Text != "0")
			{
				//verifica se os 3 campos disponíveis estão com algum tipo de texto
				if (TxtNomeAutor.Text.Length != 0 || TxtNomeTitulo.Text.Length != 0 || TxtPalavrasChave.Text.Length != 0)
				{
					//tanto faz se os 3 estão com dados ou nao, envia para a classe ConnectionWithTableDocumentos e faz a busca pela UNIDADE e CURSO tbm
					ConnectionWithTableDocumentos.ValorCodigoFaculdade = lbl_cod_unidade.Text;
					ConnectionWithTableDocumentos.ValorCodigoCurso = lbl_cod_curso.Text;
					DataTable dt = ConnectionWithTableDocumentos.FiltraListViewFaculdadeCursoAutorTituloPalavraChave();
					if (dt.Rows.Count != 0)
					{
						lv_titulos_alunos.DataSource = dt;
						lv_titulos_alunos.DataBind();
						VerificaDownloadListView();
						LabelAvisaErro.Text = "";
						//linha abaixo criada somente para remover a class no CSS do P de LabelAvisaErro caso tenha sido utilizada antes
						AvisaErroP.Attributes["class"] = "LabelRemoveMargin";
					}
					else
					{
						//chama o metodo que fará o tratamento dos elses
						LabelAvisaErroElse();
					}
				}
				else
				{
					//se o usuário não digitou nada nos campos fará a busca sem nenhuma informação, somente procurando pela unidade e curso
					ConnectionWithTableDocumentos.ValorCodigoFaculdade = lbl_cod_unidade.Text;
					ConnectionWithTableDocumentos.ValorCodigoCurso = lbl_cod_curso.Text;
					DataTable dt = ConnectionWithTableDocumentos.FiltraListViewFaculdadeCursoAutorTituloPalavraChave();
					if (dt.Rows.Count != 0)
					{
						lv_titulos_alunos.DataSource = dt;
						lv_titulos_alunos.DataBind();
						VerificaDownloadListView();
						LabelAvisaErro.Text = "";
						//linha abaixo criada somente para remover a class no CSS do P de LabelAvisaErro caso tenha sido utilizada antes
						AvisaErroP.Attributes["class"] = "LabelRemoveMargin";
					}
					else
					{
						//chama o metodo que fará o tratamento dos elses
						LabelAvisaErroElse();
					}
				}
			}
			else
			{
				//nenhuma das opçoes acima ele carregara todos os documentos cadastrados
				list_view();
			}
		}
		protected void btn_buscar_Click(object sender, EventArgs e)
		{
			try
			{
				//atribui os valores do texto para as variaveis
				EnviaNomeAutor = TxtNomeAutor.Text;
				EnviaNomeTitulo = TxtNomeTitulo.Text;
				EnviaPalavraChave = TxtPalavrasChave.Text;
				EnviaPalavraChave = EnviaPalavraChave.Replace(",", " ");	
				//chama a programação do botao
				BotaoBuscarProgramacao();
				if (IsPostBack)
				{
					//se for postback chama a programação do botao novamente
					BotaoBuscarProgramacao();
				}
			}
			catch
			{				
				string msg_erro = "Ops! alguma coisa deu errado.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}
		}

		protected void ddl_faculdade_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				//lbl_cod_unidade recebe o codigo da unidade para fazer outras verificaçoes
				lbl_cod_unidade.Text = ddl_faculdade.SelectedValue;
				//atualiza o drop down list de cursos
				cod_curso();
				//seta o valor anterior da ddl_curso para 0
				ddl_curso.SelectedValue = "0";
				//retira qualquer informaçao dos campos
				TxtNomeAutor.Text = "";
				TxtNomeTitulo.Text = "";
				TxtPalavrasChave.Text = "";
				//chama a programaçao de fato desta DDL
				DdlFaculdadeProgramacao();
				if (IsPostBack)
				{
					//se for postback, chama novamente a programaçao da DDL
					DdlFaculdadeProgramacao();
				}
			}
			catch
			{
				string msg_erro = "Ops! alguma coisa deu errado.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}
		}

		protected void ddl_curso_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				//lbl_cod_curso recebe o codigo do curso para fazer outras verificaçoes
				lbl_cod_curso.Text = ddl_curso.SelectedValue;
				//retira qualquer informaçao dos campos
				TxtNomeAutor.Text = "";
				TxtNomeTitulo.Text = "";
				TxtPalavrasChave.Text = "";
				//chama a programaçao de fato desta DDL
				DdlCursosProgramacao();
				if (IsPostBack)
				{
					//se for postback, chama novamente a programaçao da DDL
					DdlCursosProgramacao();
				}
			}
			catch
			{
				string msg_erro = "Ops! alguma coisa deu errado.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}		
		}

		protected void ddl_faculdade_TextChanged(object sender, EventArgs e)
		{
			//se foi trocado de faculdade, os valores do curso sao resetados
			lbl_cod_curso.Text = "0";
			ddl_curso.SelectedValue = "0";
		}

		protected void lv_titulos_alunos_Load(object sender, EventArgs e)
		{
			//quando a listview carregar já verificará os downloads disponiveis dos documentos
			VerificaDownloadListView();
		}

		protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
		{
			try
			{
				//faz a paginaçao da list view
				(lv_titulos_alunos.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
				if (lbl_cod_unidade.Text == "0")
				{			
					list_view();
				}
				else
				{		
					ConnectionWithTableDocumentos ConnectionWithTableDocumentos = new ConnectionWithTableDocumentos();
					ConnectionWithTableDocumentos.ValorCodigoFaculdade = lbl_cod_unidade.Text;
					DataTable dt = ConnectionWithTableDocumentos.FiltraListViewFaculdade();
					if (dt.Rows.Count != 0)
					{
						lv_titulos_alunos.DataSource = dt;
						lv_titulos_alunos.DataBind();
						LabelAvisaErro.Text = "";
						AvisaErroP.Attributes["class"] = "LabelRemoveMargin";
					}
					else
					{
						LabelAvisaErro.Text = "Nenhum título cadastrado.";
						AvisaErroP.Attributes["class"] = "LabelAvisaErro";
						lv_titulos_alunos.DataBind();
					}				
				}
			}
			catch
			{
				string msg_erro = "Ops! alguma coisa deu errado.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}
		}
		protected void lv_titulos_alunos_ItemCommand(object sender, ListViewCommandEventArgs e)
		{
			if (e.CommandName == "VisualizaPdf")
			{
				Label RecebeCaminhoPdf = (Label)e.Item.FindControl("LabelPdf");
				Label RecebeNomePdf = (Label)e.Item.FindControl("LabelTitulo");
				string
					EnviaCaminhoPdf = RecebeCaminhoPdf.Text,
					EnviaNomePdf = RecebeNomePdf.Text,
					AbrePdf = Server.MapPath(".") + EnviaCaminhoPdf;
				EnviaNomePdf = EnviaNomePdf.Replace(" ", "-") + ".pdf";
				try
				{
					WebClient client = new WebClient();
					Byte[] buffer = client.DownloadData(AbrePdf);
					if (buffer != null)
					{
						Response.ContentType = "application/pdf";
						Response.AppendHeader("Content-Length", buffer.Length.ToString());
						Response.AppendHeader("Content-Disposition", "attachment; filename=" + EnviaNomePdf);
						Response.BinaryWrite(buffer);
						Response.End();
					}
				}
				catch
				{
					string msg_erro = "Ops! alguma coisa deu errado.";
					ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
				}
			}
			if (e.CommandName == "DownloadRar")
			{
				Label RecebeCaminhoRar = (Label)e.Item.FindControl("LabelRar");
				Label RecebeNomeRar = (Label)e.Item.FindControl("LabelTitulo");
				string
					EnviaCaminhoRar = RecebeCaminhoRar.Text,
					EnviaNomeRar = RecebeNomeRar.Text,
					AbreRar = Server.MapPath(".") + EnviaCaminhoRar;
				EnviaNomeRar = EnviaNomeRar.Replace(" ", "-") + ".rar";
				try
				{
					HttpResponse response = HttpContext.Current.Response;
					response.ClearContent();
					response.Clear();
					response.ContentType = "zip/rar";
					response.AddHeader("Content-Disposition", "attachment; filename=" + EnviaNomeRar);
					response.TransmitFile(AbreRar);
					response.Flush();
					response.End();
				}
				catch
				{
					string msg_erro = "Ops! alguma coisa deu errado.";
					ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
				}
			}
		}
	}
}