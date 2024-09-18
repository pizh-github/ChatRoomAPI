"use strict"
// Connect Server
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
$("#sendButton").hide();

connection.start().then(function () {
    $("#sendButton").show();
}).catch(function () {
    return console.error(error.toString());
});

$("#sendButton").click(function () {
    var username = $("#userInput").val();
    var message = $("#messageInput").text();
    connection.invoke("SendMessage", username, message).catch(function (err) {
        return console.error(err.toString());
    })
});

connection.on("ReceiveMessage", function (user, message, time) {
    $("#content").append(`<p>${user} ${time}</p><p>${message}</p><br>`);
    $("#content").animate({ scrollTop: 100000 });
});


