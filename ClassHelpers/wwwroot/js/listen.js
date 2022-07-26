﻿"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

const toastNotification = document.getElementById('liveToast');
var toastSender = document.getElementById('senderName');
var toastContent = document.getElementById('messageContent');
var toastLink = document.getElementById('actionLink');

connection.on("PrivateMessage", function (user, message) {
    toastSender.innerHTML = user;
    toastContent.innerHTML = message;
    toastLink.textContent = "Go to conversation";
    toastLink.href = `/Chat/Message?user=${user}`;
    toastLink.removeAttribute("download");
    var toast = new bootstrap.Toast(toastNotification);
    toast.show();
});

connection.on("PrivateFileMessage", function (user, filename, base64) {
    toastSender.innerHTML = user;
    toastContent.innerHTML = "Received file: " + filename;
    toastLink.textContent = "Download file";
    toastLink.href = base64;
    toastLink.download = filename;
    var toast = new bootstrap.Toast(toastNotification);
    toast.show();
});

connection.start().then(function () { }).catch(function (err) {
    return console.error(err.toString());
});