jQuery.fn.toggleText = function(a,b) {
	return   this.html(this.html().replace(new RegExp("("+a+"|"+b+")"),function(x){return(x==a)?b:a;}));
}
$(document).ready(function(){
	$('.tgl').before('<span><i class="fas fa-eye"></i> Mostrar resumo</span>');
	$('.tgl').css('display', 'none')
	$('span', '.box-toggle').click(function() {
		$(this).next().slideToggle('slow')
		.siblings('.tgl:visible').slideToggle('fast');
		// aqui começa o funcionamento do plugin
		$(this).toggleText('Mostrar','Esconder')
		.siblings('span').next('.tgl:visible').prev()
		.toggleText('Mostrar','Esconder')
	});
})