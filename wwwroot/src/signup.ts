import * as responseHandler from "./helpers/responseHandler.js";
import { ApiResponse } from "./classes/ApiResponse.js";

// items
const userForm = <HTMLFormElement>document.getElementById("signupForm");

const fullName = <HTMLInputElement>document.getElementById("name");
const age = <HTMLInputElement>document.getElementById("age");
const sex = <HTMLInputElement>document.getElementById("sex");
const username = <HTMLInputElement>document.getElementById("username");
const password = <HTMLInputElement>document.getElementById("password");


// event listeners

userForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const convertedAge = parseInt(age.value);

    const response = await fetch("/user-signup", {
        method : "POST",
        headers : {"Content-Type" : "application/json"},
        body : JSON.stringify({
            name : fullName.value,
            age : convertedAge,
            sex : sex.value,
            username : username.value,
            password : password.value
        })
    });

    const responseData: ApiResponse = await response.json();

    if (response.status === 200){
        window.location.href = "./home.html";
        return;
    }
    console.log("trigger this part");
    responseHandler.statusCode(response.status, responseData.errors);
});