import * as responseHandler from "./helpers/responseHandler.js";
// items 
const userForm = document.getElementById("signinForm");
const username = document.getElementById("username");
const password = document.getElementById("password");
userForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    const response = await fetch("/user-signin", {
        method: "POST",
        credentials: "include",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            username: username.value,
            password: password.value
        })
    });
    if (response.status === 200) {
        window.location.href = "./home.html";
        return;
    }
    const responseData = await response.json();
    responseHandler.statusCode(response.status, responseData.errors);
    return;
});
//# sourceMappingURL=signin.js.map