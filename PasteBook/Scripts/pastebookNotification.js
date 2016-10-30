$(document).ready(function () {
    
    if (currentUser != 0) {
        GetNotifCount();
        setInterval(GetNotifCount, 3000);
    }
    
    function GetNotifCount() {
        $.ajax({
            url: notifCountUrl,
            type: 'POST',
            success: function (data) {
                UpdateBadge(data);
            },

            error: function () {
                window.location.href = errorPageUrl;
            }
        })
    }

    function UpdateBadge(data) {
        if (data.notifCount > 0) {
            $('#notifBadge').text(data.notifCount);
        }
        else {
            $('#notifBadge').text('');
        }
    }

    $(document).on('click', '#notifButton', function () {
        $('#divNotifArea').load(renderNotifUrl)

        $.ajax({
            url: notifSeenUrl,
            type: 'POST',
            success: function (data) {
                SeenNotifSuccess(data);
            },

            error: function () {
                window.location.href = errorPageUrl;
            }
        })
    });

    function SeenNotifSuccess(data) {
        GetNotifCount();
    }

});