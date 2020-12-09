//Active menu
$(document).ready(function () {
    var url = window.location.href;
    var project = url.indexOf("Project");
    var news = url.indexOf("News");
    var contact = url.indexOf("Contact");

    var isHome = true;
    if (project > -1) {
        $("#liProject > a").addClass("active-item");
        isHome = false;
    }
    if (news > -1) {
        $("#liNews > a").addClass("active-item");
        isHome = false;
    }
    if (contact > -1) {
        $("#liContact > a").addClass("active-item");
        isHome = false;
    }

    if (!isHome)
        $("#liHome > a").removeClass("active-item");
    else
        $("#liHome > a").addClass("active-item");
});