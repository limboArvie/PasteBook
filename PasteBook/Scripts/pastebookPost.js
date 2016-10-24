$(document).ready(function () {

    $('#btnPostFeed').on('click', function () {
        var data = {
            "CONTENT": $('#txtAreaPostFeed').val(),
            "ID": 0,
            "PROFILE_OWNER_ID": currentUserID,
            "POSTER_ID": currentUserID,
            "CREATED_DATE": null
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
            "CONTENT": $('#txtAreaPostTimeLine').val(),
            "ID": 0,
            "PROFILE_OWNER_ID": currentUserID,
            "POSTER_ID": currentUserID,
            "CREATED_DATE": null
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

    $(document).on('click','.btnLike', function () {

        var currentLocation = window.location;

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
                if (currentLocation.pathname == "/PasteBookApp") {
                    LikeFSuccess(data);
                }

                else {
                    LikeTLSuccess(data);
                }
                
            },
                
            error: function () {
                alert('Something went wrong')
            }
        })
    });

    $(document).on('click', '.btnComment', function () {

        var data = {
            "ID": null,
            "POST_ID": this.value,
            "POSTER_ID": currentUserID,
            "CONTENT": $('#' + this.value).val(),
            "DATE_CREATED": null
        }

        $.ajax({
            url: commentUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                CommentFSuccess(data);
            },

            error: function () {
                alert('Something went wrong')
            }
        })
    });

    $(document).on('click', '.btnCommentTL', function () {

        var data = {
            "ID": null,
            "POST_ID": this.value,
            "POSTER_ID": currentUserID,
            "CONTENT": $('#' + this.value).val(),
            "DATE_CREATED": null
        }

        $.ajax({
            url: commentTLUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                CommentTLSuccess(data);
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

    function LikeFSuccess(data) {
        $('#divFeedPost').load(renderPostUrl);
    }

    function LikeTLSuccess(data) {
        $('#divTimeLinePost').load(renderPostTLUrl);
    }

    function CommentFSuccess(data) {
        $('#divFeedPost').load(renderPostUrl);
    }

    function CommentTLSuccess(data) {
        $('#divTimeLinePost').load(renderPostTLUrl);
    }
});

