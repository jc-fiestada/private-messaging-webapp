import * as responseHandler from "./helpers/responseHandler.js";
// items
const userForm = document.getElementById("signupForm");
const fullName = document.getElementById("name");
const age = document.getElementById("age");
const sex = document.getElementById("sex");
const username = document.getElementById("username");
const password = document.getElementById("password");
// event listeners
userForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    const convertedAge = parseInt(age.value);
    const response = await fetch("/user-signup", {
        credentials: "include",
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            name: fullName.value,
            age: convertedAge,
            sex: sex.value,
            username: username.value,
            password: password.value
        })
    });
    const responseData = await response.json();
    if (response.status === 200) {
        window.location.href = "./home.html";
        return;
    }
    console.log("trigger this part");
    responseHandler.statusCode(response.status, responseData.errors);
    return;
});
//# sourceMappingURL=signup.js.map