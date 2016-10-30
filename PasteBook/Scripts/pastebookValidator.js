$(document).ready(function () {

    $('.txtUsername').blur(function () {
        $('.errorUsername').text(CheckNull($(this).val().trim(), "Username"));
        ChechUsernameFormat($(this).val().trim());
        $('.errorUsername').text(CheckMax($(this).val().trim(), 50, "Username"));
        UsernameExist($(this).val().trim());
    });

    $('.txtEmailAdd').blur(function () {
        $('.errorEmailAdd').text(CheckNull($(this).val(), "Email Address"));
        $('.errorEmailAdd').text(CheckMax($(this).val(), 50, "Email Address"));
        EmailExist($(this).val());
    });

    $('.txtFirstName').blur(function () {
        $('.errorFirstName').text(CheckNull($(this).val(), "First Name"));
        ChechNameFormat($(this).val(), "First Name", "errorFirstName");
        $('.errorFirstName').text(CheckMax($(this).val(), 50, "First Name"));
    });

    $('.txtLastName').blur(function () {
        $('.errorLastName').text(CheckNull($(this).val(), "Last Name"));
        ChechNameFormat($(this).val(), "Last Name", "errorLastName");
        $('.errorLastName').text(CheckMax($(this).val(), 50, "Last Name"));
    });

    $('.txtPassword').blur(function () {
        $('.errorPassword').text(CheckNull($(this).val(), "Password"));
        $('.errorPassword').text(CheckMax($(this).val(), 50, "Password"));
        CheckPassword($(this).val(), $('.txtRePassword').val());
    });

    $('.txtRePassword').blur(function () {
        $('.errorRePassword').text(CheckNull($(this).val(), "Confirm Password"));
        CheckPassword($(this).val(), $('.txtPassword').val());
    });

    $('.txtBirthday').blur(function () {
        $('.errorBirthday').text(CheckNull($(this).val(), "Birthday"));
    });

    $('.txtMobile').blur(function () {
        $('.errorMobile').text(CheckMax($(this).val(), 50, "Mobile Number"));
    });

    $('#btnRegister').click(function () {
        CheckPassword($('.txtPassword').val(), $('.txtRePassword').val());
        UsernameExist($('.txtUsername').val());
        EmailExist($('.txtEmailAdd').val());
        $('.errorRePassword').text(CheckNull($('.txtRePassword').val(), "Confirm Password"));
    });

    function CheckNull(value, test) {
        if (value == "") {
            return "The " + test + " field is required.";
        }

        else {
            return "";
        }
    }

    function CheckMax(value, maxLength, test) {
        if (value.length > maxLength) {
            return test + " is too long. Maximum length is " + maxLength+".";
        }
    }

    function ChechUsernameFormat(username) {
        if (username.match(/^([A-Za-z0-9._ ])([A-Za-z0-9._])*(([A-Za-z0-9_. ])*)?$/) == false) {
            $('.errorUsername').text("Invalid Username format. Only alphanumeric characters(A-Za-z0-9), period(.) and underscore(_) are allowed.");
        }
    }

    function ChechNameFormat(name, field, id) {
        if (name.match(/^([A-Za-z0-9._ ])([A-Za-z0-9._])*(([A-Za-z0-9_. ])*)?$/) == false) {
            $('.'.concat(id)).text("Invalid " + field + " format. Only alphanumeric characters(A-Za-z0-9), period(.) and underscore(_) are allowed.");
        }
    }

    function CheckPassword(repassword, password) {
        if (password != repassword) {
            $('.errorRePassword').text("Password must match.");
        }
        else if(password == repassword && password != ""){
            $('.errorRePassword').text("");
        }
    }

    function UsernameExist(value) {
        var data = {
            "username": value
        }

        if (data.username != currentUsername) {
            $.ajax({
                url: checkUsername,
                data: data,
                type: 'POST',
                success: function (data) {
                    CheckUserSuccess(data);
                },

                error: function () {
                    window.location.href = errorPageUrl;
                }
            })
        }
    }

    function CheckUserSuccess(data) {
        if (data.usernameExist) {
            $('.errorUsername').text("Username already exists.")
        }
    }

    function EmailExist(value) {
        var data = {
            "email": value
        }

        $.ajax({
            url: checkEmail,
            data: data,
            type: 'POST',
            success: function (data) {
                CheckEmailSuccess(data);
            },

            error: function () {
                window.location.href = errorPageUrl;
            }
        })
    }

    function CheckEmailSuccess(data) {
        if (data.emailExist) {
            $('.errorEmailAdd').text("Email already been used.")
        }
    }

});