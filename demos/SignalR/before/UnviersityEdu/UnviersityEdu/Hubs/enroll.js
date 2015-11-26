$(function () {
    console.log("starting hub");

    var updateStatuses = function(statuses) {
        $(statuses).each(function(index, item) {
            var id = item.Id;
            var filter = "#course_" + id;

            console.log(JSON.stringify(item));
            $(".spaces", filter).text(item.RemainingSeats);
        });
    };

    $.connection.enrollHub.client.courseStatusChanges = function(statuses) {
        updateStatuses(statuses);
    }

    $.connection.hub.start()
        .done(function () {
            console.log("hubs started");
            $.connection.enrollHub.server.getStatuses()
                .done(function(statuses) {
                    console.log(statuses);

                    updateStatuses(statuses);


                });
        });

    $(".enroll-btn").click(function(eorb) {
        eorb.preventDefault();

        var id = $(this).attr("id");

        $.connection.enrollHub.server.enrollInCourse(id);

        return false;
    });
});