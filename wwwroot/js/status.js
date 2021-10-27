"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/statusHub").build();

connection.on("ReceiveMessage", displayLocationData);

connection.start().catch(err => console.error(err.toString()));

function displayLocationData(message) {
    var obj = JSON.parse(message);
    var id = obj.Id;

    var element = document.getElementById(id);

    if (!element) {
        element = CreateRecordElement(id);
        document.getElementById("statusList").appendChild(element);
    }

    var location = element.querySelector(".location");
    location.textContent = `Location: ${obj.LinkName}`;

    var processed = element.querySelector(".processed");
    processed.textContent = `Processed: ${obj.Processed}`;

    var speed = element.querySelector(".speed");
    speed.textContent = `Speed: ${obj.Speed}`;

    var travel = element.querySelector(".travel");
    travel.textContent = `Travel Time: ${obj.TravelTime}`;
}

function CreateRecordElement(id) {
    var mainElement = document.createElement("li");
    mainElement.setAttribute("id", id);

    var containerDiv = document.createElement("div");
    containerDiv.setAttribute("class", "card");
    mainElement.appendChild(containerDiv);

    var row1 = document.createElement("div");
    row1.setAttribute("class", "row");
    var row2 = document.createElement("div");
    row2.setAttribute("class", "row");
    containerDiv.appendChild(row1);
    containerDiv.appendChild(row2);

    var location = document.createElement("label");
    location.setAttribute("class", "location col-md-6");
    var processed = document.createElement("label");
    processed.setAttribute("class", "processed col-md-6");
    var speed = document.createElement("label");
    speed.setAttribute("class", "speed col-md-6");
    var travel = document.createElement("label");
    travel.setAttribute("class", "travel col-md-6");

    row1.appendChild(location);
    row1.appendChild(processed);
    row2.appendChild(speed);
    row2.appendChild(travel);

    return mainElement;
}

function StartReading() {
    $.post("Home/StartReading", null, null);
}

function StopReading() {
    $.post("Home/StopReading", null, null);
}