$(document).ready(function () {
    $('.number').keyup(function () {
        $val = cms_decode_currency_format($(this).val());
        $(this).val(cms_encode_currency_format($val));
    });
}); //end document.ready

function cms_encode_currency_format(num) {
    if (!isNumeric(num)) {
        return '';
    }
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
}

function cms_decode_currency_format(obs) {
    if (obs == '')
        return '';
    else
        return parseInt(obs.replace(/,/g, ''));
}

function isSpace(string) {
    if (/\s/.test(string)) {
        return true;
    }
}

function isNumeric(s) {
    var re = new RegExp("^[0-9,]+$");
    return re.test(s);
}

function addProduct() {
    $.ajax({
        type: "GET",
        url: "/Admin/Product/AddProduct",
        //data: { ID: id },
        success: function (result) {
            $('#view-product').html(result);
        }
    });
}

function editProduct(id) {
    $.ajax({
        type: "GET",
        url: "/Admin/Product/EditProduct",
        data: { ID: id },
        success: function (result) {
            $('#view-product').html(result);
        }
    });
}

function addPost() {
    $.ajax({
        type: "GET",
        url: "/Admin/News/AddPost",
        //data: { ID: id },
        success: function (result) {
            $('#view-new').html(result);
        }
    });
}

function editPost(id) {
    $.ajax({
        type: "GET",
        url: "/Admin/News/EditPost",
        data: { ID: id },
        success: function (result) {
            $('#view-new').html(result);
        }
    });
}

function getUserDetail(/*id*/) {
    //if (!navigator.onLine) {
    //    swal({
    //        title: "Kiểm tra kết nối internet!",
    //        text: "",
    //        icon: "warning"
    //    })
    //    return;
    //}
    $.ajax({
        url: '/Admin/User/GetUserDetail',
        //data: { ID: id },
        type: 'POST',
        success: function (response) {
            $("#divUserDetail").html(response);
            $('#modalRole').modal('show');
        },
        error: function (result) {
            console.log(result.responseText);
        }
    });
}


function Login() {
    //window.location = '/Admin/Home/Index'
    //if (!navigator.onLine) {
    //    swal({
    //        title: "Kiểm tra kết nối internet!",
    //        text: "",
    //        icon: "warning"
    //    })
    //    return;
    //}
    var phone = $("#txtUsernameLogin").val();
    var password = $("#txtPasswordLogin").val();
    if (phone == "" || password == "") {
        swal({
            title: "Vui lòng nhập đầy đủ!",
            text: "",
            icon: "warning"
        })
        return;
    }
    $.ajax({
        url: '/Admin/Home/UserLogin',
        data: { phone: phone, password: password },
        type: 'POST',
        success: function (response) {
            if (response.Role == 1) {
                window.location.assign("/Admin/Home/Index");
            }
            else if (response.Role == 2) {
                swal({
                    title: "Tài khoản không có quyền đăng nhập!",
                    text: "",
                    icon: "warning"
                })
                $("#txtPasswordLogin").val("");
                $("#txtUsernameLogin").val("");
            }
            else if (response.FAIL_LOGIN == 2) {
                swal({
                    title: "Sai thông tin đăng nhập!",
                    text: "",
                    icon: "warning"
                })
                $("#txtPasswordLogin").val("");
            } else {
                swal({
                    title: "Hệ thống đang bảo trì",
                    text: "",
                    icon: "warning"
                })
            }
        },
        error: function (result) {
            console.log(result.responseText);
        }
    });
}

function logout() {
    //if (!navigator.onLine) {
    //    swal({
    //        title: "Kiểm tra kết nối internet!",
    //        text: "",
    //        icon: "warning"
    //    })
    //    return;
    //}
    $.ajax({
        url: '/Admin/Home/Logout',
        data: {},
        type: 'POST',
        success: function (response) {
            if (response == SUCCESS) {
                location.reload();
            }
        },
        error: function (result) {
            console.log(result.responseText);
        }
    });
}

function searchProduct() {
    var fromDate = $('#txtFromDateCategoryProduct').val().trim();
    var toDate = $('#txtToDateCategoryProduct').val().trim();
    var name = $('#name_product').val().trim();
    $.ajax({
        url: "/Admin/Category/SearchProduct",
        data: {
            Page: 1,
            FromDate: fromDate,
            ToDate: toDate,
            Name: name
        },
        success: function (result) {
            $('#listProductCategory').html(result);
        }
    });
}

function searchPost() {
    var fromDate = $('#txtFromDateCategoryPost').val().trim();
    var toDate = $('#txtToDateCategoryPost').val().trim();
    var name = $('#name_post').val().trim();
    $.ajax({
        url: "/Admin/Category/SearchPost",
        data: {
            Page: 1,
            FromDate: fromDate,
            ToDate: toDate,
            Name: name
        },
        success: function (result) {
            $('#listPostCategory').html(result);
        }
    });
}

function deleteCategoryProduct($ID) {
    swal({
        title: "Bạn chắc chắn xóa chứ?",
        text: "",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((isConFirm) => {
            if (isConFirm) {
                $.ajax({
                    url: '/Admin/Category/DeleteCategoryProduct',
                    data: { ID: $ID },
                    success: function (response) {
                        if (response == 1) {
                            swal({
                                title: "Thông báo",
                                text: "Xóa thành công!",
                                icon: "success"
                            });
                            searchProduct();
                        }
                    }
                });
            }
        });
}

function deleteCategoryPost($ID) {
    swal({
        title: "Bạn chắc chắn xóa chứ?",
        text: "",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((isConFirm) => {
            if (isConFirm) {
                $.ajax({
                    url: '/Admin/Category/DeleteCategoryPost',
                    data: { ID: $ID },
                    success: function (response) {
                        if (response == 1) {
                            swal({
                                title: "Thông báo",
                                text: "Xóa thành công!",
                                icon: "success"
                            });
                            searchPost();
                        }
                    }
                });
            }
        });
}

function editCategoryProduct($ID) {
    $.ajax({
        url: '/Admin/Category/EditCategoryProduct',
        data: { ID: $ID },
        type: 'POST',
        success: function (response) {
            $("#UpdateCategory").html(response);
            $('#modal_edit_category_product').modal('show');
        },
        error: function (result) {
            console.log(result.responseText);
        }
    });
}

function editCategoryPost($ID) {
    $.ajax({
        url: '/Admin/Category/EditCategoryPost',
        data: { ID: $ID },
        type: 'POST',
        success: function (response) {
            $("#UpdateCategory").html(response);
            $('#modal_edit_category_post').modal('show');
        },
        error: function (result) {
            console.log(result.responseText);
        }
    });
}

function saveEditCategoryProduct() {
    var id = $.trim($('#id_product').val());
    var name = $.trim($('#name_edit_product').val());
    var status = $("#status_edit_product").val();
    if (name == "") {
        swal({
            title: "Thông báo",
            text: "Mời bạn nhập vào tên danh mục",
            icon: "warning"
        });
        return;
    }
    else {
        $.ajax({
            url: '/Admin/Category/SaveEditCategoryProduct',
            data: {
                ID: id,
                Name: name,
                Status: status
            },
            type: "POST",
            success: function (response) {
                if (response == 1) {
                    swal("Sửa danh mục thành công", "", "success");
                    searchProduct();
                    $("#modal_edit_category_product").modal('hide');
                }
                else {
                    swal("có lỗi xảy ra", "không thể sửa danh mục", "warning");
                }
            }
        });
    }

}

function saveEditCategoryPost() {
    var id = $.trim($('#id_post').val());
    var name = $.trim($('#name_edit_post').val());
    var status = $("#status_edit_post").val();
    if (name == "") {
        swal({
            title: "Thông báo",
            text: "Mời bạn nhập vào tên danh mục",
            icon: "warning"
        });
        return;
    }
    else {
        $.ajax({
            url: '/Admin/Category/SaveEditCategoryPost',
            data: {
                ID: id,
                Name: name,
                Status: status
            },
            type: "POST",
            success: function (response) {
                if (response == 1) {
                    swal("Sửa danh mục thành công", "", "success");
                    searchPost();
                    $("#modal_edit_category_post").modal('hide');
                }
                else {
                    swal("có lỗi xảy ra", "không thể sửa danh mục", "warning");
                }
            }
        });
    }

}

// Tạo danh mục sản phẩm
function createCategoryProduct() {
    var name = $.trim($('#name_category_product').val());
    if (name == "") {
        swal({
            title: "Thông báo",
            text: "Vui lòng nhập vào tên danh mục",
            icon: "warning"
        })
        return;
    }
    else {
        $.ajax({
            url: "/Admin/Category/CreateCategoryProduct",
            data: {
                Name: name
            },
            success: function (response) {
                if (response == 2) {
                    swal("Không thể tạo danh mục sản phẩm", "Danh mục đã tồn tại!", "warning")
                }
                else if (response == 1) {
                    $("#modal_add_category_product").modal('hide');
                    $('#name_category_product').val("");
                    swal("Tạo danh mục thành công", "", "success");
                    searchProduct();
                }
                else {
                    swal("có lỗi xảy ra", "không thể tạo danh mục", "warning");
                }
            }
        });
    }
}

function createCategoryPost() {
    var name = $.trim($('#name_category_post').val());
    if (name == "") {
        swal({
            title: "Thông báo",
            text: "Vui lòng nhập vào tên danh mục",
            icon: "warning"
        })
        return;
    }
    else {
        $.ajax({
            url: "/Admin/Category/CreateCategoryPost",
            data: {
                Name: name
            },
            success: function (response) {
                if (response == 2) {
                    swal("Không thể tạo danh mục bài viết", "Danh mục đã tồn tại!", "warning")
                }
                else if (response == 1) {
                    $("#modal_add_category_post").modal('hide');
                    $('#name_category_post').val("");
                    swal("Tạo danh mục thành công", "", "success");
                    searchPost();
                }
                else {
                    swal("có lỗi xảy ra", "không thể tạo danh mục", "warning");
                }
            }
        });
    }
}

function searchCustomer() {
    var fromDate = $('#dtFromdateCus').val().trim();
    var toDate = $('#dtTodateCus').val().trim();
    var phone = $('#txtPhoneCus').val().trim();
    $.ajax({
        url: "/Admin/Customer/Search",
        data: {
            Page: 1,
            FromDate: fromDate,
            ToDate: toDate,
            Phone: phone,
        },
        success: function (result) {
            $('#listCustomer').html(result);
        }
    });
}

// start tìm kiếm user
function searchUser() {
    //if (!navigator.onLine) {
    //    swal({
    //        title: "Kiểm tra kết nối internet!",
    //        text: "",
    //        icon: "warning"
    //    })
    //    return;
    //}
    var phone = $.trim($("#txtPhoneUser").val());
    var fromDate = $.trim($("#txtFromDateUser").val());
    var toDate = $.trim($("#txtToDateUser").val());
    $.ajax({
        url: '/Admin/User/Search',
        data: {
            Page: 1,
            Phone: phone,
            FromDate: fromDate,
            ToDate: toDate,
        },
        type: 'POST',
        success: function (response) {
            $("#tableUser").html(response);
        },
        error: function (result) {
            console.log(result.responseText);
        }
    });
}// end tìm kiếm user

// Xóa Cus
function deleteCus(id) {
    swal({
        title: "Bạn chắc chắn xóa chứ?",
        text: "",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((isConFirm) => {
            if (isConFirm) {
                $.ajax({
                    url: "/Admin/Customer/DeleteCustomer",
                    data: { ID: id },
                    success: function (result) {
                        if (result == 1) {
                            swal({
                                title: "Xóa thành công",
                                text: "",
                                icon: "success"
                            })
                            searchCustomer();
                        }
                        else {
                            swal({
                                title: "Có lỗi",
                                text: "",
                                icon: "warning"
                            })
                        }
                    }
                });
            }
        })
}

function refreshUser(id) {
    //if (!navigator.onLine) {
    //    swal({
    //        title: "Kiểm tra kết nối internet!",
    //        text: "",
    //        icon: "warning"
    //    })
    //    return;
    //}
    swal({
        title: "Bạn chắc chắn reset mật khẩu chứ?",
        text: "",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: '/Admin/User/ResetUser',
                    data: { ID: id },
                    type: "POST",
                    success: function (response) {
                        if (response == 1) {
                            swal({
                                title: "Reset thành công!",
                                text: "Đã reset mật khẩu! Mật khẩu mới: 123456",
                                icon: "success"
                            })
                        }
                        else {
                            swal({
                                title: "Thông báo",
                                text: "Hệ thống đang bảo trì! Vui lòng thử lại",
                                icon: "warning"
                            })
                        }
                    },
                    error: function (result) {
                        console.log(result.responseText);
                    }
                });
            }
        })
}

function refreshCus(id) {
    //if (!navigator.onLine) {
    //    swal({
    //        title: "Kiểm tra kết nối internet!",
    //        text: "",
    //        icon: "warning"
    //    })
    //    return;
    //}
    swal({
        title: "Bạn chắc chắn reset mật khẩu chứ?",
        text: "",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: '/Admin/Customer/ResetCus',
                    data: { ID: id },
                    type: "POST",
                    success: function (response) {
                        if (response == 1) {
                            swal({
                                title: "Reset thành công!",
                                text: "Đã reset mật khẩu! Mật khẩu mới: 123456",
                                icon: "success"
                            })
                        }
                        else {
                            swal({
                                title: "Thông báo",
                                text: "Hệ thống đang bảo trì! Vui lòng thử lại",
                                icon: "warning"
                            })
                        }
                    },
                    error: function (result) {
                        console.log(result.responseText);
                    }
                });
            }
        })
}

//đổi mật khẩu
function changePassword() {
    //if (!navigator.onLine) {
    //    swal({
    //        title: "Kiểm tra kết nối internet!",
    //        text: "",
    //        icon: "warning"
    //    })
    //    return;
    //}
    var currentPassword = $.trim($("#txtCurrentPassword").val());
    var newPassword = $.trim($("#txtNewPassword").val());
    var confirmPassword = $.trim($("#txtConfirmPassword").val());

    if (currentPassword == "" || newPassword == "" || confirmPassword == "") {
        swal({
            title: "Vui lòng nhập đầy đủ!",
            text: "",
            icon: "warning"
        })
        return;
    }
    if (newPassword.length < 6) {
        swal({
            title: "Mật khẩu tối thiểu 6 ký tự!",
            text: "",
            icon: "warning"
        })
        return;
    }
    if (newPassword != confirmPassword) {
        $("#txtConfirmPassword").val("");
        swal({
            title: "Mật khẩu xác nhận không đúng",
            text: "",
            icon: "warning"
        })
        return;
    }

    $.ajax({
        url: '/Admin/User/ChangePassword',
        data: {
            CurrentPassword: currentPassword,
            NewPassword: newPassword
        },
        type: 'POST',
        success: function (response) {
            if (response == 1) {
                $("#changePassword").modal("hide");
                swal({
                    title: "Đổi mật khẩu thành công",
                    text: "",
                    icon: "success"
                })
                window.location.reload();
            } else
                if (response == 2) {
                    $("#txtCurrentPassword").val("");
                    swal({
                        title: "Mật khẩu cũ không đúng",
                        text: "",
                        icon: "warning"
                    })
                } else {
                    swal({
                        title: "Không thể đổi mật khẩu",
                        text: "",
                        icon: "warning"
                    })
                }
        },
        error: function (result) {
            console.log(result.responseText);
            swal({
                title: "Có lỗi",
                text: "",
                icon: "warning"
            })
        }
    });
}

// start tạo user
function createUser() {
    //if (!navigator.onLine) {
    //    swal({
    //        title: "Kiểm tra kết nối internet!",
    //        text: "",
    //        icon: "warning"
    //    })
    //    return;
    //}
    var phone = $.trim($("#txtPhoneCreateUser").val());
    var userName = $.trim($("#txtNameCreateUser").val());
    var userPass = $.trim($("#txtPassCreateUser").val());
    var userRePass = $.trim($("#txtRePassCreateUser").val());
    var vnf_regex = /((09|03|01|02|04|06|07|08|05)+([0-9]{8})\b)/g;


    if (phone.length == "" || userName.length == "" || /*userEmail.length == "" ||*/
        userPass.length == "" || userRePass.length == "") {
        swal({
            title: "Thông báo",
            text: "Vui lòng nhập đầy đủ thông tin!",
            icon: "warning"
        })
        return;
    } else if (userPass.length < 6 || userRePass.length < 6) {
        swal({
            title: "Thông báo",
            text: "Mật khẩu phải lớn hơn hoặc bằng 6 ký tự!",
            icon: "warning"
        })
        return;
    } else if (!vnf_regex.test(phone)) {
        swal({
            title: "Số điện thoại của bạn không đúng định dạng!",
            text: "",
            icon: "warning"
        })
        return;
    } else if (userPass != userRePass) {
        swal({
            title: "Thông báo",
            text: "Xác nhận mật khẩu sai!",
            icon: "warning"
        })
        return;
    }

    $.ajax({
        url: '/Admin/User/CreateUser',
        data: {
            Phone: phone,
            UserName: userName,
            userPass: userPass
        },
        type: 'POST',
        success: function (response) {
            if (response == 1) {
                $("#txtPhoneCreateUser").val("");
                $("#txtNameCreateUser").val("");
                $("#txtPassCreateUser").val("");
                $("#txtRePassCreateUser").val("");
                $('#createUser').modal('hide');
                swal({
                    title: "Thông báo",
                    text: "Tạo tài khoản thành công",
                    icon: "success"
                })
                searchUser();
            } else if (response == 2) {
                swal({
                    title: "Không thể tạo tài khoản!",
                    text: "Số điện thoại đã tồn tại. Vui lòng sử dụng số điện thoại khác.",
                    icon: "warning"
                })
                $("#createUser #txtPhoneCreateUser").val("");
            } else {
                swal({
                    title: "Có lỗi xảy ra!",
                    text: "",
                    icon: "warning"
                })
            }

        },
        error: function (result) {
            console.log(result.responseText);
        }
    });
}// end tạo user

// start xóa user
function deleteUser(id) {
    //if (!navigator.onLine) {
    //    swal({
    //        title: "Kiểm tra kết nối internet!",
    //        text: "",
    //        icon: "warning"
    //    })
    //    return;
    //}
    swal({
        title: "Bạn chắc chắn xóa chứ?",
        text: "",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: '/Admin/User/DeleteUser',
                    data: { ID: id },
                    type: "POST",
                    success: function (response) {
                        if (response == 1) {
                            swal({
                                title: "Xóa thành công!",
                                text: "",
                                icon: "success"
                            })
                            searchUser();
                        } else {
                            swal({
                                title: "Có lỗi xảy ra!",
                                text: "",
                                icon: "warning"
                            })
                        }
                    },
                    error: function (result) {
                        console.log(result.responseText);
                    }
                });
            }
        })
}// end xóa user

function editUser(id) {
    $.ajax({
        url: '/Admin/User/EditUser',
        data: { ID: id },
        type: 'POST',
        success: function (response) {
            $("#divUserDetail").html(response);
            $('#modalRole').modal('show');
        },
        error: function (result) {
            console.log(result.responseText);
        }
    });
}

function saveEditUser(id) {
    var name = $.trim($('#UserEdit').val());
    if (name == "") {
        swal({
            title: "Thông báo",
            text: "Mời bạn nhập vào tên tài khoản",
            icon: "warning"
        });
        return;
    }
    else {
        $.ajax({
            url: '/Admin/User/SaveEditUser',
            data: {
                ID: id,
                Name: name
            },
            type: "POST",
            success: function (response) {
                if (response == 1) {
                    swal("Sửa danh tài khoản thành công", "", "success");
                    searchUser();
                    $("#modalRole").modal('hide');
                }
                else {
                    swal("có lỗi xảy ra", "không thể sửa tài khoản", "warning");
                }
            }
        });
    }
}

function searchProductItem() {
    var fromDate = $('#dtFromdateProduct').val().trim();
    var toDate = $('#dtTodateProduct').val().trim();
    var name = $('#txtNameProduct').val().trim();
    var category_id = $('#txtCategoryProduct').val().trim();
    $.ajax({
        url: "/Admin/Product/Search",
        data: {
            Page: 1,
            FromDate: fromDate, 
            ToDate: toDate,
            Name: name,
            Category_id: category_id
        },
        success: function (result) {
            $('#tableProduct').html(result);
        }
    });
}

function createProduct() {
    var CategoryID = $('#cbbCategory').val();
    var Code = $('#codeCreateProduct').val().trim();
    var Name = $('#nameCreateProduct').val().trim();
    var Price = $('#priceCreateProduct').val().trim();
    var PriceSale = $('#priceSale').val().trim();
    var Image = $('#_txturlImage').val().trim();
    var Note = $.trim(CKEDITOR.instances['noteCreateProduct'].getData());
    var Description = $('#descriptionCreateProduct').val().trim();
    var New = 0;
    var Sale = 0;
    var Hot = 0;

    if ($('#new_product').is(':checked')) {
        New = 1;
    }

    if (PriceSale != "") {
        Sale = 1;
    }

    if ($('#sale_product').is(':checked')) {
        Sale = 1;
    }

    if ($('#hot_product').is(':checked')) {
        Hot = 1;
    }

    if (Price <= 0) {
        swal({
            title: "Thông báo",
            text: "Giá sản phẩm phải lớn hơn 0!",
            icon: "warning"
        })
        return;
    }

    if (Code.length = 0) {
        swal({
            title: "Thông báo",
            text: "Mã sản phẩm phải không được để trống!",
            icon: "warning"
        })
        return;
    }

    if (isSpace(Code)) {
        swal({
            title: "Thông báo",
            text: "Mã sản phẩm không được có khoảng trắng!",
            icon: "warning"
        })
        return;
    }

    if (Name == "" || Price == "" || Image == "" || Note == "" || Description == "") {
        swal({
            title: "Thông báo",
            text: "Vui lòng nhập đầy đủ thông tin!",
            icon: "warning"
        })
        return;
    }
    else {
        if (!isNumeric(Price)) {
            swal({
                title: "Thông báo",
                text: "Giá tiền chỉ được phép nhập số!",
                icon: "warning"
            })
            return;
        }
        else if (!isNumeric(PriceSale)) {
            swal({
                title: "Thông báo",
                text: "Giá tiền khuyến mãi chỉ được phép nhập số!",
                icon: "warning"
            })
            return;
        }
        else {
            $.ajax({
                url: '/Admin/Product/CreateProduct',
                data: {
                    CategoryID: CategoryID,
                    Code: Code,
                    Name: Name,
                    Price: Price,
                    PriceSale: PriceSale,
                    Note: Note,
                    ImageUrl: Image,
                    Description: Description,
                    New: New,
                    Sale: Sale,
                    Hot: Hot
                },
                type: 'POST',
                success: function (response) {
                    if (response == 1) {
                        swal({
                            title: "Thành công!",
                            text: "",
                            icon: "success"
                        });
                        $('#createItem').modal('hide');
                        window.location = "/Admin/Product/Index";
                        searchProductItem();
                    }
                    else if (response == 2) {
                        swal({
                            title: "Không thể tạo sản phẩm",
                            text: "Mã sản phẩm đã tồn tại. Vui lòng sử dụng mã khác.",
                            icon: "warning"
                        });
                    }
                    else {
                        swal({
                            title: "Có lỗi xảy ra!",
                            text: "Không thể tạo sản phẩm",
                            icon: "warning"
                        });
                    }
                },
                error: function (result) {
                    console.log(result.responseText);
                }
            });
        }
    }
}

function deleteProduct(id) {
    swal({
        title: "Bạn chắc chắn xóa chứ?",
        text: "",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((isconfirm) => {
        if (isconfirm) {
            $.ajax({
                url: '/Admin/Product/DeleteProduct',
                data: { ID: id },
                success: function (response) {
                    if (response == 1) {
                        swal({
                            title: "Thông báo",
                            text: "Xóa thành công!",
                            icon: "success"
                        });
                        searchProductItem();
                    }
                    else {
                        swal({
                            title: "Thông báo",
                            text: "Lỗi!",
                            icon: "warning"
                        });
                    }
                }
            });
        }
    })
}

//Lưu lại cập nhập
function editItem() {
    var ID = $('#ID').val();
    var CategoryID = $('#cbbCategory_edit').val();
    var Code = $('#CodeCreate_edit').val().trim();
    var Name = $('#NameCreate_edit').val().trim();
    var Price = $('#PriceCreate_edit').val().trim();
    var PriceSale = $('#priceSaleEdit').val().trim();
    var Description = $('#descriptionEditProduct').val().trim();
    //var Image = $('#tagImage').attr('src');
    var ImageUrl = "";
    var Note = $.trim(CKEDITOR.instances['NoteEdit'].getData());
    var New = 0;
    var Sale = 0;
    var Hot = 0;

    if ($('#new_product_edit').is(':checked')) {
        New = 1;
    }

    if ($('#best_sale_product_edit').is(':checked')) {
        Sale = 1;
    }

    if (PriceSale != '') {
        Sale = 1;
    }

    if ($('#hot_product_edit').is(':checked')) {
        Hot = 1;
    }

    $.each($('._lstImage'), function () {
        ImageUrl += $(this).attr('src') + ',';
    });

    ImageUrl = ImageUrl.slice(0, ImageUrl.length - 1);

    if (ImageUrl == "") {
        swal({
            title: "Thông báo",
            text: "Vui lòng chọn ảnh sản phẩm!",
            icon: "warning"
        })
        return;
    }

    if (Price <= 0) {
        swal({
            title: "Thông báo",
            text: "Giá sản phẩm phải lớn hơn 0!",
            icon: "warning"
        })
        return;
    }

    if ( Name == "" || Price == "" || Description == "" || Note == "") {
        swal({
            title: "Thông báo",
            text: "Vui lòng nhập đầy đủ thông tin!",
            icon: "warning"
        })
        return;
    }

    $.ajax({
        url: '/Admin/Product/SaveEditItem',
        data: {
            ID: ID,
            Code: Code,
            Name: Name,
            CategoryID: CategoryID,
            ImageUrl: ImageUrl,
            Note: Note,
            Price: Price,
            PriceSale: PriceSale,
            Description: Description,
            New: New,
            Sale: Sale,
            Hot: Hot
        },
        type: 'POST',
        success: function (response) {
            if (response == 1) {
                swal({
                    title: "Thành công!",
                    text: "",
                    icon: "success"
                });
                window.location = "/Admin/Product/Index";
            }
            else {
                swal({
                    title: "Có lỗi xảy ra!",
                    text: "Không thể sửa sản phẩm.",
                    icon: "warning"
                });
            }
        },
        error: function (result) {
            console.log(result.responseText);
        }
    });
}

function searchPostItem() {
    var fromDate = $('#dtFromdateNew').val().trim();
    var toDate = $('#dtTodateNew').val().trim();
    var name = $('#txtNameNew').val().trim();
    //var category_id = $('#txtCategoryPost').val().trim();
    $.ajax({
        url: "/Admin/News/Search",
        data: {
            Page: 1,
            FromDate: fromDate,
            ToDate: toDate,
            Name: name,
            //Category_id: category_id
        },
        success: function (result) {
            $('#tableNew').html(result);
        }
    });
}

function createPost() {
    //var CategoryID = $('#txtCategoryPost_add').val();
    var Name = $('#NameCreate_post').val().trim();
    var Image = $('#_txturlImage').val().trim();
    var Note = $.trim(CKEDITOR.instances['NoteCreate_post'].getData());
    var Description = $('#DescriptionCreate_post').val().trim();
    var New = 0;
    var Hot = 0;

    if ($('#new_post').is(':checked')) {
        New = 1;
    }

    if ($('#hot_post').is(':checked')) {
        Hot = 1;
    }

    if (Name == "" || Image == "" || Note == "" || Description == "" ) {
        swal({
            title: "Thông báo",
            text: "Vui lòng nhập đầy đủ thông tin!",
            icon: "warning"
        })
        return;
    }

    else {
        $.ajax({
            url: '/Admin/News/CreatePost',
            data: {
                //CategoryID: CategoryID,
                Name: Name,
                Note: Note,
                ImageUrl: Image,
                Description: Description,
                New: New,
                Hot: Hot
            },
            type: 'POST',
            success: function (response) {
                if (response == 1) {
                    swal({
                        title: "Thành công!",
                        text: "",
                        icon: "success"
                    });
                    window.location = "/Admin/News/Index";
                    searchPostItem();
                } else {
                    swal({
                        title: "Có lỗi xảy ra!",
                        text: "Không thể tạo sản phẩm",
                        icon: "warning"
                    });
                }
            },
            error: function (result) {
                console.log(result.responseText);
            }
        });
    }
}

function saveEditPost(id) {
   // var CategoryID = $('#txtCategoryPost_edit').val();
    var Name = $('#NamePost_edit').val().trim();
    var Description = $('#DescriptionEdit_post').val().trim();
    var ImageUrl = "";
    var Note = $.trim(CKEDITOR.instances['notePost_edit'].getData());

    var New = 0;
    var Hot = 0;

    if ($('#new_post_edit').is(':checked')) {
        New = 1;
    }

    if ($('#hot_post_edit').is(':checked')) {
        Hot = 1;
    }

    $.each($('._lstImage'), function () {
        ImageUrl += $(this).attr('src') + ',';
    });

    ImageUrl = ImageUrl.slice(0, ImageUrl.length - 1);

    if (ImageUrl == "") {
        swal({
            title: "Thông báo",
            text: "Vui lòng chọn ảnh bài viết!",
            icon: "warning"
        })
        return;
    }

    if (Name == "" || Description == "" || Note == "") {
        swal({
            title: "Thông báo",
            text: "Vui lòng nhập đầy đủ thông tin!",
            icon: "warning"
        })
        return;
    }

    $.ajax({
        url: '/Admin/News/SaveEditPost',
        data: {
            ID: id,
            Name: Name,
            //CategoryID: CategoryID,
            ImageUrl: ImageUrl,
            Note: Note,
            Description: Description,
            New: New,
            Hot: Hot
        },
        type: 'POST',
        success: function (response) {
            if (response == 1) {
                swal({
                    title: "Thành công!",
                    text: "",
                    icon: "success"
                });
                window.location = "/Admin/News/Index";
            }
            else {
                swal({
                    title: "Có lỗi xảy ra!",
                    text: "Không thể sửa sản phẩm.",
                    icon: "warning"
                });
            }
        },
        error: function (result) {
            console.log(result.responseText);
        }
    });
}

function deletePost(id) {
    swal({
        title: "Bạn chắc chắn xóa chứ?",
        text: "",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((isconfirm) => {
        if (isconfirm) {
            $.ajax({
                url: '/Admin/News/DeletePost',
                data: { ID: id },
                success: function (response) {
                    if (response == 1) {
                        swal({
                            title: "Thông báo",
                            text: "Xóa thành công!",
                            icon: "success"
                        });
                        searchPostItem();
                    }
                    else {
                        swal({
                            title: "Thông báo",
                            text: "Lỗi!",
                            icon: "warning"
                        });
                    }
                }
            });
        }
    })
}

function searchSlideItem() {
    var fromDate = $('#dtFromdateSlide').val().trim();
    var toDate = $('#dtTodateSlide').val().trim();
    $.ajax({
        url: "/Admin/Slide/Search",
        data: {
            Page: 1,
            FromDate: fromDate,
            ToDate: toDate
        },
        success: function (result) {
            $('#tableSlide').html(result);
        }
    });
}

function createSlide() {

    var Image = $('#_txturlImage').val().trim();

    if (Image == "") {
        swal({
            title: "Thông báo",
            text: "Vui lòng chọn ảnh slide!",
            icon: "warning"
        })
        return;
    }
    else {
        $.ajax({
            url: '/Admin/Slide/CreateSlide',
            data: {
                ImageUrl: Image
            },
            type: 'POST',
            success: function (response) {
                if (response == 1) {
                    swal({
                        title: "Thành công!",
                        text: "",
                        icon: "success"
                    });
                    window.location = "/Admin/Slide/Index";
                    searchSlideItem();
                } else {
                    swal({
                        title: "Có lỗi xảy ra!",
                        text: "Không thể tạo sản phẩm",
                        icon: "warning"
                    });
                }
            },
            error: function (result) {
                console.log(result.responseText);
            }
        });
    }
}

function editSlide(id) {
    $.ajax({
        url: '/Admin/Slide/EditSlide',
        data: { ID: id },
        type: 'POST',
        success: function (response) {
            $("#divSlideDetail").html(response);
            $('#modalSlide').modal('show');
        },
        error: function (result) {
            console.log(result.responseText);
        }
    });
}

function saveEditSlide(id) {
    var ImageUrl = "";
    $.each($('._lstImage'), function () {
        ImageUrl += $(this).attr('src') + ',';
    });

    ImageUrl = ImageUrl.slice(0, ImageUrl.length - 1);

    if (ImageUrl == "") {
        swal({
            title: "Thông báo",
            text: "Vui lòng chọn ảnh slide!",
            icon: "warning"
        })
        return;
    }

    $.ajax({
        url: '/Admin/Slide/SaveEditSlide',
        data: {
            ID: id,
            ImageUrl: ImageUrl
        },
        type: 'POST',
        success: function (response) {
            if (response == 1) {
                swal({
                    title: "Thành công!",
                    text: "",
                    icon: "success"
                });
                window.location = "/Admin/Slide/Index";
            }
            else {
                swal({
                    title: "Có lỗi xảy ra!",
                    text: "Không thể sửa sản phẩm.",
                    icon: "warning"
                });
            }
        },
        error: function (result) {
            console.log(result.responseText);
        }
    });
}

function deleteSlide(id) {
    swal({
        title: "Bạn chắc chắn xóa chứ?",
        text: "",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((isConFirm) => {
            if (isConFirm) {
                $.ajax({
                    url: '/Admin/Slide/DeleteSlide',
                    data: { ID: id },
                    success: function (response) {
                        if (response == 1) {
                            swal({
                                title: "Thông báo",
                                text: "Xóa thành công!",
                                icon: "success"
                            });
                            searchSlideItem();
                        }
                    }
                });
            }
        });
}