
$(document).ready(function () {

    //GetSessionLogin();

    FocusTabMenu();

    $('.date').datepicker({
        dateFormat: "dd/mm/yy"
    });

}); //end document.ready


//
function FocusTabMenu() {

    var url = window.location.pathname;

    switch (url) {
        case "/Admin/Home/Index":
        case "/Admin/Home":
            $('#tabHome').addClass('active');
            break;
        case "/Admin/Statistics":
            $('#tabStatics').addClass('active');
            break;
        case "/Admin/Slide/Index":
            $('#tabSlide').addClass('active');
            break;
        case "/Admin/Category/Index":
            $('#tabCategory').addClass('active');
            break;
        case "/Admin/Order/Index":
            $('#tabOrder').addClass('active');
            break;
        case "/Admin/Customer/GetAllCustomer":
            $('#tabCustomer').addClass('active');
            break;
        case "/Admin/Product/Index":
            $('#tabProduct').addClass('active');
            break;
        case "/Admin/News/Index":
            $('#tabNews').addClass('active');
            break;
        case "/Admin/Security/GetAllUser":
            $('#tabUser').addClass('active');
            break;
        default:
            break;
    }

}


////lấy thông tin đối tượng vừa đăng nhập
//function GetSessionLogin() {

//    $.ajax({
//        url: '/Home/GetUserLogin',
//        type: 'GET',
//        success: function (response) {
//            $("#userNameLogin").text(response.UserName);
//            var role = response.Role;
//            if (role != 1) {
//                $("#tabUser").hide(); 
//            }
//        },
//        error: function (result) {
//            console.log(result.responseText);
//        }
//    });

//}