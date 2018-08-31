<%@ Page MaintainScrollPositionOnPostback="false" Language="C#" AutoEventWireup="true" CodeBehind="cad-cursos.aspx.cs" Inherits="TCCon.cad_cursos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
		<meta charset="UTF-8" />
		<link rel="shortcut icon" href="images/logo/icon.png" />
		<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
		<script src="https://use.fontawesome.com/releases/v5.0.12/js/all.js"></script>
		<link rel="stylesheet" href="css/style.css" type="text/css" /> 
		<link rel="stylesheet" href="css/software_style.css" type="text/css" />
		<script type="text/javascript" src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
 		<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.0.0/jquery.min.js"></script>
  		<script type="text/javascript" src="ScriptSite/hamburger_menu.js"></script>
		<script type="text/javascript" src="ScriptSite/masc_only_num.js"></script>
		<title> TCCon - BIBLIOTECA DIGITAL </title>
		<script type="text/javascript">
			$(function(){
				$("#AlterarVisual").click(function(e){
					e.preventDefault();
					el = $(this).data('element');
					$(el).toggle();
					$("#btn_alt_curso").attr("tabindex",-1).focus();
				});			
			});
			$(function(){
				$("#ExcluirVisual").click(function(e){
					e.preventDefault();
					el = $(this).data('element');
					$(el).toggle();
					$("#btn_exc_curso").attr("tabindex",-1).focus();
				});			
			});
		</script>
	</head>
	<body>
		<form id="form1" runat="server">
		<div class="global">
			<div id="DivConfirmarAlterar" class="DivConfirmarAcao">
				<div class="DivContainerConfirmar">
					<div class="DivCancelarX">
						<button runat="server" data-element="#DivConfirmarAlterar"><i class="fas fa-times fa-1x"></i></button>
					</div>
					<div class="DivMensagemConfirmar">
						<p> Tem certeza que deseja alterar? </p>
					</div>
					<div class="DivBotoesConfirmar">
						<ul>
							<li>
								<asp:Button class="btnConfirmarAcao" ID="btn_alt_curso" runat="server" Visible="true" Text="ALTERAR" OnClick="btn_alt_curso_Click" />
								<button class="btnConfirmarAcao" runat="server" onServerClick="btn_cancel_Click"> CANCELAR</button>
							</li>
						</ul>				
					</div>
				</div>
			</div>
			<div id="DivConfirmarExcluir" class="DivConfirmarAcao">
				<div class="DivContainerConfirmar">
					<div class="DivCancelarX">
						<button runat="server" data-element="#DivConfirmarExcluir"><i class="fas fa-times fa-1x"></i></button>
					</div>
					<div class="DivMensagemConfirmar">
						<p> Tem certeza que deseja excluir? </p>
					</div>
					<div class="DivBotoesConfirmar">
						<ul>
							<li>
								<asp:Button class="btnConfirmarAcao" ID="btn_exc_curso" runat="server" Visible="true" Text="EXCLUIR" OnClick="btn_exc_curso_Click" />
								<button class="btnConfirmarAcao" runat="server" onServerClick="btn_cancel_Click"> CANCELAR</button>
							</li>
						</ul>				
					</div>
				</div>
			</div>
			<div class="top_software">
				<div class="div_usuario">
					<p>  Usuário <i class="far fa-user"></i> : <asp:Label ID="lblUsuario" runat="server"></asp:Label></p>
				</div>
				<div class="div_unidade_1024px">
					<p> <i class="far fa-building fa-2x"></i> <asp:Label ID="lblNomeUnidade1024px" runat="server" Text=""></asp:Label>
					</p>
				</div>
				<div class="div_btnlogout">
					<asp:Label ID="lbl_nome_projeto" runat="server" Visible="false"></asp:Label>
					<asp:Label ID="ComparaValorCodigoCurso" runat="server" Visible="false"></asp:Label>
					<asp:Label ID="ComparaValorNomeCurso" runat="server" Visible="false"></asp:Label>
					<asp:Label ID="lbl_busca_cod_curso" runat="server" Visible="false" ></asp:Label>

					<asp:Button ID="btnVoltar" runat="server" Visible="False" OnClick="btnVoltar_Click"/>
					<button class="btnVoltar" runat="server" onServerClick="btnVoltar_Click"><i class="fas fa-arrow-left"></i> VOLTAR</button>
					<asp:Button ID="btnLogout" runat="server" Visible="False" OnClick="btnLogout_Click"/>
					<button class="btnLogout" runat="server" onServerClick="btnLogout_Click"><i class="fas fa-sign-out-alt"></i> SAIR</button>
				</div>
				<div class="div_unidade_1023px">
					<p> <i class="far fa-building fa-2x"></i> <asp:Label ID="lblNomeUnidade1023px" runat="server" Text=""></asp:Label></p>
				</div>
			</div>
			<div class="inner">	
				<div class="general_inner_cursos">
					<div class="DivTituloDocumentos">
						<ul>
							<li class="LiTituloCadastroDeDocumento"> <p><i class="fa fa-book" aria-hidden="true"></i> CADASTRO DE CURSOS </p></li>
							<li class="LiInformativo"><p>Cadastro obrigatório <i class="AsteriscoValida fas fa-asterisk fa-xs"></i></p></li>
						</ul>
					</div>
					<nav class="nav_cad_cursos">
						<ul>
							<li class="LiNomeDosCampos"><i class="fas fa-clipboard"></i>CÓDIGO DO CURSO </li>
							<li>
								<asp:TextBox ID="txt_cod_curso_igual" CssClass="txt_cadastros" runat="server" MinLength="1" MaxLength="4" placeholder="Ex:78,65,100" onKeyDown="Mascara(this,Valor);" onKeyPress="Mascara(this,Valor);" onKeyUp="Mascara(this,Valor);" AutoPostBack="false"></asp:TextBox> <i class="AsteriscoValida fas fa-asterisk fa-xs"></i>
							</li>
							<li class="LiNomeDosCampos"><i class="fas fa-address-book"></i> NOME DO CURSO </li>
							<li>
								<asp:TextBox ID="txt_nome_curso" CssClass="txt_cadastros" runat="server" MinLength="4" MaxLength="99" placeholder="Ex:SECRETARIADO" AutoPostBack="false"></asp:TextBox> <i class="AsteriscoValida fas fa-asterisk fa-xs"></i>
							</li>
							<li class="LiNomeDosCampos"><i class="fas fa-clipboard-list"></i> TIPO DE PROJETO  </li>
							<li>
								<asp:DropDownList ID="ddl_tipo_projeto" CssClass="txt_cadastros" runat="server">
									<asp:ListItem Text="Tipo de Projeto" Value="0"/>
									 <asp:ListItem Text="ARTIGO CIENTÍFICO" Value="1"/>
									 <asp:ListItem Text="MONOGRAFIA" Value="2"/>
									 <asp:ListItem Text="PROJETO TECNOLÓGICO" Value="3"/>
								</asp:DropDownList> <i class="AsteriscoValida fas fa-asterisk fa-xs"></i>
							</li>
							<li class="lbl_aviso_erro">
								<asp:Label ID="lbl_aviso_erro" runat="server" Text="" ForeColor=""></asp:Label>
							</li>
						</ul>
					</nav>
					<div class="DivBotoes">
						<nav>
							<ul>
								<li class="li_btn_cad_curso">
									<button class="btn_cadastros" runat="server" onServerClick="btn_cad_curso_Click"><i class="fas fa-save"></i> CADASTRAR</button>
									<asp:Button ID="btn_cad_curso" runat="server" Visible="False" Text="" OnClick="btn_cad_curso_Click" />
								</li>
								<li class="li_btn_cad_curso">
									<button id="AlterarVisual" data-element="#DivConfirmarAlterar" class="btn_cadastros" runat="server"><i class="fas fa-pencil-alt"></i> ALTERAR</button>								
								</li>
								<li class="li_btn_cad_curso">
									<button class="btn_cadastros" runat="server" onServerClick="btn_cancel_Click"><i class="fas fa-ban"></i> CANCELAR</button>
									<asp:Button ID="btn_cancel" runat="server" Visible="False" Text="" OnClick="btn_cancel_Click" />
								</li>
								<li class="li_btn_cad_curso">
									<button id="ExcluirVisual" data-element="#DivConfirmarExcluir" class="btn_cadastros" runat="server"><i class="fas fa-trash-alt"></i>EXCLUIR</button>								
								</li>
							</ul>
						</nav>
					</div>
					<div class="div_gridview">
						<asp:GridView 
						ID="gv_cursos_faculdade" 
						runat="server" 
						AutoGenerateColumns="False" 
						DataKeyNames="COD_CURSO" 
						OnSelectedIndexChanging="gv_cursos_faculdade_SelectedIndexChanging" 
						EmptyDataText="Não foi registrado nenhum curso."
						Gridlines ="none"
						CssClass="css_gridview"
						AlternatingRowStyle-CssClass="alt"
						>
							<Columns>
								<asp:BoundField DataField="cod_curso" HeaderText="cod_autoincrement" Visible="False" />
								<asp:BoundField DataField="ID_CURSOS_IGUAL" HeaderText="CÓDIGO" />
								<asp:BoundField DataField="CURSO" HeaderText="NOME DO CURSO" HtmlEncode="False" />
								<asp:BoundField DataField="TIPO_PROJETO" HeaderText="TIPO DE PROJETO" HtmlEncode="False" />
								<asp:CommandField SelectText="Selecionar" ShowSelectButton="True"/>
							</Columns>
						</asp:GridView>
					</div>
				</div>
			</div>
			<div class="footer">
				<div class="footer_inside_left">
					<nav class="nav_menu_footer">							
						<ul>
							<li><a href="http://www.saopaulo.sp.gov.br/" target="_blank";"> <i class="fa fa-building" aria-hidden="true"></i>GOVERNO DO ESTADO DE SÃO PAULO</a> </li>	  
							<li><a href="http://www.cps.sp.gov.br/" target="_blank";"><i class="fa fa-building" aria-hidden="true"></i> CENTRO PAULA SOUZA</a> </li>     
						</ul>
					</nav>
				</div>
				<div class="footer_inside_right">
					<img src="images/footer/cps_gov_sp.png" />
				</div>
			</div>
		</div>
		</form>
	</body>
</html>
