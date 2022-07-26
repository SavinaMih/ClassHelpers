"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

const toastNotification = document.getElementById('liveToast');
var toastSender = document.getElementById('senderName');
var toastContent = document.getElementById('messageContent');
var toastLink = document.getElementById('actionLink');

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;
document.getElementById("sendFileButton").disabled = true;

connection.on("PrivateMessage", function (user, message) {
    if (user == document.getElementById("userInput").innerHTML) {
        var li = document.createElement("li");
        document.getElementById("messagesList").appendChild(li);
        li.textContent = `${user} said: ${message}`;
    }
    else {
        toastSender.innerHTML = user;
        toastContent.innerHTML = message;
        toastLink.textContent = "Go to conversation";
        toastLink.href = `/Chat/Message?user=${user}`;
        toastLink.removeAttribute("download");
        var toast = new bootstrap.Toast(toastNotification);
        toast.show();
    }
});

connection.on("PrivateFileMessage", function (user, filename, base64) {
    if (user == document.getElementById("userInput").innerHTML) {
        var li = document.createElement("li");
        var a = document.createElement("a");
        a.textContent = "Download";
        a.href = base64;
        a.download = filename;
        li.textContent = `${user} sent ${filename}\n`;
        li.appendChild(a);
        document.getElementById("messagesList").appendChild(li);
    }
    else {
        toastSender.innerHTML = user;
        toastContent.innerHTML = "Received file: " + filename;
        toastLink.textContent = "Download file";
        toastLink.href = base64;
        toastLink.download = filename;
        var toast = new bootstrap.Toast(toastNotification);
        toast.show();
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    document.getElementById("sendFileButton").disabled = false;
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

document.getElementById("sendFileButton").addEventListener("click", function (event) {
    var name = document.getElementById("userName").innerHTML;
    var user = document.getElementById("userInput").innerHTML;
    var file = document.getElementById("fileInput").files[0];
    var fileName = file.name;

    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
        connection.invoke("SendPrivateFileMessage", name, user, fileName, reader.result).catch(function (err) {
            return console.error(err.toString());
        });
    }

    event.preventDefault();
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `You sent ${fileName}`;
});