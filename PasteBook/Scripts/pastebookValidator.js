$(document).ready(function () {
    $('#txtUsername').blur(function () {
        $('#errorUsername').text(CheckNull($(this).val(), "Username"));
        $('#errorUsername').text(CheckMax($(this).val(), 50, "Username"));
        UsernameExist($(this).val());
    });

    $('#txtEmailAdd').blur(function () {
        $('#errorEmailAdd').text(CheckNull($(this).val(), "Email Address"));
        $('#errorEmailAdd').text(CheckMax($(this).val(), 50, "Email Address"));
        EmailExist($(this).val());
    });

    $('#txtFirstName').blur(function () {
        $('#errorFirstName').text(CheckNull($(this).val(), "First Name"));
        $('#errorFirstName').text(CheckMax($(this).val(), 50, "First Name"));
        UsernameExist($(this).val());
    });

    $('#txtLastName').blur(function () {
        $('#errorLastName').text(CheckNull($(this).val(), "Last Name"));
        $('#errorLastName').text(CheckMax($(this).val(), 50, "Last Name"));
        UsernameExist($(this).val());
    });

    $('#txtPassword').blur(function () {
        $('#errorPassword').text(CheckNull($(this).val(), "Password"));
        $('#errorPassword').text(CheckMax($(this).val(), 50, "Password"));
        CheckPassword($(this).val(), $('#txtRePassword').val());
    });

    $('#txtMobile').blur(function () {
        $('#errorMobile').text(CheckMax($(this).val(), 50, "Mobile Number"));
    });

    function CheckNull(value, test) {
        if (value == "") {
            return "The " + test + " field is required.";
        }
    }

    function CheckMax(value, maxLength, test) {
        if (value.length > maxLength) {
            return test + " is too long. Maximum length is " + maxLength+".";
        }
    }

    function CheckPassword(password, repassword) {
        if (password != repassword && repassword != "") {
            $('#errorRePassword').text("Password must match.");
        }
    }

    function UsernameExist(value) {
        var data = {
            "username": value
        }

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

    function CheckUserSuccess(data) {
        if (data.usernameExist) {
            $('#errorUsername').text("Username already exists.")
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
            $('#errorEmailAdd').text("Email already been used by another account.")
        }
    }

});