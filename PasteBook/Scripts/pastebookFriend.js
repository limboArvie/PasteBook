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
                alert('Something went wrong')
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
                if (currentLocation.pathname == "/PasteBookApp/friends" || currentLocation.pathname == "/PasteBookApp/Friends") {
                    FriendAcceptedSuccessFPage(data)
                }

                else {
                    FriendStatusSuccessProfile(data)
                }
                
            },

            error: function () {
                alert('Something went wrong')
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
                if (currentLocation.pathname == "/PasteBookApp/friends" || currentLocation.pathname == "/PasteBookApp/Friends") {
                    FriendIgnoredSuccessFPage(data)
                }

                else {
                    FriendStatusSuccessProfile(data)
                }
            },

            error: function () {
                alert('Something went wrong')
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