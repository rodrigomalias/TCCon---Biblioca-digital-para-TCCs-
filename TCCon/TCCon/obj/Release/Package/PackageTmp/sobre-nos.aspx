<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sobre-nos.aspx.cs" Inherits="TCCon.sobre_nos" %>

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
								<li class="active_link"><a href="/sobre-nos.aspx" style="border-bottom:none;"><i class="fa fa-user-circle" aria-hidden="true"></i> SOBRE NÓS</a> </li>  
								<li class="sliding_middle_out"><a href="/administracao.aspx"><i class="fa fa-lock" aria-hidden="true"></i> ADMINISTRAÇÃO</a> </li>	    
							</ul>
						</nav>				
					</div>
				</div>
			</div>
			<div class="inner">
				<div class="sobre_nos_inner">
					<div class="content_sobre_nos">
						<div class="content_title">
							<p> TRABALHO DE CONCLUSÃO DE CURSO REALIZADO POR ALUNOS DA FATEC ITAQUAQUECETUBA</p>
						</div>
						<div class="content_texto_sobre_nos">
							<div class="img_fatec">
								<img src="images/sobre-nos/fatec-itaqua.jpg" />	
							</div>
							<h2> BIBLIOTECA DE TCCS FATEC </h2>
							<p>
								TCCon é uma biblioteca que disponibiliza online os Trabalhos de Conclusão de Curso (TCCs) dos alunos da Fatec Itaquaquecetuba, 
								para que qualquer pessoa com internet possa acessá-los, sem a necessidade de login.
							</p>
							<p>
								A ideia do site surgiu com base nos problemas encontrados na Fatec Itaquaquecetuba, 
								na qual os alunos têm grande dificuldade para acessar os projetos dos demais Fatecanos.
							</p>
							<p>
								Esse espaço foi criado para melhorar e facilitar a busca desses trabalhos, a fim de nortear os alunos que estão iniciando essa atividade 
								que é obrigatória para a obtenção do seu diploma.
								E utilizar a tecnologia como um aliado para a saúde do planeta, já que assim torna-se possível a substituição do projeto encadernado pela mídia digital.
							</p>
							<p>
								O TCCon também é um projeto – TCC. Desenvolvido em 2018 pelos alunos Rodrigo Malias Aguiar e Thaynara Coutinho Perestrelo
								da Fatec Itaquaquecetuba do curso de Gestão da Tecnologia da Informação.
							</p>
						</div>
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
