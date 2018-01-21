function open_sidebar() {
    document.getElementById("mySidebar").style.display = "block";
    document.getElementById("myOverlay").style.display = "block";
}

function close_sidebar() {
    document.getElementById("mySidebar").style.display = "none";
    document.getElementById("myOverlay").style.display = "none";
}

function compare_password() {
    if (document.getElementById('psw').value !== document.getElementById('psw_confirm').value) {
        alert("Ошибка: Поля 'Пароль' и 'Подтверждение пароля' не совпадают!");
        document.getElementById('psw_confirm').focus();
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
    $(".close-modal").click(function (e) {
        e.preventDefault();        
        $("#mySidebar").css("zIndex", "4");
    });
});
