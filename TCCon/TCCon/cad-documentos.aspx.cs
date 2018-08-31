using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;
namespace TCCon
{
	public partial class cad_documentos : System.Web.UI.Page
	{
		public string
			ValorVerificaDownloadRar,
			ValorCodigoDocumento,                           //recebe o valor do codigo do documento
			ValorCodigoFaculdade,                           //recebe o valor do codigo da faculdade
			ValidaDataEntrega,                              //variavel para verificar se a data esta correta                        
			EnviaBancoDeDadosRar,                           //envia o caminho do RAR para o banco de dados
			EnviaBancoDeDadosPdf,                           //envia o caminho do PDF para o banco de dados
			RecebePrimaryKeyCursosUnidades,                 //recebe a primary key de CURSOS UNIDADES para a chave primaria deste curso e unidade no BD
			RecebeUltimoCodigoDocumento;					//recebe o ultimo codigo de documento cadastro para poder fazer a insercao na tabela de DocumentosCursos
		protected void Page_Load(object sender, EventArgs e)
		{
			LinkButtonPdf.Attributes.Add("onclick", "document.getElementById('" + upload_pdf.ClientID + "').click(); return false;");
			LinkButtonRar.Attributes.Add("onclick", "document.getElementById('" + upload_rar.ClientID + "').click(); return false;");
			if (Session["usuario"] != null)
			{
				lblUsuario.Text = Session["usuario"].ToString();
				ValorCodigoFaculdade = Session["faculdadeID"].ToString();
				lblNomeUnidade1024px.Text = Session["faculdadeNome"].ToString();
				lblNomeUnidade1023px.Text = Session["faculdadeNome"].ToString();
			}
			else
			{
				Response.Redirect("administracao.aspx");
			}
			if (!IsPostBack)
			{
				cod_curso();
				list_view();
			}
			txt_data_entrega.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");

		}
		public void LimparTodosTextBox(Control controle)
		{
			foreach (Control ctle in controle.Controls)
			{
				if (ctle is TextBox)
				{
					((TextBox)ctle).Text = string.Empty;
				}
				else if (ctle.Controls.Count > 0)
				{
					LimparTodosTextBox(ctle);
					ddl_cursos_fatec.SelectedIndex = 0;
					LabelRecebeVerificaDataEntrega.Text = "";
					LabelRecebeVerificaPdf.Text = "";
					LabelRecebeVerificaRar.Text = "";
					LabelCodigoCurso.Text = "";
					LabelComparaCodigoDocumento.Text = "";
					LabelComparaNomeTitulo.Text = "";
					LabelAvisaErro.Text = "";
				}
			}
		}
		//metodo para validar a data que o usuario tentar colocar.
		//UltimaData setada para o ano da primeira fatec a ser construida
		//DataAtual setada para ser o ano 'atual'
		//DataEscritaComparar setada para receber a data que o usuário escrever e fazer a comparação entre a mínima(UltimaData) e a máxima (DataAtual)
		private bool ValidaData()
		{
			int 
				UltimaData = 1973,
				DataAtual = DateTime.Now.Year,
				DataEscritaComparar = Convert.ToInt32(txt_data_entrega.Text);
			//verifica se a data é maior que a atual e se é menor que data da primeira fatec registrada.
			if (DataEscritaComparar > UltimaData && DataEscritaComparar <= DataAtual)
			{
				LabelRecebeVerificaDataEntrega.Text = txt_data_entrega.Text;
				return true;
			}
			else
			{
				LabelAvisaErro.Text = "Ano de entrega precisa ser entre 1973 e " + DataAtual + ".";
				return false;
			}
		}
		//metodo para validar o arquivo selecionado na upload_rar, verifica se é de tipo .rar ou .zip, se nao for, envia um aviso de erro
		private bool VerificaRar()
		{			
			if (upload_rar.HasFiles)
			{
				string
					TrocaNomeRar = upload_rar.PostedFile.FileName,
					PegarExtensaoRar = System.IO.Path.GetExtension(TrocaNomeRar);

				if (PegarExtensaoRar.ToLower() != ".rar" && PegarExtensaoRar.ToLower() != ".zip")
				{
					LabelAvisaErro.Text = "Somente upload de .rar ou .zip são permitidos.";
					return false;
				}
				else
				{
					//troca o nome do arquivo para o nome do titulo do projeto e salva o arquivo no servidor e envia a string de recuperaçao do arquivo para o BD
					TrocaNomeRar = txt_nome_titulo.Text + "-" + txt_data_entrega.Text + PegarExtensaoRar;
					TrocaNomeRar = TrocaNomeRar.Replace(" ", "-");
					upload_rar.PostedFile.SaveAs(Server.MapPath(".") + "//uploads//rar//" + TrocaNomeRar);
					EnviaBancoDeDadosRar = "//uploads//rar//" + TrocaNomeRar.ToString();
					LabelRecebeVerificaRar.Text = EnviaBancoDeDadosRar;
					return true;
				}
			}
			else
			{
				return true;
			}
		}
		//metodo para validar o arquivo selecionado na upload_pdf, verifica se é do tipo .pdf, se nao for, envia um aviso de erro
		private bool VerificaPdf()
		{
			string
				PegarExtensaoPdf = System.IO.Path.GetExtension(upload_pdf.FileName),
				TrocaNomePdf = upload_pdf.FileName;
			//pega a extensao do arquivo e o nome do arquivo e verifica se a extensao é valida

			if (PegarExtensaoPdf.ToLower() != ".pdf")
			{
				LabelAvisaErro.Text = "Somente upload de .pdfs são permitidos.";
				return false;
			}
			else
			{
				//troca o nome do arquivo para o nome do titulo do projeto e salva o arquivo no servidor e envia a string de recuperaçao do arquivo para o BD
				TrocaNomePdf = txt_nome_titulo.Text + "-" + txt_data_entrega.Text + PegarExtensaoPdf;
				TrocaNomePdf = TrocaNomePdf.Replace(" ", "-");
				upload_pdf.PostedFile.SaveAs(Server.MapPath(".") + "//uploads//pdf//" + TrocaNomePdf);
				EnviaBancoDeDadosPdf = "//uploads//pdf//" + TrocaNomePdf.ToString();
				LabelRecebeVerificaPdf.Text = EnviaBancoDeDadosPdf;
				return true;
			}
		}
		//metodo para buscar todos os CURSOS dessa fatec
		private void cod_curso()
		{
			DatabaseConnection DatabaseConnectionClass = new DatabaseConnection();
			MySqlConnection cn = DatabaseConnectionClass.connect();
			try
			{
				using (MySqlCommand cmd = new MySqlCommand("select cursos.CURSO, cursos.cod_curso from cursos_unidades inner join cursos on (cursos_unidades.fk_curso = cursos.cod_curso) inner join unidades on (cursos_unidades.fk_unidade = unidades.cod_unidade) where cod_unidade = '" + ValorCodigoFaculdade + "' order by cursos.curso;"))
				{
					cmd.CommandType = CommandType.Text;
					cmd.Connection = cn;
					cn.Open();
					ddl_cursos_fatec.DataSource = cmd.ExecuteReader();
					ddl_cursos_fatec.DataTextField = "CURSO";
					ddl_cursos_fatec.DataValueField = "cod_curso";
					ddl_cursos_fatec.DataBind();
					cn.Close();

					cn.Open();
					ddl_FiltrarCurso.DataSource = cmd.ExecuteReader();
					ddl_FiltrarCurso.DataTextField = "CURSO";
					ddl_FiltrarCurso.DataValueField = "cod_curso";
					ddl_FiltrarCurso.DataBind();
					cn.Close();
				}
				ddl_cursos_fatec.Items.Insert(0, new ListItem("Selecione um CURSO.", "0"));
				ddl_FiltrarCurso.Items.Insert(0, new ListItem("Selecione um CURSO.", "0"));
			}
			catch
			{
				string msg_erro = "Ops! alguma coisa deu errado.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}
		}
		//metodo que verifica se exibe ou não o botão de download RAR
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
		//metodo que atualiza a listview
		private void list_view()
		{
			try
			{
				ConnectionWithTableDocumentos ConnectionWithTableDocumentos = new ConnectionWithTableDocumentos();
				ConnectionWithTableDocumentos.ValorCodigoFaculdade = ValorCodigoFaculdade;
				DataTable dt = ConnectionWithTableDocumentos.VerificaListViewVazia();
				if (dt.Rows.Count != 0)
				{
					lv_titulos_alunos.DataSource = dt;
					lv_titulos_alunos.DataBind();
					VerificaDownloadListView();
				}
				else
				{
					LabelAvisaErro.Text = "Cadastre um título!";
					lv_titulos_alunos.DataBind();
				}
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

		protected void btnVoltar_Click(object sender, EventArgs e)
		{
			Response.Redirect("general.aspx");
		}
		protected void btn_cad_documentos_Click(object sender, EventArgs e)
		{
			if (LabelCodigoCurso.Text.Length == 0 || ddl_cursos_fatec.SelectedIndex == 0)
			{
				LabelAvisaErro.Text = "Curso inválido.";
				ddl_cursos_fatec.Focus();
			}
			else if (txt_nome_titulo.Text.Length < 4 || txt_nome_titulo.Text.Length > 200)
			{
				LabelAvisaErro.Text = "Nome do título precisa ser maior que 4 caracteres e menor que 200.";
				txt_nome_titulo.Focus();
			}
			else if (txt_data_entrega.Text.Length != 4)
			{
				LabelAvisaErro.Text = "Data de entrega somente com 4 digitos.";
				txt_data_entrega.Focus();
			}
			else if (txt_palavras_chave.Text.Length < 4 || txt_palavras_chave.Text.Length > 200)
			{
				LabelAvisaErro.Text = "Quantidade de caracteres precisa ser maior que 4 e menor que 200.";
				txt_palavras_chave.Focus();
			}
			else if (txt_resumo.Text.Length < 10 || txt_resumo.Text.Length > 5000)
			{
				LabelAvisaErro.Text = "Quantidade de caracteres precisa ser maior que 10 e menor que 5000.";
				txt_resumo.Focus();
			}
			else if (txt_primeiro_autor.Text.Length < 4 || txt_primeiro_autor.Text.Length > 200)
			{
				LabelAvisaErro.Text = "Nome do autor precisa ser maior que 4 caracteres e menor que 200.";
				txt_primeiro_autor.Focus();
			}
			else if (txt_segundo_autor.Text.Length > 0 && txt_segundo_autor.Text.Length < 4 || txt_segundo_autor.Text.Length > 200)
			{
				LabelAvisaErro.Text = "Nome do autor precisa ser maior que 4 caracteres e menor que 200.";
				txt_segundo_autor.Focus();
			}
			else if (txt_terceiro_autor.Text.Length > 0 && txt_terceiro_autor.Text.Length < 4 || txt_terceiro_autor.Text.Length > 200)
			{
				LabelAvisaErro.Text = "Nome do autor precisa ser maior que 4 caracteres e menor que 200.";
				txt_terceiro_autor.Focus();
			}
			else if (txt_quarto_autor.Text.Length > 0 && txt_quarto_autor.Text.Length < 4 || txt_quarto_autor.Text.Length > 200)
			{
				LabelAvisaErro.Text = "Nome do autor precisa ser maior que 4 caracteres e menor que 200.";
				txt_quarto_autor.Focus();
			}
			else if (!upload_pdf.HasFile)
			{
				LabelAvisaErro.Text = "Por favor selecione um .PDF.";
				LinkButtonPdf.Focus();
			}
			else
			{
				//envia os dados de CODIGO DO DOCUMENTO, CODIGO DA FACULDADE, NOME DO TITULO e DATA DO PROJETO para verificar se este titulo já existe
				ConnectionWithTableDocumentos ConnectionWithTableDocumentos = new ConnectionWithTableDocumentos();
				ConnectionWithTableDocumentos.ValorCodigoDocumento = txt_cod_documento.Text;
				ConnectionWithTableDocumentos.ValorCodigoFaculdade = ValorCodigoFaculdade;
				ConnectionWithTableDocumentos.ValorNomeTitulo = txt_nome_titulo.Text;
				ConnectionWithTableDocumentos.ValorNomeAutorUm = txt_primeiro_autor.Text;
				//traz um datable com a listagem dos itens acima e verifica se existe algum
				DataTable VerificaTituloPrimeiroAutorDocumentoDt = ConnectionWithTableDocumentos.VerificaTituloPrimeiroAutorDocumentoCadastrar();
				if (VerificaTituloPrimeiroAutorDocumentoDt.Rows.Count.ToString() != "0")
				{
					LabelAvisaErro.Text = "Este documento já existe!";
					txt_nome_titulo.Focus();
				}
				else
				{
					//verifica o tipo dos arquivos e se estão de corretos
					//valida a data para enviar para o BD
					VerificaRar();
					VerificaPdf();
					ValidaData();
					if (VerificaRar() && VerificaPdf() && ValidaData())
					{						
						//retira todas as , para não gerar problema na hora de trazer os dados para a listview
						txt_palavras_chave.Text = txt_palavras_chave.Text.Replace("   ", " ");
						txt_palavras_chave.Text = txt_palavras_chave.Text.Replace("  ", " ");
						txt_palavras_chave.Text = txt_palavras_chave.Text.Replace(",", " ");
						//envia os valores dos campos para as variaveis de inserçao no banco de dados						
						ConnectionWithTableDocumentos.ValorNomeAutorDois = txt_segundo_autor.Text;
						ConnectionWithTableDocumentos.ValorNomeAutorTres = txt_terceiro_autor.Text;
						ConnectionWithTableDocumentos.ValorNomeAutorQuatro = txt_quarto_autor.Text;
						ConnectionWithTableDocumentos.ValorAnoProjeto = txt_data_entrega.Text;
						ConnectionWithTableDocumentos.ValorPalavraChave = txt_palavras_chave.Text;
						ConnectionWithTableDocumentos.ValorCaminhoPdf = EnviaBancoDeDadosPdf;
						ConnectionWithTableDocumentos.ValorCaminhoRar = EnviaBancoDeDadosRar;
						txt_resumo.Text = txt_resumo.Text.Replace("\n", "<br />");
						ConnectionWithTableDocumentos.ValorResumo = txt_resumo.Text;
						ConnectionWithTableCursosUnidades ConnectionWithTableCursosUnidades = new ConnectionWithTableCursosUnidades();
						//envia os valores de CODIGO DA FACULDADE e CODIGO DO CURSO para buscar a primary key
						ConnectionWithTableCursosUnidades.ValorCodigoCurso = LabelCodigoCurso.Text;
						ConnectionWithTableCursosUnidades.ValorCodigoFaculdade = ValorCodigoFaculdade;
						//chama o metodo que busca a primary key desse curso nessa unidade
						ConnectionWithTableCursosUnidades.RecebeCodigoPrimaryKeyCursosUnidades();
						//RecebePrimaryKeyCursosUnidades recebe o valor da primary key do metodo acima
						RecebePrimaryKeyCursosUnidades = ConnectionWithTableCursosUnidades.RecebePrimaryKeyCursosUnidades;
						ConnectionWithTableDocumentos.RecebePrimaryKeyCursosUnidades = RecebePrimaryKeyCursosUnidades;
						//cadastra o documento na tabela de documentos
						ConnectionWithTableDocumentos.InsereDocumento();
						//chama o metodo para limpar todas as textbox
						LimparTodosTextBox(this);
						//atualiza a list view
						list_view();
						string msg_cadastra = "Documento cadastrado!";
						ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.confirm(\"" + msg_cadastra + "\");", true);					
					}
				}
			}
		}
		protected void btn_alt_documentos_Click(object sender, EventArgs e)
		{
			if (LabelComparaCodigoDocumento.Text == "")
			{
				LabelAvisaErro.Text = "Por favor selecione um documento.";
			}
			else if (LabelCodigoCurso.Text.Length == 0 || ddl_cursos_fatec.SelectedIndex == 0)
			{
				LabelAvisaErro.Text = "Curso inválido.";
				ddl_cursos_fatec.Focus();
			}
			else if (txt_nome_titulo.Text.Length < 5 || txt_nome_titulo.Text.Length > 200)
			{
				LabelAvisaErro.Text = "Nome do título precisa ser maior que 4 caracteres e menor que 200.";
				txt_nome_titulo.Focus();
			}
			else if (txt_primeiro_autor.Text.Length < 5 || txt_primeiro_autor.Text.Length > 200)
			{
				LabelAvisaErro.Text = "Nome do autor precisa ser maior que 4 caracteres e menor que 200.";
				txt_primeiro_autor.Focus();
			}
			else if (txt_resumo.Text.Length < 10 || txt_resumo.Text.Length > 5000)
			{
				LabelAvisaErro.Text = "Quantidade de caracteres precisa ser maior que 10 e menor que 5000.";
				txt_resumo.Focus();
			}
			else if (txt_segundo_autor.Text.Length > 0 && txt_segundo_autor.Text.Length < 5 || txt_segundo_autor.Text.Length > 200)
			{
				LabelAvisaErro.Text = "Nome do autor precisa ser maior que 4 caracteres e menor que 200.";
				txt_segundo_autor.Focus();
			}
			else if (txt_terceiro_autor.Text.Length > 0 && txt_terceiro_autor.Text.Length < 5 || txt_terceiro_autor.Text.Length > 200)
			{
				LabelAvisaErro.Text = "Nome do autor precisa ser maior que 4 caracteres e menor que 200.";
				txt_terceiro_autor.Focus();
			}
			else if (txt_quarto_autor.Text.Length > 0 && txt_quarto_autor.Text.Length < 5 || txt_quarto_autor.Text.Length > 200)
			{
				LabelAvisaErro.Text = "Nome do autor precisa ser maior que 4 caracteres e menor que 200.";
				txt_quarto_autor.Focus();
			}
			else if (txt_palavras_chave.Text.Length < 5 || txt_palavras_chave.Text.Length > 200)
			{
				LabelAvisaErro.Text = "Quantidade de caracteres precisa ser maior que 4 e menor que 200.";
				txt_palavras_chave.Focus();
			}
			else if (txt_data_entrega.Text.Length != 4)
			{
				LabelAvisaErro.Text = "Ano da entrega somente com 4 digitos.";
				txt_data_entrega.Focus();
			}
			else
			{
				//envia os dados de CODIGO DO DOCUMENTO, CODIGO DA FACULDADE, NOME DO TITULO e DATA DO PROJETO para verificar se este titulo já existe
				ConnectionWithTableDocumentos ConnectionWithTableDocumentos = new ConnectionWithTableDocumentos();
				ConnectionWithTableDocumentos.ValorCodigoDocumento = txt_cod_documento.Text;
				ConnectionWithTableDocumentos.ValorCodigoFaculdade = ValorCodigoFaculdade;
				ConnectionWithTableDocumentos.ValorNomeTitulo = txt_nome_titulo.Text;
				ConnectionWithTableDocumentos.ValorAnoProjeto = txt_data_entrega.Text;
				//traz um datable com a listagem dos itens acima e verifica se existe algum
				DataTable VerificaTituloPrimeiroAutorDocumentoDt = ConnectionWithTableDocumentos.VerificaTituloPrimeiroAutorDocumentoAlterar();
				if (VerificaTituloPrimeiroAutorDocumentoDt.Rows.Count.ToString() != "0")
				{
					LabelAvisaErro.Text = "Este documento já existe!";
					txt_nome_titulo.Focus();
				}
				else
				{
					ConnectionWithTableCursosUnidades ConnectionWithTableCursosUnidades = new ConnectionWithTableCursosUnidades();
					//envia os valores de CODIGO DA FACULDADE e CODIGO DO CURSO para buscar a primary key
					ConnectionWithTableCursosUnidades.ValorCodigoCurso = LabelCodigoCurso.Text;
					ConnectionWithTableCursosUnidades.ValorCodigoFaculdade = ValorCodigoFaculdade;
					//chama o metodo que busca a primary key desse curso nessa unidade
					ConnectionWithTableCursosUnidades.RecebeCodigoPrimaryKeyCursosUnidades();
					//RecebePrimaryKeyCursosUnidades recebe o valor da primary key do metodo acima
					RecebePrimaryKeyCursosUnidades = ConnectionWithTableCursosUnidades.RecebePrimaryKeyCursosUnidades;
					ConnectionWithTableDocumentos.RecebePrimaryKeyCursosUnidades = RecebePrimaryKeyCursosUnidades;
					//envia todos os dados do documento para a classe ConnectionWithTableDocumentos, exceto o caminho do PDF e do RAR
					ConnectionWithTableDocumentos.ValorNomeAutorUm = txt_primeiro_autor.Text;
					ConnectionWithTableDocumentos.ValorNomeAutorDois = txt_segundo_autor.Text;
					ConnectionWithTableDocumentos.ValorNomeAutorTres = txt_terceiro_autor.Text;
					ConnectionWithTableDocumentos.ValorNomeAutorQuatro = txt_quarto_autor.Text;
					//ordena por ordem alfabetica e retira as , para não gerar problema na hora de chamar na list view
					txt_palavras_chave.Text = txt_palavras_chave.Text.Replace("   ", " ");
					txt_palavras_chave.Text = txt_palavras_chave.Text.Replace("  ", " ");
					txt_palavras_chave.Text = txt_palavras_chave.Text.Replace(",", " ");
					ConnectionWithTableDocumentos.ValorPalavraChave = txt_palavras_chave.Text;
					txt_resumo.Text = txt_resumo.Text.Replace("\n", "<br />");
					ConnectionWithTableDocumentos.ValorResumo = txt_resumo.Text;
					if (!upload_pdf.HasFile && !upload_rar.HasFiles)
					{
						//envia os caminhos de PDF e RAR caso nao haja nenhuma alteração neles
						ConnectionWithTableDocumentos.ValorCaminhoPdf = LabelRecebeVerificaPdf.Text;
						ConnectionWithTableDocumentos.ValorCaminhoRar = LabelRecebeVerificaRar.Text;
						//chama o metodo para alterar na tabela de Documentos
						ConnectionWithTableDocumentos.AlteraDocumento();
						//chama o metodo para limpar todas as textbox
						LimparTodosTextBox(this);
						//atualiza a list view
						list_view();
						string msg_altera = "Documento alterado!";
						ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.confirm(\"" + msg_altera + "\");", true);
						
					}
					else if (upload_pdf.HasFile && upload_rar.HasFile)
					{
						VerificaPdf();
						VerificaRar();
						if (VerificaPdf() && VerificaRar())
						{
							//envia os caminhos de PDF e RAR caso haja alguma alteração no upload de PDF
							ConnectionWithTableDocumentos.ValorCaminhoPdf = LabelRecebeVerificaPdf.Text;
							ConnectionWithTableDocumentos.ValorCaminhoRar = LabelRecebeVerificaRar.Text;
							//chama o metodo de alterar na tabela de Documentos
							ConnectionWithTableDocumentos.AlteraDocumento();
							//chama o metodo para limpar todas as textbox
							LimparTodosTextBox(this);
							//atualiza a list view
							list_view();
							string msg_altera = "Documento alterado!";
							ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.confirm(\"" + msg_altera + "\");", true);
							
						}
					}
					else if (upload_pdf.HasFile)
					{
						VerificaPdf();
						if (VerificaPdf())
						{
							//envia os caminhos de PDF e RAR caso haja alguma alteração no upload de PDF
							ConnectionWithTableDocumentos.ValorCaminhoPdf = LabelRecebeVerificaPdf.Text;
							ConnectionWithTableDocumentos.ValorCaminhoRar = LabelRecebeVerificaRar.Text;
							//chama o metodo de alterar na tabela de Documentos
							ConnectionWithTableDocumentos.AlteraDocumento();
							//chama o metodo para limpar todas as textbox
							LimparTodosTextBox(this);
							//atualiza a list view
							list_view();
							string msg_altera = "Documento alterado!";
							ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.confirm(\"" + msg_altera + "\");", true);
						}
					}
					else if (upload_rar.HasFile)
					{
						VerificaRar();
						if (VerificaRar())
						{
							//envia os caminhos de PDF e RAR caso haja alguma alteração no upload de RAR
							ConnectionWithTableDocumentos.ValorCaminhoPdf = LabelRecebeVerificaPdf.Text;
							ConnectionWithTableDocumentos.ValorCaminhoRar = LabelRecebeVerificaRar.Text;
							//chama o metodo de alterar na tabela de Documentos
							ConnectionWithTableDocumentos.AlteraDocumento();
							//chama o metodo para limpar todas as textbox
							LimparTodosTextBox(this);
							//atualiza a list view
							list_view();
							string msg_altera = "Documento alterado!";
							ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.confirm(\"" + msg_altera + "\");", true);
						}
					}
				}
			}
		}

		protected void btn_cancel_documentos_Click(object sender, EventArgs e)
		{
			try
			{
				LimparTodosTextBox(this);
			}
			catch
			{
				string msg_erro = "Ops! alguma coisa deu errada.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}
		}

		protected void btn_exc_documentos_Click(object sender, EventArgs e)
		{
			try
			{
				if (LabelComparaCodigoDocumento.Text == "" || LabelComparaNomeTitulo.Text == "")
				{
					LabelAvisaErro.Text = "Selecione um documento para excluir.";
				}
				else
				{
					ConnectionWithTableDocumentos ConnectionWithTableDocumentos = new ConnectionWithTableDocumentos();
					ConnectionWithTableCursosUnidades ConnectionWithTableCursosUnidades = new ConnectionWithTableCursosUnidades();
					//envia os valores do CODIGO DA FACULDADE e CODIGO DO CURSO para a classe ConnectionWithTableCursosUnidades busca a PRIMARY KEY desses valores no BD
					ConnectionWithTableCursosUnidades.ValorCodigoFaculdade = ValorCodigoFaculdade;
					ConnectionWithTableCursosUnidades.ValorCodigoCurso = LabelCodigoCurso.Text;
					//chama o metodo para receber a primary key de CURSOS_UNIDADES
					ConnectionWithTableCursosUnidades.RecebeCodigoPrimaryKeyCursosUnidades();
					RecebePrimaryKeyCursosUnidades = ConnectionWithTableCursosUnidades.RecebePrimaryKeyCursosUnidades;
					//envia o valor do CÓDIGO DO DOCUMENTO e chama o metodo para deletar o documento
					ConnectionWithTableDocumentos.ValorCodigoDocumento = LabelComparaCodigoDocumento.Text;
					ConnectionWithTableDocumentos.DeletaDocumento();
					LimparTodosTextBox(this);
					list_view();
					string msg_erro_delete = "Documento excluído!";
					ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro_delete + "\");", true);
					if (IsPostBack)
					{
						list_view();
					}
				}	
			}
			catch 
			{
				string msg_erro = "Ops! alguma coisa deu errada.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}			
		}

		protected void ddl_cursos_fatec_SelectedIndexChanged(object sender, EventArgs e)
		{
			LabelCodigoCurso.Text = ddl_cursos_fatec.SelectedValue;
		}

		protected void lv_titulos_alunos_Load(object sender, EventArgs e)
		{
			VerificaDownloadListView();
		}

		protected void lv_titulos_alunos_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
		{
			try
			{
				Label CodigoDocumento = lv_titulos_alunos.Items[e.NewSelectedIndex].FindControl("LabelCodigoDocumento") as Label;
				Label CodigoCurso = lv_titulos_alunos.Items[e.NewSelectedIndex].FindControl("LabelCodigoCurso") as Label;
				Label NomeTitulo = lv_titulos_alunos.Items[e.NewSelectedIndex].FindControl("LabelTitulo") as Label;
				Label NomeCurso = lv_titulos_alunos.Items[e.NewSelectedIndex].FindControl("LabelNomeCurso") as Label;
				Label DataProjeto = lv_titulos_alunos.Items[e.NewSelectedIndex].FindControl("LabelDataProjeto") as Label;
				Label AutorUm = lv_titulos_alunos.Items[e.NewSelectedIndex].FindControl("LabelAutorUm") as Label;
				Label AutorDois = lv_titulos_alunos.Items[e.NewSelectedIndex].FindControl("LabelAutorDois") as Label;
				Label AutorTres = lv_titulos_alunos.Items[e.NewSelectedIndex].FindControl("LabelAutorTres") as Label;
				Label AutorQuatro = lv_titulos_alunos.Items[e.NewSelectedIndex].FindControl("LabelAutorQuatro") as Label;
				Label PalavraChave = lv_titulos_alunos.Items[e.NewSelectedIndex].FindControl("LabelPalavraChave") as Label;
				Label Pdf = lv_titulos_alunos.Items[e.NewSelectedIndex].FindControl("LabelPdf") as Label;
				Label Rar = lv_titulos_alunos.Items[e.NewSelectedIndex].FindControl("LabelRar") as Label;
				Label Resumo = lv_titulos_alunos.Items[e.NewSelectedIndex].FindControl("LabelResumo") as Label;
				Resumo.Text = Resumo.Text.Replace("<br />", "\n");			
				txt_cod_documento.Text = CodigoDocumento.Text;
				LabelComparaCodigoDocumento.Text = CodigoDocumento.Text; //'variavel' para comparar com a principal e ver se o usuario trocou o codigo do documento
				LabelCodigoCurso.Text = CodigoCurso.Text;
				txt_nome_titulo.Text = NomeTitulo.Text;
				LabelComparaNomeTitulo.Text = NomeTitulo.Text;			  //'variavel' para comparar com a principal e ver se o usuario trocou o nome do documento
				ddl_cursos_fatec.SelectedValue = LabelCodigoCurso.Text;
				txt_data_entrega.Text = DataProjeto.Text;
				txt_primeiro_autor.Text = AutorUm.Text;
				txt_segundo_autor.Text = AutorDois.Text;
				txt_terceiro_autor.Text = AutorTres.Text;
				txt_quarto_autor.Text = AutorQuatro.Text;
				txt_palavras_chave.Text = PalavraChave.Text;
				txt_resumo.Text = Resumo.Text;
				LabelRecebeVerificaPdf.Text = Pdf.Text;
				LabelRecebeVerificaRar.Text = Rar.Text;			
				lv_titulos_alunos.Items.Clear();				
			}
			catch 
			{
				string msg_erro = "Ops! alguma coisa deu errada.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}
		}

		protected void ddl_FiltrarCurso_SelectedIndexChanged(object sender, EventArgs e)
		{
			LabelCodigoCurso.Text = ddl_FiltrarCurso.SelectedValue;
			if (LabelCodigoCurso.Text == "0")
			{
				list_view();
			}
			else
			{
				ConnectionWithTableDocumentos ConnectionWithTableDocumentos = new ConnectionWithTableDocumentos();
				ConnectionWithTableDocumentos.ValorCodigoFaculdade = ValorCodigoFaculdade;
				ConnectionWithTableDocumentos.ValorCodigoCurso = LabelCodigoCurso.Text;
				DataTable dt = ConnectionWithTableDocumentos.FiltraListViewCurso();
				if (dt.Rows.Count != 0)
				{
					lv_titulos_alunos.DataSource = dt;
					lv_titulos_alunos.DataBind();
				}
				else
				{
					LabelAvisaErro.Text = "Cadastre um título!";
					lv_titulos_alunos.DataBind();
				}
			}
		}

		protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
		{
			try
			{
				(lv_titulos_alunos.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
				list_view();
			}
			catch
			{
				string msg_erro = "Ops!. Alguma coisa deu errada.";
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
					string msg_erro = "Ops! alguma coisa deu errada.";
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
					string msg_erro = "Ops! alguma coisa deu errada.";
					ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
				}
			}
		}
	}
}