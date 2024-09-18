"use strict"
// Connect Server
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
$("#sendButton").hide();

connection.start().then(function () {
    $("#sendButton").show();
}).catch(function () {
    return console.error(error.toString());
});

// 发送消息
$("#sendButton").click(function () {
    var username = $("#userInput").val();
    var message = $("#messageInput").text();
    connection.invoke("SendMessage", username, message).catch(function (err) {
        return console.error(err.toString());
    })
});

// 接收消息
connection.on("ReceiveMessage", function (user, message, time) {
    $("#content").append(`<p>${user} ${time}</p><p>${message}</p><br>`);
    $("#content").animate({ scrollTop: 100000 });
});

//查看聊天记录
var pageIndex = 1;
$("#findMessage").click(function () {
    $("#historyMessage").fadeIn();
    $.post("/Home/GetMessages", { pageIndex: pageIndex, pageSize: 10 }, function (data) {
        $.each(data, function (i, e) {
            $("#historyMessage").append(`<p>${e.uesrName} ${e.createTime}</p><p>${e.content}</p><br>`);
        });
    })
});


