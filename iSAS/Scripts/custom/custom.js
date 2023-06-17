$(document).ready(function(){
    
    /*Dynamic Table*/
$("#jsGrid").jsGrid({
                height: "400px",
                width: "100%",
                filtering: true,
                editing: true,
                inserting: true,
                sorting: true,
                paging: true,
                autoload: true,
                pageSize: 15,
                pageButtonCount: 5,
                deleteConfirm: "Do you really want to delete the client?",
                controller: db,
                fields: [
                    { name: "Name", type: "text", width: 150 },
                    { name: "Age", type: "number", width: 50 },
                    { name: "Address", type: "text", width: 200 },
                    { name: "Country", type: "select", items: db.countries, valueField: "Id", textField: "Name" },
                    { name: "Married", type: "checkbox", title: "Is Married", sorting: false },
                    { type: "control" }
                ]
}); 

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
    
$('#student_attendance_date, #tc_applied_date, #tc_create_date, #tc_issue_date').daterangepicker({
 singleDatePicker: true,
});    
    
$("#headertoggle").click(function(e) {
        //e.preventDefault();
        $(".headertoggle").toggleClass("headertoggled");
		$(".loginMenu").slideToggle();
});
    
$("#menu-toggle").click(function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
});   
    
//$(".responsive-calendar").responsiveCalendar({
//          //time: '2013-05',
//          events: {
//            "2017-05-21": {},
//            "2017-05-10": {}, 
//            "2017-05-03":{}, 
//            "2017-05-12": {}}
//});
    
   
    
  
});