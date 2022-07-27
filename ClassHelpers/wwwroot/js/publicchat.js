"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;
document.getElementById("sendFileButton").disabled = true;

connection.on("PublicMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} said: ${message}`;
});

connection.on("PublicFileMessage", function (user, filename, base64) {
    if (user != document.getElementById("userName").innerHTML) {
        var li = document.createElement("li");
        var a = document.createElement("a");
        a.textContent = "Download";
        a.href = base64;
        a.download = filename;
        li.textContent = `${user} sent ${filename}\n`;
        li.appendChild(a);
        document.getElementById("messagesList").appendChild(li);
    }
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
    document.getElementById("sendFileButton").disabled = false;
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

document.getElementById("sendFileButton").addEventListener("click", function (event) {
    var user = document.getElementById("userName").innerHTML;
    var file = document.getElementById("fileInput").files[0];
    var fileName = file.name;

    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
        connection.invoke("SendPublicFileMessage", user, fileName, reader.result).catch(function (err) {
            return console.error(err.toString());
        });
    }

    event.preventDefault();
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `You sent ${fileName}`;
});