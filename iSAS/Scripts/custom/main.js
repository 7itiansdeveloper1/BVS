$(document).ready(function(){


  $(".submenu > a").click(function(e) {
    e.preventDefault();
    var $li = $(this).parent("li");
    var $ul = $(this).next("ul");

    if($li.hasClass("open")) {
      $ul.slideUp(350);
      $li.removeClass("open");
    } else {
      $(".nav > li > ul").slideUp(350);
      $(".nav > li").removeClass("open");
      $ul.slideDown(350);
      $li.addClass("open");
    }
  });
  
  $("#menu-toggle").click(function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });
	
	
	
function toggleIcon(e) {
    $(e.target)
        .prev('.panel-heading')
        .find(".more-less")
        .toggleClass('glyphicon-plus glyphicon-minus');
}
$('.panel-group').on('hidden.bs.collapse', toggleIcon);
$('.panel-group').on('shown.bs.collapse', toggleIcon);

$("#headertoggle").click(function(e) {
        //e.preventDefault();
        $(".headertoggle").toggleClass("headertoggled");
		$(".loginMenu").slideToggle();
});

$('#startDate').daterangepicker({
          singleDatePicker: true,
          //startDate: moment().subtract(6, 'days')
});

$(function() {
    $('#existingpolicyowner').change(function(){
        if ($(this).val() == "1") {
            $('#yesno').show();
        } else {
            $('#yesno').hide();
        }
    });
});
  
  
  
});