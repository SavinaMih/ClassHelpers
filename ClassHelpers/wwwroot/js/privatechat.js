"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

const toastNotification = document.getElementById('liveToast');
var toastSender = document.getElementById('senderName');
var toastContent = document.getElementById('messageContent');
var toastLink = document.getElementById('actionLink');

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("PrivateMessage", function (user, message) {
    if (user == document.getElementById("userInput").innerHTML) {
        var li = document.createElement("li");
        document.getElementById("messagesList").appendChild(li);
        li.textContent = `${user} said: ${message}`;
    }
    else {
        toastSender.innerHTML = user;
        toastContent.innerHTML = message;
        toastLink.href = toastLink.href + `?user=${user}`;
        var toast = new bootstrap.Toast(toastNotification);
        toast.show();
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var name = document.getElementById("userName").innerHTML;
    var user = document.getElementById("userInput").innerHTML;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendPrivateMessage", name, user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `You said: ${message}`;
});

