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
                AddFriendSuccess(data)
            },

            error: function () {
                alert('Something went wrong')
            }
        })
    });

    function AddFriendSuccess(data) {
        
    }
});