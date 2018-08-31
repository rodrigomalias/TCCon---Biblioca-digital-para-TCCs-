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
	public partial class cad_cursos : System.Web.UI.Page
	{
		/*
			lbl_nome_projeto recebe pela gridview o valor atual do tipo de projeto.

			ComparaValorNomeCurso recebe o valor na gridview junto com o txt_nome_curso e 
			irá comparar com o nome atual da textbox, serve somente para verificar se o usuário modificou o nome na textbox.
			
			ComparaValorCodigoCurso recebe o valor na gridview junto com o txt_cod_curso_igual e 
			irá comparar com o id_cursos_igual com o atual na textbox, serve somente para verificar se o usuário modificou o código na textbox.

			lbl_busca_cod_curso receberá o valor através de uma query select no banco de dados e é por ele que os WHERE de cada ALTER serão feitos.
		*/
		public string
			//ComparaValorCodigoCurso,						//Recebe o mesmo valor da textbox de codigo curso e servirá para comparar os valores caso haja alguma alteração
			//ComparaValorNomeCurso,                        //Recebe o mesmo valor da textbox de nome curso e servirá para comparar os valores caso haja alguma alteração
			ValorCodigoFaculdade,								//Recebe o código da faculdade, exemplo: fatec itaquaquecetuba 155
			ValorCodigoUltimoCurso,								//Recebe o ultimo código cadastrado na tabela cursos para fazer a inserção na tabela de cursos_unidades
			RecebeCodigoCursoSelecionadoPrimaryKey;				//Recebe o código especifico da primary key do curso solicitado
		protected void Page_Load(object sender, EventArgs e)
		{
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
				bind_data_gv_cursos_faculdade();
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
		private void bind_data_gv_cursos_faculdade()
		{
			ConnectionWithTableCursos ConnectionWithTableCursos = new ConnectionWithTableCursos();
			try
			{
				//verifica se existe alguma informacao na gridview e a atualiza
				ConnectionWithTableCursos.ValorCodigoFaculdade = ValorCodigoFaculdade;
				DataTable dt = ConnectionWithTableCursos.VerificaGridViewVazia();
				if (dt.Rows.Count != 0)
				{
					gv_cursos_faculdade.DataSource = dt;
					gv_cursos_faculdade.DataBind();
				}
				else
				{
					lbl_aviso_erro.Text = "Cadastre um curso!";
					gv_cursos_faculdade.DataBind();
				}
			}
			catch
			{
				string msg_erro = "Ops! alguma coisa deu errado.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}
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
					ddl_tipo_projeto.SelectedIndex = 0;
					lbl_nome_projeto.Text = "";
					ComparaValorCodigoCurso.Text = "";
					ComparaValorNomeCurso.Text = "";
					lbl_busca_cod_curso.Text = "";
					lbl_aviso_erro.Text = "";
				}
			}
		}
		protected void btn_cad_curso_Click(object sender, EventArgs e)
		{
			if (txt_cod_curso_igual.Text.Length == 0)
			{
				lbl_aviso_erro.Text = "Campo 'CÓDIGO DO CURSO' obrigatório.";
				txt_cod_curso_igual.Focus();
			}
			else if (txt_cod_curso_igual.Text.Length > 5)
			{
				lbl_aviso_erro.Text = "Campo 'CÓDIGO DO CURSO' precisa de no máximo 5 caracteres.";
				txt_cod_curso_igual.Focus();
			}
			else if (txt_nome_curso.Text.Length == 0)
			{
				lbl_aviso_erro.Text = "Campo 'NOME DO CURSO' obrigatório.";
				txt_nome_curso.Focus();
			}
			else if (txt_nome_curso.Text.Length < 4)
			{
				lbl_aviso_erro.Text = "Campo 'NOME DO CURSO' precisa de no mínimo 4 caracteres.";
				txt_nome_curso.Focus();
			}
			else if (txt_nome_curso.Text.Length > 100)
			{
				lbl_aviso_erro.Text = "Campo 'NOME DO CURSO' precisa de no máximo 100 caracteres.";
				txt_nome_curso.Focus();
			}
			else if (ddl_tipo_projeto.SelectedIndex == 0)
			{
				lbl_aviso_erro.Text = "Campo 'TIPO DE PROJETO' obrigatório.";
				ddl_tipo_projeto.Focus();
			}
			else
			{
				try
				{
					ConnectionWithTableCursos ConnectionWithTableCursos = new ConnectionWithTableCursos();
					ConnectionWithTableCursosUnidades ConnectionWithTableCursosUnidades = new ConnectionWithTableCursosUnidades();
					//pega os valores de CODIGO DA FACULDADE, CODIGO DO CURSO, NOME DO CURSO e TIPO DE PROJETO 
					ConnectionWithTableCursos.ValorCodigoFaculdade = ValorCodigoFaculdade;
					ConnectionWithTableCursos.ValorCodigoCurso = txt_cod_curso_igual.Text;
					ConnectionWithTableCursos.ValorNomeCurso = txt_nome_curso.Text.ToUpper();
					ConnectionWithTableCursos.ValorTipoCurso = ddl_tipo_projeto.SelectedItem.ToString();
					//Verifica se ja existe algum cadastro de ID DO CURSO no banco para evitar duplicidade.
					DataTable VerificaIdDt = ConnectionWithTableCursos.VerificaCursoIdCadastrado();
					//Verifica se ja existe algum cadastro do NOME DO CURSO no banco para evitar duplicidade.
					DataTable VerificaNomeDt = ConnectionWithTableCursos.VerificaCursoNomeCadastrado();
					if (VerificaIdDt.Rows.Count.ToString() != "0")
					{
						LimparTodosTextBox(this);
						lbl_aviso_erro.Text = "Este curso já está cadastrado!";
					}
					else if(VerificaNomeDt.Rows.Count.ToString() != "0")
					{
						LimparTodosTextBox(this);
						lbl_aviso_erro.Text = "Este curso já está cadastrado!";
					}
					else
					{
						//faz a insercao dos valores: CODIGO DO CURSO, NOME DO CURSO E TIPO DE PROJETO no BD
						ConnectionWithTableCursos.InsereCurso();
						//faz uma busca no banco de dados procurando o ultimo codigo cadastrado(o cadastro feito acima) e envia para uma variavel de insercao na tabela de cursos_unidades
						ValorCodigoUltimoCurso = ConnectionWithTableCursos.RecebeUltimoCursoCadastrado();
						//pega os valores de CODIGO DA FACULDADE e CODIGO DO CURSO 
						ConnectionWithTableCursosUnidades.ValorCodigoFaculdade = ValorCodigoFaculdade;
						ConnectionWithTableCursosUnidades.ValorCodigoCurso = ValorCodigoUltimoCurso;
						//faz a insercao do curso criado acima na tabela CURSOS_UNIDADES no BD
						ConnectionWithTableCursosUnidades.InsereCursosUnidades();
						//Limpa todos os campos, atualiza a gridview e envia uma mensagem de sucesso para o usuario
						LimparTodosTextBox(this);
						bind_data_gv_cursos_faculdade();
						string msg_sucesso = "Curso cadastrado!";
						ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_sucesso + "\");", true);
					}
				}
				catch
				{
					string msg_erro = "Ops! alguma coisa deu errado.";
					ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
				}
			}
		}
		protected void btn_alt_curso_Click(object sender, EventArgs e)
		{
			if (ComparaValorCodigoCurso.Text == "" || ComparaValorNomeCurso.Text == "")
			{
				lbl_aviso_erro.Text = "Por favor selecione um curso.";
				gv_cursos_faculdade.Focus();
			}
			else if (txt_cod_curso_igual.Text.Length == 0)
			{
				lbl_aviso_erro.Text = "Campo 'CÓDIGO DO CURSO' obrigatório.";
				txt_cod_curso_igual.Focus();
			}
			else if (txt_cod_curso_igual.Text.Length > 5)
			{
				lbl_aviso_erro.Text = "Campo 'CÓDIGO DO CURSO' precisa de no máximo 5 caracteres.";
				txt_cod_curso_igual.Focus();
			}
			else if (txt_nome_curso.Text.Length == 0)
			{
				lbl_aviso_erro.Text = "Campo 'NOME DO CURSO' obrigatório.";
				txt_nome_curso.Focus();
			}
			else if (txt_nome_curso.Text.Length < 3)
			{
				lbl_aviso_erro.Text = "Campo 'NOME DO CURSO' precisa de no mínimo 4 caracteres.";
				txt_nome_curso.Focus();
			}
			else if (txt_nome_curso.Text.Length > 100)
			{
				lbl_aviso_erro.Text = "Campo 'NOME DO CURSO' precisa de no máximo 100 caracteres.";
				txt_nome_curso.Focus();
			}
			else if (ddl_tipo_projeto.SelectedIndex == 0)
			{
				lbl_aviso_erro.Text = "Campo 'TIPO DE PROJETO' obrigatório.";
				ddl_tipo_projeto.Focus();
			}
			else
			{
				try
				{
					ConnectionWithTableCursos ConnectionWithTableCursos = new ConnectionWithTableCursos();
					//pega os valores de CODIGO DA FACULDADE, CODIGO DO CURSO, NOME DO CURSO e TIPO DE PROJETO e envia para a classe ConnectionWithTableCursos
					ConnectionWithTableCursos.ValorCodigoFaculdade = ValorCodigoFaculdade;
					ConnectionWithTableCursos.ValorCodigoCurso = txt_cod_curso_igual.Text;
					ConnectionWithTableCursos.ValorNomeCurso = txt_nome_curso.Text;
					ConnectionWithTableCursos.ValorTipoCurso = ddl_tipo_projeto.SelectedItem.ToString();
					//a variavel ValorCodigoCursoAntigo irá receber o valor antigo que aquele curso possui para verificar posteriormente a primary key
					ConnectionWithTableCursos.ValorCodigoCursoAntigo = ComparaValorCodigoCurso.Text;
					//chamo o metodo RecebeCodigoCursoSelecionadoPrimaryKey que irá utilizar as variaveis ValorCodigoCursoAntigo e ValorCodigoFaculdade 
					//para encontrar a variavel RecebeCodigoPrimaryKey
					ConnectionWithTableCursos.RecebeCodigoCursoSelecionadoPrimaryKey();
					//A variavel RecebeCodigoCursoSelecionadoPrimaryKey recebe o valor vindo do metodo acima
					RecebeCodigoCursoSelecionadoPrimaryKey = ConnectionWithTableCursos.RecebeCodigoPrimaryKey;
					if (txt_cod_curso_igual.Text == ComparaValorCodigoCurso.Text && txt_nome_curso.Text == ComparaValorNomeCurso.Text)
					{
						//chamo o metodo RecebeCodigoCursoSelecionadoPrimaryKey que irá utilizar as variaveis ValorCodigoCurso e ValorCodigoFaculdade 
						//para encontrar a variavel RecebeCodigoPrimaryKey
						ConnectionWithTableCursos.RecebeCodigoCursoSelecionadoPrimaryKey();
						//A variavel RecebeCodigoCursoSelecionadoPrimaryKey recebe o valor vindo do metodo acima
						RecebeCodigoCursoSelecionadoPrimaryKey = ConnectionWithTableCursos.RecebeCodigoPrimaryKey;
						//Altera o curso através do metodo AlteraCurso
						ConnectionWithTableCursos.AlteraCurso();
						//Limpa todos os campos, atualiza a gridview e envia uma mensagem de sucesso para o usuario
						LimparTodosTextBox(this);
						bind_data_gv_cursos_faculdade();
						string msg_sucesso = "Curso alterado!";
						ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_sucesso + "\");", true);
					}
					else if (txt_cod_curso_igual.Text == ComparaValorCodigoCurso.Text)
					{
						//Verifica se ja existe algum cadastro do NOME DO CURSO no banco para evitar duplicidade.
						DataTable VerificaNomeDt = ConnectionWithTableCursos.VerificaCursoNomeCadastrado();
						if (VerificaNomeDt.Rows.Count.ToString() != "0")
						{
							lbl_aviso_erro.Text = "Este curso já existe!";
							txt_nome_curso.Focus();
						}
						else
						{
							//Altera o curso através do metodo AlteraCurso
							ConnectionWithTableCursos.AlteraCurso();
							//Limpa todos os campos, atualiza a gridview e envia uma mensagem de sucesso para o usuario
							LimparTodosTextBox(this);
							bind_data_gv_cursos_faculdade();
							string msg_sucesso = "Curso alterado!";
							ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_sucesso + "\");", true);
						}
					}
					else if (txt_nome_curso.Text == ComparaValorNomeCurso.Text)
					{
						//Verifica se ja existe algum cadastro de ID DO CURSO no banco para evitar duplicidade.
						DataTable VerificaIdDt = ConnectionWithTableCursos.VerificaCursoIdCadastrado();
						if (VerificaIdDt.Rows.Count.ToString() != "0")
						{
							lbl_aviso_erro.Text = "Este código já existe!";
							txt_cod_curso_igual.Focus();
						}
						else
						{
							//Altera o curso através do metodo AlteraCurso
							ConnectionWithTableCursos.AlteraCurso();
							//Limpa todos os campos, atualiza a gridview e envia uma mensagem de sucesso para o usuario
							LimparTodosTextBox(this);
							bind_data_gv_cursos_faculdade();
							string msg_sucesso = "Curso alterado com sucesso!";
							ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_sucesso + "\");", true);
						}
					}
					else
					{
						//Verifica se ja existe algum cadastro de ID DO CURSO no banco para evitar duplicidade.
						DataTable VerificaIdDt = ConnectionWithTableCursos.VerificaCursoIdCadastrado();
						//Verifica se ja existe algum cadastro do NOME DO CURSO no banco para evitar duplicidade.
						DataTable VerificaNomeDt = ConnectionWithTableCursos.VerificaCursoNomeCadastrado();
						if (VerificaNomeDt.Rows.Count.ToString() == "1" || VerificaIdDt.Rows.Count.ToString() == "1")
						{
							lbl_aviso_erro.Text = "Este nome ou código já existe!";
							txt_cod_curso_igual.Focus();
						}
						else
						{
							//Altera o curso através do metodo AlteraCurso
							ConnectionWithTableCursos.AlteraCurso();
							LimparTodosTextBox(this);
							bind_data_gv_cursos_faculdade();
							string msg_sucesso = "Curso alterado!";
							ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_sucesso + "\");", true);
						}
					}
				}
				catch
				{
					string msg_erro = "Ops! alguma coisa deu errado.";
					ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
				}
			}
		}

		protected void btn_exc_curso_Click(object sender, EventArgs e)
		{
			try
			{			
				if (ComparaValorCodigoCurso.Text == "" || ComparaValorNomeCurso.Text == "")
				{
					lbl_aviso_erro.Text = "Selecione um curso para excluir.";
				}
				else
				{
					ConnectionWithTableCursosUnidades ConnectionWithTableCursosUnidades = new ConnectionWithTableCursosUnidades();
					ConnectionWithTableCursos ConnectionWithTableCursos = new ConnectionWithTableCursos();
					ConnectionWithTableDocumentos ConnectionWithTableDocumentos = new ConnectionWithTableDocumentos();
					//recebe os valores do ValorCodigoFaculdade e ComparaValorCodigoCurso
					ConnectionWithTableCursos.ValorCodigoFaculdade = ValorCodigoFaculdade;
					ConnectionWithTableCursos.ValorCodigoCursoAntigo = ComparaValorCodigoCurso.Text;
					//chamo o metodo RecebeCodigoCursoSelecionadoPrimaryKey que irá utilizar as variaveis ValorCodigoCursoAntigo e ValorCodigoFaculdade 
					//para encontrar a variavel RecebeCodigoPrimaryKey
					ConnectionWithTableCursos.RecebeCodigoCursoSelecionadoPrimaryKey();
					//A variavel RecebeCodigoCursoSelecionadoPrimaryKey recebe o valor vindo do metodo acima
					RecebeCodigoCursoSelecionadoPrimaryKey = ConnectionWithTableCursos.RecebeCodigoPrimaryKey;
					//envia os valores de CODIGO DA FACULDADE e CODIGO DO CURSO para verificar se existe algum documento cadastrado neste curso
					ConnectionWithTableDocumentos.ValorCodigoFaculdade = ValorCodigoFaculdade;
					ConnectionWithTableDocumentos.ValorCodigoCurso = RecebeCodigoCursoSelecionadoPrimaryKey;
					DataTable VerificaDocumentoCadastrado = ConnectionWithTableDocumentos.VerificaDocumentoCursoCadastrado();
					if (VerificaDocumentoCadastrado.Rows.Count.ToString() != "0")
					{
						lbl_aviso_erro.Text = "Há um documento cadastrado neste curso!";
						txt_cod_curso_igual.Focus();
					}
					else
					{
						//Envia o valor de fk_curso e fk_unidade para a classe ConnectionWithTableCursosUnidades
						ConnectionWithTableCursosUnidades.RecebeCodigoPrimaryKey = RecebeCodigoCursoSelecionadoPrimaryKey;
						ConnectionWithTableCursosUnidades.ValorCodigoFaculdade = ValorCodigoFaculdade;
						//Chama o metodo DeletaCursosUnidades	
						ConnectionWithTableCursosUnidades.DeleteCursosUnidades();
						//Chama o metodo DeletaCurso
						ConnectionWithTableCursos.DeletaCurso();
						LimparTodosTextBox(this);
						bind_data_gv_cursos_faculdade();
						string msg_erro_delete = "Curso excluído!";
						ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.confirm(\"" + msg_erro_delete + "\");", true);
					}		
				}
			}
			catch
			{
				string msg_erro = "Ops! alguma coisa deu errado.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}
		}

		protected void btn_cancel_Click(object sender, EventArgs e)
		{	
			try
			{
				LimparTodosTextBox(this);
			}
			catch
			{
				string msg_erro = "Ops! alguma coisa deu errado.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}
		}

		protected void gv_cursos_faculdade_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
		{
			try
			{			
				txt_cod_curso_igual.Text = gv_cursos_faculdade.Rows[e.NewSelectedIndex].Cells[1].Text;
				txt_nome_curso.Text = gv_cursos_faculdade.Rows[e.NewSelectedIndex].Cells[2].Text;
				lbl_nome_projeto.Text = gv_cursos_faculdade.Rows[e.NewSelectedIndex].Cells[3].Text;
				ComparaValorCodigoCurso.Text = gv_cursos_faculdade.Rows[e.NewSelectedIndex].Cells[1].Text;
				ComparaValorNomeCurso.Text = gv_cursos_faculdade.Rows[e.NewSelectedIndex].Cells[2].Text;
				if (lbl_nome_projeto.Text == "ARTIGO CIENTÍFICO")
					ddl_tipo_projeto.SelectedValue = "1";
				else if (lbl_nome_projeto.Text == "MONOGRAFIA")
					ddl_tipo_projeto.SelectedValue = "2";
				else if (lbl_nome_projeto.Text == "PROJETO TECNOLÓGICO")
					ddl_tipo_projeto.SelectedValue = "3";
			}
			catch
			{
				string msg_erro = "Ops! alguma coisa deu errado.";
				ClientScript.RegisterStartupScript(typeof(string), string.Empty, "window.alert(\"" + msg_erro + "\");", true);
			}
		}
	}
}