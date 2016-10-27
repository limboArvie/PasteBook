$(document).ready(function () {
    $('#btnAddFriend').on('click', function () {
        var data = {
            "USER_ID": currentUserID,
            "FRIEND_ID": currentProfileID,
            "REQUEST": "N",
            "BLOCKED":"N"
        }

        $.ajax({
            url: addFriendUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                FriendStatusSuccessProfile(data)
            },

            error: function () {
                window.location.href = errorPageUrl;
            }
        })
    });

    $(document).on('click', '.btnAcceptRequest', function () {

        var currentLocation = window.location;

        var data = {
            "USER_ID": currentUserID,
            "FRIEND_ID": this.value,
            "REQUEST": "Y",
            "BLOCKED":"N"
        }

        $.ajax({
            url: acceptRequestUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                if (currentLocation.pathname == "/pastebook.com/friends" || currentLocation.pathname == "/pastebook.com/Friends") {
                    FriendAcceptedSuccessFPage(data)
                }

                else {
                    FriendStatusSuccessProfile(data)
                }
                
            },

            error: function () {
                window.location.href = errorPageUrl;
            }
        })

    });

    $(document).on('click', '.btnIgnoreRequest',function () {

        var currentLocation = window.location;

        var data = {
            "USER_ID": currentUserID,
            "FRIEND_ID": this.value,
            "REQUEST": "N",
            "BLOCKED": "N"
        }

        $.ajax({
            url: ignoreRequestUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                if (currentLocation.pathname == "/pastebook.com/friends" || currentLocation.pathname == "/pastebook.com/Friends") {
                    FriendIgnoredSuccessFPage(data)
                }

                else {
                    FriendStatusSuccessProfile(data)
                }
            },

            error: function () {
                window.location.href = errorPageUrl;
            }
        })

    });

    function FriendStatusSuccessProfile(data) {
        $('#txtfullName').load(renderProfileBannerUrl)
    }

    function FriendAcceptedSuccessFPage(data) {
        window.location.href = renderFriendsPage
    }

    function FriendIgnoredSuccessFPage(data) {
        $('#pendingRequest').load(renderPendingRequestUrl)
    }
});