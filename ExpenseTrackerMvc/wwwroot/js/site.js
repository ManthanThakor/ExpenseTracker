$(document).ready(function () {
    $("#logoutBtn").click(function () {
        localStorage.removeItem("jwt");
        alert("Logged out successfully!");
        window.location.href = "/Auth/Login";
    });
});