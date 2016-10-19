$(document).ready(function () {

    $('#btnPostFeed').on('click', function () {
        var data = {
            "Content": $('#txtAreaPostFeed').val()
        }

        if (data.Content == '') {
            //put bootsrap error
        }

        else {
            $.ajax({
                url: postUrl,
                data: data,
                type: 'POST',
                success: function (data) {
                    AddPostFeedSuccess(data)
                },

                error: function () {
                    alert('Something went wrong')
                }
            })
        }
    });

    $('#btnPostTimeLine').on('click', function () {
        var data = {
            "Content": $('#txtAreaPostTimeLine').val()
        }

        if (data.Content == '') {
            //put bootsrap error
        }

        else {
            $.ajax({
                url: postTLUrl,
                data: data,
                type: 'POST',
                success: function (data) {
                    AddPostTimeLineSuccess(data)
                },

                error: function () {
                    alert('Something went wrong')
                }
            })
        }
    });

    $(document).delegate('.btnLike','click', function () {

        var data = {
            "ID": null,
            "LIKED_BY": currentUserID,
            "POST_ID" : this.value
        }

        $.ajax({
            url: likeUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                LikeSuccess(data);
            },
                
            error: function () {
                alert('Something went wrong')
            }
        })
    });


    function AddPostFeedSuccess(data) {
        $('#txtAreaPostFeed').val('')
        $('#divFeedPost').load(renderPostUrl)
    }

    function AddPostTimeLineSuccess(data) {
        $('#txtAreaPostTimeLine').val('')
        $('#divTimeLinePost').load(renderPostTLUrl)
    }

    function LikeSuccess(data) {
        $('#divFeedPost').load(renderPostUrl);
    }
});