//Layout scripts 
$(function () {
    $('#lang_ru').click(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/api/culture/changeculture',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify("ru"),
            success: function () {
                location.reload(true);
            },
            error: function (obj, error, status) {
                var response = jQuery.parseJSON(obj.responseText);
                if (response['ModelState']) {
                    $.each(response['ModelState'], function (index, item) { alert(item + '\n'); });
                }
            }
        });
    });
});

$(function () {
    $('#lang_en').click(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/api/culture/changeculture',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify("en"),
            success: function () {
                location.reload(true);
            },
            error: function (obj, error, status) {
                var response = jQuery.parseJSON(obj.responseText);
                if (response['ModelState']) {
                    $.each(response['ModelState'], function (index, item) { alert(item + '\n'); });
                }
            }
        });
    });
});

$(function () {
    $('#logoff').click(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/api/account/logout',
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            success: function () {
                location.reload(true);
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
    });
});

function show_nav(obj, name) {
    $("#menuCustomers").css("display", "none");
    $("#menuCoaches").css("display", "none");
    $("#menuManagers").css("display", "none");
    $("#" + name).css("display", "block");
    $('#data_result').empty();
    $('#title_main_cont_level_2').text("");
    $('#title_main_cont_level_1').text(obj.text);
}

$(function () {
    $('.my-nav').click(function (e) {
        e.preventDefault();
        $('#title_main_cont_level_2').text($(this).text());
    });
});


//EditInfoPartia;
function edit_personal_info() {
    $.ajax({
        url: '/api/account/getuserinfo',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            document.getElementById('modal_editing_per_info').style.display = 'block';
            document.getElementById('mySidebar').style.zIndex = '0';
            switch (data.Role) {
                case "Administrator":
                    WriteResponseAdmin(data);
                    document.getElementById('editing_per_info_admin_form').style.display = 'block';
                    break;

                case "Customer":
                    WriteResponseCustomer(data);
                    document.getElementById('editing_per_info_customer_form').style.display = 'block';
                    break;
            }

        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function Close_modal_editing() {
    document.getElementById('modal_editing_per_info').style.display = 'none';
    document.getElementById('editing_per_info_admin_form').style.display = 'none';
    document.getElementById('editing_per_info_customer_form').style.display = 'none';
    document.getElementById('role_status').innerHTML = '';
    document.getElementById('mySidebar').style.zIndex = '4';
}

function WriteResponseAdmin(user) {
    document.getElementById('username_admin_form').value = user.UserName;
    document.getElementById('firstname_admin_form').value = user.FirstName;
    document.getElementById('lastname_admin_form').value = user.LastName;
    document.getElementById('patronymic_admin_form').value = user.Patronymic;
    document.getElementById('role_status').innerHTML = user.Role;
}

function WriteResponseCustomer(user) {
    document.getElementById('username_customer_form').value = user.UserName;
    document.getElementById('firstname_customer_form').value = user.FirstName;
    document.getElementById('lastname_customer_form').value = user.LastName;
    document.getElementById('patronymic_customer_form').value = user.Patronymic;
    $("#dateofbirth_customer_form").val(user.DateOfBirth !== null ? user.DateOfBirthString : null);
    $("#customer_selected_gender").val(user.Sex);
    $('#role_status').text(user.Role);
    $("#growth_customer_form").val(user.Growth !== null ? user.Growth : null);
    $("#weight_customer_form").val(user.Weight !== null ? user.Weight : null);
    $("#address_customer_form").val(user.Address);
    $("#phone_customer_form").val(user.Phone);
    
}

$(function () {
    $("#editing_per_info_customer_form").submit(function (e) {
        e.preventDefault();
        var data = {
            Email: $('#username_customer_form').val(),
            FirstName: $('#firstname_customer_form').val(),
            LastName: $('#lastname_customer_form').val(),
            Patronymic: $('#patronymic_customer_form').val(),
            DateOfBirth: new Date(($('#dateofbirth_customer_form').val()).replace(/(\d+).(\d+).(\d+)/, '$3/$2/$1')),
            Role: $('#role_status').text(),
            Growth: $("#growth_customer_form").val(),
            Weight: $("#weight_customer_form").val(),
            Address: $("#address_customer_form").val(),
            Phone: $("#phone_customer_form").val(),
            Sex: $("#customer_selected_gender").val()
        };
        $.ajax({
            type: 'POST',
            url: '/api/account/updateuserinfo',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            success: function () {
                location.reload(true);
            },
            error: function (obj, error, status) {
                var response = jQuery.parseJSON(obj.responseText);
                if (response['ModelState']) {
                    $.each(response['ModelState'], function (index, item) { alert(item + '\n'); });
                }
            }
        });
    });
});

$(function () {
    $("#dateofbirth_customer_form, #dateofbirth_form_manager_by_admin, #dateofbirth_form_coach_by_admin, #dateofbirth_form_customer_by_admin").datepicker({ maxDate: 0, dateFormat: "dd.mm.yy" });
});

function open_sidebar() {
    document.getElementById("mySidebar").style.display = "block";
    document.getElementById("myOverlay").style.display = "block";
}

function close_sidebar() {
    document.getElementById("mySidebar").style.display = "none";
    document.getElementById("myOverlay").style.display = "none";
}








//LoginPartial, RegisterPartial
$(function () {
    $('#login_form').submit(function (e) {
        e.preventDefault();
        var data = {
            Email: $('#username_login').val(),
            Password: $('#psw_login').val()
        };
        $.ajax({
            type: 'POST',
            url: '/api/account/login',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            success: function () {
                location.reload(true);
            },
            error: function (obj, error, status) {
                var response = jQuery.parseJSON(obj.responseText);
                if (response['ModelState']['Account.login']) {
                    $.each(response['ModelState']['Account.login'], function (index, item) { alert(item + '\n'); });
                }
            }
        });
    });
});

function compare_password(item) {
    if ($(item).closest("form.register-form").find("input.password").val() !== 
        $(item).closest("form.register-form").find("input.password-confirm").val()) {
        alert("Ошибка: Поля 'Пароль' и 'Подтверждение пароля' не совпадают!");
        $(item).focus();
        return false;
    }
}

function change_to_register_modal() {
    document.getElementById('logon_modal').style.display = 'none';
    document.getElementById('register_modal').style.display = 'block';
}

function change_to_logon_modal() {
    document.getElementById('register_modal').style.display = 'none';
    document.getElementById('logon_modal').style.display = 'block';
}

$(function () {
    $('#register_form').submit(function (e) {
        e.preventDefault();
        var data = {
            Email: $('#email').val(),
            Password: $('#psw').val(),
            ConfirmPassword: $('#psw_confirm').val(),
            FirstName: $('#firstname').val(),
            LastName: $('#lastname').val(),
            Patronymic: $('#patronymic').val()
        };
        $.ajax({
            type: 'POST',
            url: '/api/account/register',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            success: function () {
                location.reload(true);
            },
            error: function (obj, error, status) {
                var response = jQuery.parseJSON(obj.responseText);
                if (response['ModelState']) {
                    $.each(response['ModelState'], function (index, item) { alert(item + '\n'); });
                }
            }
        });
    });
});



$(function () {
    $(".close-modal").click(function (e) {
        e.preventDefault();        
        $("#mySidebar").css("zIndex", "4");
    });
});


//Administrator area

$(function () {
    $("#selected_role_register_by_admin").on("change", function () {
        $("#register_form_customer_by_admin, #register_form_coach_by_admin, #register_form_manager_by_admin").css("display", "none");
        $('#register_form_' + $("#selected_role_register_by_admin").val() + '_by_admin').css("display", "block");
    });
});

$(function () {
    $("#nav_add_customer").click(function () {
        $("#register_form_customer_by_admin").css("display", "block");
        $("#register_modal").css("display", "block");
    });
});

$(function () {
    $('#register_form_customer_by_admin').submit(function (e) {
        e.preventDefault();
        var data = {
            Email: $('#email_form_customer_by_admin').val(),
            Password: $('#psw_form_customer_by_admin').val(),
            ConfirmPassword: $('#psw_confirm_form_customer_by_admin').val(),
            FirstName: $('#firstname_form_customer_by_admin').val(),
            LastName: $('#lastname_form_customer_by_admin').val(),
            Patronymic: $('#patronymic_form_customer_by_admin').val(),
            Role: $('#selected_role_register_by_admin').val(),
            DateOfBirth: new Date(($('#dateofbirth_form_customer_by_admin').val()).replace(/(\d+).(\d+).(\d+)/, '$3/$2/$1')),
            Gender: $('#customer_selected_gender_register_form').val(),
            Growth: $('#growth_form_customer_by_admin').val(),
            Weight: $("#weight_form_customer_by_admin").val(),
            Address: $("#address_form_customer_by_admin").val(),
            Phone: $("#phone_form_customer_by_admin").val()
        };
        $.ajax({
            type: 'POST',
            url: '/api/administrator/registercustomer',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            success: function () {
                location.reload(true);
            },
            error: function (obj, error, status) {
                var response = jQuery.parseJSON(obj.responseText);
                if (response['ModelState']) {
                    $.each(response['ModelState'], function (index, item) { alert(item + '\n'); });
                }
            }
        });
    });
});

