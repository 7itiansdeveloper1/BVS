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
  }


$(document).ready(function(){

var scrollTo = $('.topbar li a.active').parent().position().left;                // retrieve its position relative to its parent
    //$('.scroller').scrollLeft(scrollTo); // simply update the scroll of the scroller
     var $el = $('.topbar ul');
      $el.scrollLeft(scrollTo-$el.scrollLeft()+($('.has-scroll li a.active').parent().width()/2));
      updateScroll2($el);
});