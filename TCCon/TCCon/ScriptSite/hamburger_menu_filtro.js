(function($) {

  $(document).ready(function(){
	$('.DivMenuMobileFiltro').on('click', function() {
	toggleNavigation($(this), $('.DivMobileFiltro'));
      changeLetters($(this));
    });

    function toggleNavigation(btn, nav) {
      btn.toggleClass('open');
      nav.toggleClass('open');
    }

    function changeLetters(btn) {
		var m = $('.DivMenuMobileFiltro span.m');
		var e = $('.DivMenuMobileFiltro span.e');
		var n = $('.DivMenuMobileFiltro span.n');
		var u = $('.DivMenuMobileFiltro span.u');

      e.toggleClass('btn-close');

      if(btn.hasClass('open'))
      {
        m.text("E");
        n.text("I");
        u.text("T");
      }
      else
      {
        m.text("M");
        n.text("N");
        u.text("U");
      }
    }
  });

})(jQuery);