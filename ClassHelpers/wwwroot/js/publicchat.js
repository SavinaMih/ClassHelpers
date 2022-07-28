"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;
document.getElementById("sendFileButton").disabled = true;

connection.on("PublicMessage", function (user, message) {
    const html = `<li class="d-flex justify-content-between mb-4">
            <div class="card w-100">
              <div class="card-header d-flex justify-content-between p-3">
                <p class="fw-bold mb-0">${user}</p>
              </div>
              <div class="card-body">
                <p class="mb-0">
                  ${message}
                </p>
              </div>
            </div>
            <img src="https://cdn-icons-png.flaticon.com/512/149/149071.png" alt="avatar"
              class="rounded-circle d-flex align-self-start ms-3 shadow-1-strong" width="60">
          </li>`;
    var li = document.createElement("li");
    li.innerHTML = html;
    document.getElementById("messageList").appendChild(li);
});

connection.on("PublicFileMessage", function (user, filename, base64) {
    if (user != document.getElementById("userName").innerHTML) {
        const html = `<li class="d-flex justify-content-between mb-4">
            <img src="https://cdn-icons-png.flaticon.com/512/1643/1643239.png" alt="avatar"
            class="rounded-circle d-flex align-self-start me-3 shadow-1-strong" width="60">
            <div class="card w-100">
                <div class="card-header d-flex justify-content-between p-3">
                    <p class="fw-bold mb-0">File message</p>
                </div>
                <div class="card-body">
                    <p class="mb-0">
                        ${user} sent ${filename}. <a href=${base64} download=${filename}>Download</a>
                    </p>
                </div>
            </div>
        </li>`;
        var li = document.createElement("li");
        li.innerHTML = html;
        document.getElementById("messageList").appendChild(li);
    }
});

connection.on("UserJoinedChatroom", function (user) {
    const html = `<li class="d-flex justify-content-between mb-4">
            <img src="https://cdn-icons-png.flaticon.com/512/1759/1759348.png" alt="avatar"
            class="rounded-circle d-flex align-self-start me-3 shadow-1-strong" width="60">
            <div class="card w-100">
                <div class="card-header d-flex justify-content-between p-3">
                    <p class="fw-bold mb-0">Status message</p>
                </div>
                <div class="card-body">
                    <p class="mb-0">
                        ${user} joined the chatroom
                    </p>
                </div>
            </div>
        </li>`;
    var li = document.createElement("li");
    li.innerHTML = html;
    document.getElementById("messageList").appendChild(li);
});

connection.on("UserLeftChatroom", function (user) {
    const html = `<li class="d-flex justify-content-between mb-4">
            <img src="https://cdn-icons-png.flaticon.com/512/1759/1759348.png" alt="avatar"
            class="rounded-circle d-flex align-self-start me-3 shadow-1-strong" width="60">
            <div class="card w-100">
                <div class="card-header d-flex justify-content-between p-3">
                    <p class="fw-bold mb-0">Status message</p>
                </div>
                <div class="card-body">
                    <p class="mb-0">
                        ${user} left the chatroom
                    </p>
                </div>
            </div>
        </li>`;
    var li = document.createElement("li");
    li.innerHTML = html;
    document.getElementById("messageList").appendChild(li);
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
    const html = `<li class="d-flex justify-content-between mb-4">
        <img src="https://cdn-icons-png.flaticon.com/512/3135/3135715.png" alt="avatar"
            class="rounded-circle d-flex align-self-start me-3 shadow-1-strong" width="60">
            <div class="card w-100">
                <div class="card-header d-flex justify-content-between p-3">
                    <p class="fw-bold mb-0">${user}</p>
                </div>
                <div class="card-body">
                    <p class="mb-0">
                        You sent ${fileName}
                    </p>
                </div>
            </div>
    </li>`;
    var li = document.createElement("li");
    li.innerHTML = html;
    document.getElementById("messageList").appendChild(li);
});