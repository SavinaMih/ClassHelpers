"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("PrivateMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user} said: ${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var name = document.getElementById("userName").innerHTML;
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendPrivateMessage", name, user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `You said: ${message}`;
});