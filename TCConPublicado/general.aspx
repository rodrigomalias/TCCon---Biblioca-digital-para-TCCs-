<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="general.aspx.cs" Inherits="TCCon.general" %>

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
		<title> TCCon - BIBLIOTECA DIGITAL </title>
	</head>
	<body>
		<form id="form1" runat="server">
		<div class="global">
			<div class="top_software">
				<div class="div_usuario">
					<p>  Usuário <i class="far fa-user"></i> : <asp:Label ID="lblUsuario" runat="server"></asp:Label></p>
				</div>
				<div class="div_unidade_1024px">
					<p> <i class="far fa-building fa-2x"></i> <asp:Label ID="lblNomeUnidade1024px" runat="server" Text=""></asp:Label></p>
				</div>
				<div class="div_btnlogout">
					<asp:Button ID="btnLogout" runat="server" Visible="False"/>
					<button class="btnLogout" runat="server" onServerClick="btnLogout_Click"><i class="fas fa-sign-out-alt"></i> SAIR</button>
				</div>
				<div class="div_unidade_1023px">
					<p> <i class="far fa-building fa-2x"></i> <asp:Label ID="lblNomeUnidade1023px" runat="server" Text=""></asp:Label></p>
				</div>
			</div>
			<div class="inner">	
				<div class="general_inner">
					<nav>
						<ul>
							<li><a href="/cad-documentos.aspx"><i class="fas fa-graduation-cap"></i> CADASTRAR TÍTULOS</a> </li>
							<li><a href="/cad-cursos.aspx"><i class="fa fa-book" aria-hidden="true"></i> CADASTRAR CURSOS</a> </li>
						</ul>
					</nav>
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
