function isNum(caractere) {
	var strValidos = "0123456789,"
	if (strValidos.indexOf(caractere) == -1)
		return false;
	return true;
}
function validaTecla(campo, event) {
	var BACKSPACE = 8, key, tecla;
	CheckTAB = true;
	if (navigator.appName.indexOf("Netscape") != -1)
		tecla = event.which;
	else
		tecla = event.keyCode;
	key = String.fromCharCode(tecla);
	//alert( 'key: ' + tecla + ' -> campo: ' + campo.value);
	if (tecla == 13)
		return false;
	if (tecla == BACKSPACE)
		return true;
	return (isNum(key));
}
/*Função Pai de Mascaras*/
function Mascara(o, f) {
	v_obj = o
	v_fun = f
	setTimeout("execmascara()", 1)
}
/*Função que Executa os objetos*/
function execmascara() {
	v_obj.value = v_fun(v_obj.value)
}
/*Função que padroniza valor monétario*/
function Valor(v) {
	v = v.replace(/\D/, "") //Remove tudo o que não é dígito
	v = v.replace(/^([0-9]{3}\?){3}-[0-9]{2}$/, "$1.$2");
	return v
}