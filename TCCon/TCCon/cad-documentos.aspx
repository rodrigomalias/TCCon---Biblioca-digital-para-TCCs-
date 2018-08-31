<%@ Page  MaintainScrollPositionOnPostback="false" Language="C#" AutoEventWireup="true" CodeBehind="cad-documentos.aspx.cs" Inherits="TCCon.cad_documentos" %>
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
		<script type="text/javascript" src="ScriptSite/RevelaResumo.js"></script>
		<title> TCCon - BIBLIOTECA DIGITAL</title>
		<script type="text/javascript">
			function FileInfoPdf() {
				document.getElementById('<%=TextBoxPdf.ClientID%>').value = document.getElementById('<%=upload_pdf.ClientID%>').value;
			}
			function FileInfoRar() {
				document.getElementById('<%=TextBoxRar.ClientID%>').value = document.getElementById('<%=upload_rar.ClientID%>').value;
			}		
			$(function(){
				$("#AlterarVisual").click(function(e){
					e.preventDefault();
					el = $(this).data('element');
					$(el).toggle();
					$("#btn_alt_documentos").attr("tabindex",-1).focus();
				});			
			});
			$(function(){
				$("#ExcluirVisual").click(function(e){
					e.preventDefault();
					el = $(this).data('element');
					$(el).toggle();
					$("#btn_exc_documentos").attr("tabindex",-1).focus();
				});			
			});
		</script>
	</head>
<body>
    <form id="form1" runat="server">
        <div class="global">
			<div id="DivConfirmarAlterar" runat="server" class="DivConfirmarAcao">
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
								<asp:Button class="btnConfirmarAcao" ID="btn_alt_documentos" runat="server" Visible="true" Text="ALTERAR" OnClick="btn_alt_documentos_Click" />	
								<button class="btnConfirmarAcao" runat="server" onServerClick="btn_cancel_documentos_Click"> CANCELAR</button>
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
								<asp:Button class="btnConfirmarAcao" ID="btn_exc_documentos" runat="server" Visible="true" Text="EXCLUIR" OnClick="btn_exc_documentos_Click" />
								<button class="btnConfirmarAcao" runat="server" onServerClick="btn_cancel_documentos_Click"> CANCELAR</button>
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
					<p> <i class="far fa-building fa-2x"></i> <asp:Label ID="lblNomeUnidade1024px" runat="server" Text=""></asp:Label></p>
				</div>
				<div class="div_btnlogout">
					<asp:Button ID="btnVoltar" runat="server" Visible="False" OnClick="btnVoltar_Click"/>
					<button class="btnVoltar" runat="server" onServerClick="btnVoltar_Click"><i class="fas fa-arrow-left"></i> VOLTAR</button>
					<asp:Button ID="btnLogout" runat="server" Visible="False" OnClick="btnLogout_Click"/>
					<button class="btnLogout" runat="server" onServerClick="btnLogout_Click"><i class="fas fa-sign-out-alt"></i> SAIR</button>
				</div>
				<div class="div_unidade_1023px">
					<p> <i class="far fa-building fa-2x"></i> <asp:Label ID="lblNomeUnidade1023px" runat="server" Text=""></asp:Label></p>
				</div>
			</div>
			<div class="inner_cad_documentos">
				<div class="DivTituloDocumentos">
					<ul>
						<li class="LiTituloCadastroDeDocumento"> <p><i class="fas fa-graduation-cap"></i> CADASTRO DE DOCUMENTOS </p></li>
						<li class="LiInformativo"><p>Cadastro obrigatório <i class="AsteriscoValida fas fa-asterisk fa-xs"></i></p></li>
						<li class="LiInformativo"><p>Cadastro opcional <i class="CheckValida fas fa-check fa-xs"></i></p></li>
					</ul>
				</div>
				<div class="DivEsquerdaDocumentos">
					<nav class="nav_cad_documentos">
						<ul>		
							<li> <asp:TextBox ID="txt_cod_documento" CssClass="txt_cadastros" runat="server" ReadOnly="True" Visible="false"></asp:TextBox> </li>
							
							<li class="LiNomeDosCampos"><i class="fas fa-clipboard"></i> CURSO DO TÍTULO </li>
							<li><asp:DropDownList ID="ddl_cursos_fatec" CssClass="txt_cadastros" runat="server" OnSelectedIndexChanged="ddl_cursos_fatec_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList> <i class="AsteriscoValida fas fa-asterisk fa-xs"></i></li>

							<li class="LiNomeDosCampos"><i class="fas fa-graduation-cap"></i> NOME DO TÍTULO </li>
							<li><asp:TextBox ID="txt_nome_titulo" CssClass="txt_cadastros" runat="server" MinLength="4" MaxLength="199" placeholder="Ex:BIBLIOTECA DIGITAL" AutoPostBack="false"></asp:TextBox> <i class="AsteriscoValida fas fa-asterisk fa-xs"></i></li>
						
							<li class="LiNomeDosCampos"><i class="fas fa-calendar-alt"></i> ANO DE ENTREGA </li>
							<li><asp:TextBox ID="txt_data_entrega" CssClass="txt_cadastros" runat="server" MinLength="4" MaxLength="4" placeholder="Ex:2018" onKeyDown="Mascara(this,Valor);" onKeyPress="Mascara(this,Valor);" onKeyUp="Mascara(this,Valor);" AutoPostBack="false"></asp:TextBox> <i class="AsteriscoValida fas fa-asterisk fa-xs"></i></li>
						
							<li class="LiNomeDosCampos"><i class="fas fa-prescription-bottle"></i> RESUMO</li>
							<li><asp:TextBox ID="txt_resumo" style="height:180px;" CssClass="txt_cadastros" runat="server" MinLength="10" MaxLength="5000" placeholder="Ex:Este projeto tem como..." TextMode="MultiLine" AutoPostBack="false"></asp:TextBox> <i class="AsteriscoValida fas fa-asterisk fa-xs"></i></li>		
						</ul>
					</nav>
				</div>
				<div class="DivDireitaDocumentos">
					<nav class="nav_cad_documentos">
						<ul>
							<li class="LiNomeDosCampos"><i class="fas fa-key"></i> PALAVRAS CHAVE</li>
							<li><asp:TextBox ID="txt_palavras_chave" style="height:80px;" CssClass="txt_cadastros" runat="server" MinLength="4" MaxLength="199" placeholder="Ex:Tecnologia,Informação,Gestão" TextMode="MultiLine" AutoPostBack="false"></asp:TextBox> <i class="AsteriscoValida fas fa-asterisk fa-xs"></i></li>						

							<li class="LiNomeDosCampos"><i class="fas fa-user"></i> 1º AUTOR </li>
							<li><asp:TextBox ID="txt_primeiro_autor" CssClass="txt_cadastros" runat="server" MinLength="4" MaxLength="199" placeholder="Ex:Fulano da Silva" AutoPostBack="false"></asp:TextBox> <i class="AsteriscoValida fas fa-asterisk fa-xs"></i></li>
							
							<li class="LiNomeDosCampos"><i class="fas fa-user"></i> 2º AUTOR</li>
							<li><asp:TextBox ID="txt_segundo_autor" CssClass="txt_cadastros" runat="server" MinLength="4" MaxLength="199" placeholder="Ex:Ciclano dos Santos" AutoPostBack="false" ></asp:TextBox> <i class="CheckValida fas fa-check fa-xs"></i></li>
							
							<li class="LiNomeDosCampos"><i class="fas fa-user"></i> 3º AUTOR</li>
							<li><asp:TextBox ID="txt_terceiro_autor" CssClass="txt_cadastros" runat="server" MinLength="4" MaxLength="199" placeholder="Ex:Beltrano de Alcantara" AutoPostBack="false"></asp:TextBox> <i class="CheckValida fas fa-check fa-xs"></i></li>
							
							<li class="LiNomeDosCampos"><i class="fas fa-user"></i> 4º AUTOR</li>
							<li><asp:TextBox ID="txt_quarto_autor" CssClass="txt_cadastros" runat="server" MinLength="4" MaxLength="199" placeholder="Ex:Fulano dos Santos" AutoPostBack="false"></asp:TextBox> <i class="CheckValida fas fa-check fa-xs"></i></li>	
						</ul>
					</nav>
				</div>
				<div class="DivFileUploadDocumentos">
					<div class="DivUploadPdf">
						<ul>
							<li id="LiNomePdfDoTitulo" class="LiNomeDosCampos"><i class="fas fa-file-pdf"></i> .PDF DO TÍTULO</li>
							<li><asp:LinkButton ID="LinkButtonPdf" runat="server" CssClass="LinkButtonFileUpload"><i class="fas fa-upload"></i> UPLOAD DE .PDF</asp:LinkButton> <i class="AsteriscoValida fas fa-asterisk fa-xs"></i></li>
							<li>
								<asp:TextBox ID="TextBoxPdf" runat="server" CssClass="TextBoxFileUpload" ReadOnly="true"></asp:TextBox>
								<asp:FileUpload ID="upload_pdf" runat="server" onchange="FileInfoPdf()" style="display:none;"/>								
							</li>
						</ul>
					</div>
					<div class="DivUploadRar">
						<ul>
							<li id="LiNomeRarDoTitulo" class="LiNomeDosCampos"><i class="fas fa-file-archive"></i> .RAR DO TÍTULO</li>
							<li><asp:LinkButton ID="LinkButtonRar" runat="server" CssClass="LinkButtonFileUpload"><i class="fas fa-upload"></i> UPLOAD DE .RAR</asp:LinkButton> <i class="CheckValida fas fa-check fa-xs"></i></li>
							<li>
								<asp:TextBox ID="TextBoxRar" runat="server" CssClass="TextBoxFileUpload" ReadOnly="true"></asp:TextBox>
								<asp:FileUpload ID="upload_rar" runat="server" onchange="FileInfoRar()" style="display:none;"/>								
							</li>
						</ul>
					</div>		
					<div class="DivLabelAvisaErro">
						<ul>
							<asp:Label ID="LabelRecebeVerificaPdf" runat="server" Visible="false"></asp:Label>
							<asp:Label ID="LabelRecebeVerificaRar" runat="server" Visible="false"></asp:Label>
							<asp:Label ID="LabelRecebeVerificaDataEntrega" runat="server" Visible="false"></asp:Label>
							<asp:Label ID="LabelCodigoCurso" runat="server" Visible="false"></asp:Label>
							<asp:Label ID="LabelComparaCodigoDocumento" runat="server" Visible="false"></asp:Label>
							<asp:Label ID="LabelComparaNomeTitulo" runat="server" Visible="false"></asp:Label>
							<li class="lbl_aviso_erro"><asp:Label ID="LabelAvisaErro" runat="server" Text=""></asp:Label></li>
						</ul>
					</div>
				</div>
				<div class="DivBotoes">
					<nav class="nav_cad_documentos">
						<ul>
							<li class="li_btn_cad_curso">
								<button class="btn_cadastros" runat="server" onServerClick="btn_cad_documentos_Click"><i class="fas fa-save"></i> CADASTRAR</button>
								<asp:Button ID="btn_cad_documentos" runat="server" Visible="False" Text="" OnClick="btn_cad_documentos_Click" />
							</li>						
							<li class="li_btn_cad_curso">		
								<!--<div class="meu-box">	
									MOSTRAR UMA MENSAGEM DE AVISO QUANDO PASSAR O MOUSE EM CIMA 
									<button class="btn_cadastros HoverBotoes" runat="server" onServerClick="btn_alt_documentos_Click"><i class="fas fa-pencil-alt"></i> ALTERAR</button>	
									<p class="MensagemBotao">Selecione um curso para alterar.</p>
								</div>-->
								<button id="AlterarVisual" data-element="#DivConfirmarAlterar" class="btn_cadastros" runat="server"><i class="fas fa-pencil-alt"></i> ALTERAR</button>											
							</li>
							<li class="li_btn_cad_curso">
								<button class="btn_cadastros" runat="server" onServerClick="btn_cancel_documentos_Click"><i class="fas fa-ban"></i> CANCELAR</button>
								<asp:Button ID="btn_cancel_documentos" runat="server" Visible="False" Text="" OnClick="btn_cancel_documentos_Click" />
							</li>
							<li class="li_btn_cad_curso">
								<button id="ExcluirVisual" data-element="#DivConfirmarExcluir" class="btn_cadastros" runat="server"><i class="fas fa-trash-alt"></i> EXCLUIR</button>				
							</li>
						</ul>
					</nav>
				</div>
				<div class="DivFiltrarListView">	
					<ul>			
						<li class="LiFiltroListView">	
							<div class="meu-box">							
								<i class="fas fa-filter"></i> Filtrar Cursos  <asp:DropDownList ID="ddl_FiltrarCurso" CssClass="txt_cadastros input-nome" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddl_FiltrarCurso_SelectedIndexChanged"></asp:DropDownList>
								<p class="MensagemAjuda">Utilize 'Selecione um Curso' para exibir todos os cursos.</p>
							</div>
						</li>
					</ul>
				</div>
				<div class="div_bottom_inner">
					<asp:ListView 
						ID="lv_titulos_alunos" 
						runat="server" 
						DataKeyNames="COD_DOCUMENTO" 
						OnSelectedIndexChanging="lv_titulos_alunos_SelectedIndexChanging" 
						OnItemCommand="lv_titulos_alunos_ItemCommand" 		
						OnPagePropertiesChanging="OnPagePropertiesChanging"
						GroupPlaceholderID="groupPlaceHolder1"
						ItemPlaceholderID="itemPlaceHolder1" OnLoad="lv_titulos_alunos_Load"
						>
						<ItemTemplate>
							<div class="div_lv_titulos_alunos"><span class="SpanDivTitulosAlunos">
								</ul>
									<ul>
									<li style="text-align:center;" class="li_titulo">
										<p class="NomesCamposListView"> <asp:Label ID="LabelTitulo" runat="server" Text='<%#Eval("TITULO") %>'> </asp:Label></p>
										<asp:Label ID="LabelCodigoDocumento" runat="server" Visible="false" Text='<%#Eval("COD_DOCUMENTO") %>'></asp:Label>						
									</li>
									<li>
										<p class="NomesCamposListView">Autor(es)</p>
										<p class="ValoresBancoDeDados"><asp:Label ID="LabelAutorUm" runat="server" Text='<%#Eval("AUTOR_UM") %>'></asp:Label></p>
										<p class="ValoresBancoDeDados"><asp:Label ID="LabelAutorDois" runat="server" Text='<%#Eval("AUTOR_DOIS") %>'></asp:Label></p>
										<p class="ValoresBancoDeDados"><asp:Label ID="LabelAutorTres" runat="server" Text='<%#Eval("AUTOR_TRES") %>'></asp:Label></p>
										<p class="ValoresBancoDeDados"><asp:Label ID="LabelAutorQuatro" runat="server" Text='<%#Eval("AUTOR_QUATRO") %>'></asp:Label></p>
									</li>
									<li>
										<p class="NomesCamposListView">Ano da entrega</p>
										<p class="ValoresBancoDeDados"><asp:Label ID="LabelDataProjeto" runat="server" Text='<%#Eval("ano_projeto") %>'></asp:Label></p>										
									</li>
									<li>
										<p class="NomesCamposListView">Curso</p>
										<p class="ValoresBancoDeDados"><asp:Label ID="LabelNomeCurso" runat="server" Text='<%#Eval("CURSO") %>'></asp:Label></p>	
										<asp:Label ID="LabelCodigoCurso" runat="server" Visible="false" Text='<%#Eval("cod_curso") %>'></asp:Label>
									</li>									
									<li>
										<p class="NomesCamposListView">Palavras-Chave</p>
										<p class="ValoresBancoDeDados"><asp:Label ID="LabelPalavraChave" runat="server" Text='<%#Eval("PALAVRA_CHAVE") %>'></asp:Label></p>
									</li>
									<li>													
										<div class="box-toggle">
											<div class="tgl">
												<p class="ValoresBancoDeDados"><asp:Label ID="LabelResumo" runat="server" Text='<%#Eval("resumo") %>'></asp:Label></p>	
											</div>
										</div>
									</li>
									<li>
										<asp:Label ID="LabelPdf" runat="server" Visible="false" Text='<%#Eval("PDF") %>'></asp:Label>
										<asp:LinkButton CssClass="LinkButtonListView" ID="LinkButtonExibePdf" runat="server" CommandName="VisualizaPdf" CommandArgument='<%# Container.DataItemIndex %>'><i class="fas fa-external-link-square-alt"></i> Exibir .PDF</asp:LinkButton>	
									</li>
									<li>									
										<asp:Label ID="LabelRar" runat="server" Visible="false" Text='<%#Eval("RAR") %>'></asp:Label>
										<asp:LinkButton CssClass="LinkButtonListView" ID="LinkButtonBaixarRar" runat="server" CommandName="DownloadRar" CommandArgument='<%# Container.DataItemIndex %>'><i class="fas fa-download"></i> Download .RAR</asp:LinkButton>
									</li>
								</ul>
								<p class="BotaoSelecionar"><asp:LinkButton CssClass="BotaoSelecionarListView" ID="BotaoSelecionar" runat="server" CommandName="Select"><i class="fas fa-hand-pointer"></i> SELECIONAR</asp:LinkButton> </p>
							</span></div>					
						</ItemTemplate>	
						<LayoutTemplate>
							<div class="DivBotoesPagina">						
								<asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
								<ul class="PaginaAtual">
									<li>
										<asp:DataPager ID="DataPager1" runat="server" PagedControlID="lv_titulos_alunos" PageSize="6">
											<Fields>
												<asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
												ShowNextPageButton="false" ButtonCssClass="BotaoPaginaAnteriorProxima" />
												<asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="NumeroPaginasExistentes" />
												<asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton = "false" ButtonCssClass="BotaoPaginaAnteriorProxima" />
											</Fields>
										</asp:DataPager>
									</li>
								</ul>
							</div>
						</LayoutTemplate>
						<GroupTemplate>
								<asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
						</GroupTemplate>
					</asp:ListView>							
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
