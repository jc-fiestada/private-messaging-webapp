import * as responseHandler from "./helpers/responseHandler.js";
import { ApiResponse } from "./classes/ApiResponse";

// items 

const userForm = <HTMLFormElement>document.getElementById("signinForm");
const username = <HTMLInputElement> document.getElementById("username");
const password = <HTMLInputElement> document.getElementById("password");


userForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const response = await fetch("/user-signin", {
        method : "POST",
        credentials : "include",
        headers : {"Content-Type" : "application/json"},
        body : JSON.stringify({
            username : username.value,
            password : password.value
        })
    });

    if (response.status === 200){
        window.location.href = "./home.html";
        return;
    }

    const responseData : ApiResponse = await response.json();

    responseHandler.statusCode(response.status, responseData.errors);
    return;
})
