// Toggle Mobile Menu & More Dropdown
function toggleMenu() {
    document.querySelector('.nav-links').classList.toggle('active');
    document.querySelector('.burger').classList.toggle('active');
}

// Ensure Dropdown Works Properly in Mobile View
document.querySelector(".dropdown > a").addEventListener("click", function (e) {
    if (window.innerWidth <= 768) {
        e.preventDefault(); // Prevent link redirection
        document.querySelector(".dropdown-menu").classList.toggle("active");
    }
});

// Change Navbar on Scroll
window.addEventListener("scroll", function () {
    const navbar = document.querySelector(".navbar");
    navbar.style.background = window.scrollY > 50 ? "rgba(20, 20, 20, 0.85)" : "rgba(20, 20, 20, 0.6)";
});
