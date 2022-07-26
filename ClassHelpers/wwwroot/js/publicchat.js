"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("PublicMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} said: ${message}`;
});

connection.on("UserJoinedChatroom", function (user) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} joined the chatroom`;
});

connection.on("UserLeftChatroom", function (user) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} left the chatroom`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    connection.invoke("JoinChatroom", document.getElementById("userName").innerHTML);
}).catch(function (err) {
    return console.error(err.toString());
});

window.onunload = function () {
    document.getElementById("sendButton").disabled = false;
    connection.invoke("LeaveChatroom", document.getElementById("userName").innerHTML).catch(function (err) {
        return console.error(err.toString());
    });
};

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userName").innerHTML;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendPublicMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});