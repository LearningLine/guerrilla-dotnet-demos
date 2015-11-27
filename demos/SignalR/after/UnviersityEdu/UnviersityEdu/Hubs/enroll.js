$(document).ready(function () {
	console.log("starting hub init...");
	var hub = $.connection.hub;
	var enroll = $.connection.enrollHub;

	var client = enroll.client;
	client.enrollmentStatuses = function (e) {
		console.log("status received: " + e);
		$(e).each(function() {
			var c = this;
			var srt = "course_" + c.id;
			console.log(srt);
			var count = 200 - c.enrollments;
			$(".spaces", "#" + srt).text(count);
		});

	};

	var svr = enroll.server;

	// enroll-btn
	// 
	hub.start().done(function () {
		console.log("hub started");
		svr.getEnrollmentStatus();
	});

	$(".enroll-btn").click(function (e) {
		e.preventDefault();

		var id = $(this).attr("id");
		console.log(id);
		svr.enrollIn(id);

		return false;
	});



});