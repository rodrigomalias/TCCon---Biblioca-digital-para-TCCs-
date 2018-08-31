<%@ Page MaintainScrollPositionOnPostback="false" Language="C#" AutoEventWireup="true" CodeBehind="monografias.aspx.cs" Inherits="TCCon.monografias" %>

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
		<script type="text/javascript" src="ScriptSite/hamburger_menu_filtro.js"></script>
		<script type="text/javascript" src="ScriptSite/RevelaResumo.js"></script>
		<title> TCCon - BIBLIOTECA DIGITAL </title>
	</head>
	<body>
		<div class="global">
			<div class="top">
				<div class="mobile_logo">
					<a href="/index.aspx"><img src="images/logo/fatec.jpg" /></a>
				</div>
				<div class="toggle-nav">	
					<span class="e"><span class="bar"></span><span class="bar"></span><span class="bar"></span></span>			
				</div>	
				<div class="nav-pane">		
					<div class="menu">	
						<nav class="nav_menu">							
							<ul>
								<li><img src="images/logo/fatec.jpg" /></li>
								<li class="sliding_middle_out"><a href="/index.aspx"><i class="fa fa-home" aria-hidden="true"></i> INÍCIO</a> </li>
								<li class="active_link"><a href="/monografias.aspx" style="border-bottom:none;"><i class="fa fa-book" aria-hidden="true"></i> TCCS</a> </li>
								<li class="sliding_middle_out"><a href="/sobre-nos.aspx"><i class="fa fa-user-circle" aria-hidden="true"></i> SOBRE NÓS</a> </li>  
								<li class="sliding_middle_out"><a href="/administracao.aspx"><i class="fa fa-lock" aria-hidden="true"></i> ADMINISTRAÇÃO</a> </li>	    
							</ul>
						</nav>				
					</div>
				</div>
			</div>
			<div class="DivTituloMonografia">
				<ul>
					<li> <p> ACERVO DE TCCS DA FATEC </p></li>
				</ul>
			</div>
			<div class="DivMenuMobileFiltro">
				<ul>
					<li class="LiTituloFiltrarBuscaMobile"><i class="fas fa-filter"></i> FILTRAR BUSCA
						<div class="MenuMobileFiltroBotao">	
							<span class="e"><span class="bar"></span><span class="bar"></span><span class="bar"></span></span>			
						</div>
					</li>
				</ul>				
			</div>
			<div class="inner">
			<form class="form_tccs_fatec" name="tccs_fatec" runat="server">
				<div class="DivMobileFiltro">
				<div class="menu_tccs">
					<nav class="NavMonografiaFiltro">
						<ul>
							<li class="LiFilrarBusca"><i class="fas fa-filter"></i> FILTRAR BUSCA</li>
							
							<li class="LiNomeDosCampos"><i class="fas fa-building"></i> FACULDADE </li>
							<li><asp:DropDownList CssClass="txt_FiltroMonografias" ID="ddl_faculdade" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_faculdade_SelectedIndexChanged" OnTextChanged="ddl_faculdade_TextChanged" ></asp:DropDownList>
								<asp:Label ID="lbl_cod_unidade" runat="server" Visible="false"></asp:Label>
							</li>

							<li class="LiNomeDosCampos"><i class="fas fa-clipboard"></i> CURSO </li>
							<li><asp:DropDownList CssClass="txt_FiltroMonografias" ID="ddl_curso" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_curso_SelectedIndexChanged"></asp:DropDownList>
								<asp:Label ID="lbl_cod_curso" runat="server" Visible="false"></asp:Label>
							</li>		

							<li class="LiNomeDosCampos"><i class="fas fa-graduation-cap"></i> NOME DO TITULO  </li>
							<li><asp:TextBox ID="TxtNomeTitulo" CssClass="txt_FiltroMonografias" runat="server" placeholder="Ex:BIBLIOTECA DIGITAL." maxlength="199"></asp:TextBox></li>

							<li class="LiNomeDosCampos"><i class="fas fa-user"></i> NOME DO AUTOR </li>
							<li><asp:TextBox ID="TxtNomeAutor" CssClass="txt_FiltroMonografias" runat="server" placeholder="Ex:FULANO DE TAL..." maxlength="199"></asp:TextBox></li>

							<li class="LiNomeDosCampos"><i class="fas fa-key"></i> PALAVRAS CHAVE </li>
							<li><asp:TextBox ID="TxtPalavrasChave" CssClass="txt_FiltroMonografias" runat="server" placeholder="Ex:TECNOLOGIA, FUTURISMO..." maxlength="199"></asp:TextBox></li>

							<li>					
								<button class="BtnPesquisarMonografiasFiltro" runat="server" onServerClick="btn_buscar_Click"><span class="SpanBordaBtnPesquisar"><i class="fa fa-search" aria-hidden="true"></i> PESQUISAR</span></button>
								<asp:Button ID="btn_buscar"  runat="server" Visible="False" Text="" OnClick="btn_buscar_Click" />
							</li>
						</ul>
					</nav>	
				</div>
				</div>
				<div class="tccs_container">
					<asp:ListView 
						ID="lv_titulos_alunos" 
						runat="server" 
						DataKeyNames="COD_DOCUMENTO" 
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
										<p class="NomesCamposListView"><i class="fas fa-graduation-cap"></i> <asp:Label ID="LabelTitulo" runat="server" Text='<%#Eval("TITULO") %>'> </asp:Label></p>
										<asp:Label ID="LabelCodigoDocumento" runat="server" Visible="false" Text='<%#Eval("COD_DOCUMENTO") %>'></asp:Label>	
									</li>
									<li>
										<p class="NomesCamposListView">Autor(es) <i class="fas fa-user"></i></p>
										<p class="ValoresBancoDeDados"><asp:Label ID="LabelAutorUm" runat="server" Text='<%#Eval("AUTOR_UM") %>'></asp:Label></p>
										<p class="ValoresBancoDeDados"><asp:Label ID="LabelAutorDois" runat="server" Text='<%#Eval("AUTOR_DOIS") %>'></asp:Label></p>
										<p class="ValoresBancoDeDados"><asp:Label ID="LabelAutorTres" runat="server" Text='<%#Eval("AUTOR_TRES") %>'></asp:Label></p>
										<p class="ValoresBancoDeDados"><asp:Label ID="LabelAutorQuatro" runat="server" Text='<%#Eval("AUTOR_QUATRO") %>'></asp:Label></p>
									</li>
									<li>
										<p class="NomesCamposListView">Ano da entrega <i class="fas fa-calendar-alt"></i></p>
										<p class="ValoresBancoDeDados"><asp:Label ID="LabelDataProjeto" runat="server" Text='<%#Eval("ano_projeto") %>'></asp:Label></p>
									</li>
									<li>
										<p class="NomesCamposListView">Curso <i class="fas fa-clipboard"></i></p>	
										<p class="ValoresBancoDeDados"><asp:Label ID="LabelNomeCurso" runat="server" Text='<%#Eval("CURSO") %>'></asp:Label></p>
										<asp:Label ID="LabelCodigoCurso" runat="server" Visible="false" Text='<%#Eval("cod_curso") %>'></asp:Label>
									</li>									
									<li>
										<p class="NomesCamposListView">Palavras-Chave <i class="fas fa-key"></i></p>
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
									<li style="padding-bottom:10px;">

									</li>
								</ul>
							</span></div>					
						</ItemTemplate>	
						<LayoutTemplate>
							<div class="DivBotoesPagina">						
								<asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
								<ul class="PaginaAtual">
									<li>
										<asp:DataPager ID="DataPager1" runat="server" PagedControlID="lv_titulos_alunos" PageSize="8">
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
					<p id="AvisaErroP" runat="server"><asp:Label ID="LabelAvisaErro" runat="server"></asp:Label></p>
				</div>
			</form>
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
	</body>
</html>
