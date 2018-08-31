<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="administracao.aspx.cs" Inherits="TCCon.administracao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
		<meta charset="UTF-8" />
		<link rel="shortcut icon" href="images/logo/icon.png" />
		<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
		<script src="https://use.fontawesome.com/releases/v5.0.12/js/all.js"></script>
		<link rel="stylesheet" href="css/style.css" type="text/css" /> 
		<script type="text/javascript" src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
 		<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.0.0/jquery.min.js"></script>
  		<script type="text/javascript" src="ScriptSite/hamburger_menu.js"></script>
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
								<li class="sliding_middle_out"><a href="/monografias.aspx"><i class="fa fa-book" aria-hidden="true"></i> TCCS</a> </li>
								<li class="sliding_middle_out"><a href="/sobre-nos.aspx"><i class="fa fa-user-circle" aria-hidden="true"></i> SOBRE NÓS</a> </li>  
								<li class="active_link"><a href="/administracao.aspx" style="border-bottom:none;"><i class="fa fa-lock" aria-hidden="true"></i> ADMINISTRAÇÃO</a> </li>	    
							</ul>
						</nav>				
					</div>
				</div>
			</div>
			<div class="inner">
				<div class="inner_image">	
					<form class="contact_form" name="contact_form" runat="server">
						<ul>
		    				<li><p class="IniciarSessao">INICIAR SESSÃO</p></li>

							<li><p> USUÁRIO <i class="fas fa-address-card"></i> </p></li>
							<li><asp:TextBox ID="txtLogin" runat="server" placeholder="Seu usuário" name="login" required="required" maxlength="200"></asp:TextBox></li>

							<li><p> SENHA <i class="fas fa-unlock-alt"></i> </p></li>
							<li><asp:TextBox ID="txtSenha" runat="server" type="password" placeholder="Sua Senha" required="required" maxlength="200"></asp:TextBox></li>

							<li><p class="LblAviso"><asp:Label ID="lblAviso" runat="server" Text=""></asp:Label></p></li>
							
							<li><button class="btn_login" runat="server" onServerClick="btnLogin_Click"><span class="SpanBtnLogin"><i class="fas fa-hand-point-up"></i> INICIAR SESSÃO</span></button></li>
							<li><asp:Button ID="btnLogin" runat="server" Text="" Visible="false" OnClick="btnLogin_Click"/></li>
							<!--<li><a href="/recuperar-login.aspx"> Esqueceu seu usuário?</a></li>
							<li><a href="/recuperar-email.aspx"> Esqueceu sua senha?</a></li>	-->
						</ul>
					</form>
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
	</body>
</html>
