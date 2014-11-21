/// <reference path="../Scripts/jquery-1.10.2.js" />
/// <reference path="../Scripts/jquery.signalR-2.1.2.js" />

"use strict";

$(function() {
    var chatHub = $.connection.chatHub;

    $.connection.log = true;

    chatHub.client.newClientConnected = function(name) {
        //console.log("someone joined " + name);

        $("<div>" + name + " has joined</div>").appendTo($("#messages"));

    };

    chatHub.client.onNewChat = function (name, message) {
        //console.log("someone joined " + name);

        $("<div>" + name + " says " + message + "</div>").appendTo($("#messages"));

    };

    $.connection.hub.start()
        .done(function(data) {
            console.log("Hub started successfully");

            $("#join").click(function () {
                var name = $("#name").val();
                chatHub.server.connect(name);
            });

            $("#chat").click(function () {
                var name = $("#name").val();
                var message = $("#message").val();
                chatHub.server.sendChat(name, message);
            });


        })
        .fail(function(err) {
            console.log("ouch " + err);
        });


});