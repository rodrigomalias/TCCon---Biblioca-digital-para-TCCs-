<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TCCon.index" %>

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
								<li class="active_link"><a href="/index.aspx" style="border-bottom:none;"><i class="fa fa-home" aria-hidden="true"></i> INÍCIO</a> </li>
								<li class="sliding_middle_out"><a href="/monografias.aspx"><i class="fa fa-book" aria-hidden="true"></i> TCCS</a> </li>
								<li class="sliding_middle_out"><a href="/sobre-nos.aspx"><i class="fa fa-user-circle" aria-hidden="true"></i> SOBRE NÓS</a> </li>  
								<li class="sliding_middle_out"><a href="/administracao.aspx"><i class="fa fa-lock" aria-hidden="true"></i> ADMINISTRAÇÃO</a> </li>	    
							</ul>
						</nav>				
					</div>
				</div>
			</div>
			<div class="inner">
				<div class="inner_image">	
					<div class="content_top">
					</div>				
					<div class="content">
						<p>ACERVO DOS TRABALHOS DE CONCLUSÃO DE CURSO DA FATEC ITAQUAQUECETUBA.</p>
					</div>
					<div class="content_button">
						<ul>
							<li><a href="/monografias.aspx" class="sliding-u-l-r-l"><i class="fa fa-search" aria-hidden="true"></i> PESQUISAR</a> </li>
						</ul>
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
	</body>
</html>
