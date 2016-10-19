$(document).ready(function () {
    $('#btnAddFriend').on('click', function () {
        var data = {
            "CurrentProfile": currentProfileID
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