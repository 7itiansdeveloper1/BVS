// This is where it all goes :)

$(document).on('ready', function() {
  var $target = $('.js-swipenavigation');
  var hasTapEvent = ('ontouchstart' in window);

  if(hasTapEvent) {
    $target.find('a').each(function(index, el) {
      var $el = $(el);
      $el.addClass('hover-disabled');
      $el.on('touchstart', function(event) {
        $el.addClass('touch');
      });
      $el.on('touchend', function(event) {
        $el.removeClass('touch');
      });
    });
  } else {
    //mousewheel
    $target.on('mousewheel', function(event) {
      event.preventDefault();
      var $el = $target.find('ul');
      $el.scrollLeft($el.scrollLeft()-event.deltaY);
      updateScroll();
    });

    //next/back
    $(window).on('load resize', _.throttle(checkScroll, 150));
    $target.find('.next').on('click', function(event) {
      event.preventDefault();
      var $el = $target.find('ul');
      $el.scrollLeft($el.scrollLeft()+150);
      updateScroll();
    });
    $target.find('.back').on('click', function(event) {
      event.preventDefault();
      var $el = $target.find('ul');
      $el.scrollLeft($el.scrollLeft()-150);
      updateScroll();
    });
  }

  function updateScroll() {
    var $el = $target.find('ul');
    if(parseInt($el.scrollLeft()) == 0) {
      $target.find('.back').addClass('is-hidden');
    } else {
      $target.find('.back').removeClass('is-hidden');
    }

    var width = 0;
    $target.find('ul').find('li').each(function(index, el) {
      width += $(el).width();
    });
	
	//alert('width');

    if(parseInt($el.scrollLeft()) + parseInt($target.find('ul').width()) == width) {
	//if(parseInt($el.scrollLeft()) == 0) {	
      $target.find('.next').addClass('is-hidden');
    } else {
      $target.find('.next').removeClass('is-hidden');
    }
	
	/* When Last li active */
	/*if($target.find('li:last').find('a').hasClass('active')){
	  $target.parent().find('.next').addClass('is-hidden');
	  $target.find('.next').addClass('is-hidden');
	}*/
	
  }

  function checkScroll() {
    if($target.find('ul')[0].clientHeight == $target.find('ul')[0].offsetHeight) {
      $target.removeClass('has-scroll');
    } else {
      $target.addClass('has-scroll');
    }
    updateScroll();
  }
  
  

});


/* ==============================Active State Show====================================== */

function updateScroll2($target) {
    var $el = $target.find('ul');
    if(parseInt($el.scrollLeft()) == 0) {
      $target.find('.back').addClass('is-hidden');
    } else {
      $target.find('.back').removeClass('is-hidden');
    }

    var width = 0;
    $target.find('ul').find('li').each(function(index, el) {
      width += $(el).width();
    });

    if(parseInt($el.scrollLeft()) + parseInt($target.find('ul').width()) == width) {
      $target.find('.next').addClass('is-hidden');
    } else {
      $target.find('.next').removeClass('is-hidden');
    }
	
	/* When Last li active */
	/*if($target.find('li:last').find('a').hasClass('active')){
	  $target.parent().find('.next').addClass('is-hidden');
	  $target.find('.next').addClass('is-hidden');
	}*/
	
  }


$(document).ready(function(){

var scrollTo = $('.topbar li a.active').parent().position().left;                // retrieve its position relative to its parent
    //$('.scroller').scrollLeft(scrollTo); // simply update the scroll of the scroller
     var $el = $('.topbar ul');
      $el.scrollLeft(scrollTo-$el.scrollLeft()+($('.has-scroll li a.active').parent().width()/2));
      updateScroll2($el);
});

