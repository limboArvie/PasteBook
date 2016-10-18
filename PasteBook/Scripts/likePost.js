$(document).ready(function () {
    $('.btnLike').on('click', function () {

        var data = {
            "ID": null,
            "LIKED_BY": @Session["Userid"],
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

        function LikeSuccess(data) {
            alert('Liked')
        }
    });         
});