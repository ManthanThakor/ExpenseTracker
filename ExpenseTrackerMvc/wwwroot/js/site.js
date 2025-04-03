function toggleMenu() {
    document.querySelector('.nav-links').classList.toggle('active');
    document.querySelector('.burger').classList.toggle('active');
}

// Dropdown in Mobile View
document.querySelector(".dropdown > a").addEventListener("click", function (e) {
    if (window.innerWidth <= 768) {
        e.preventDefault();
        document.querySelector(".dropdown-menu").classList.toggle("active");
    }
});

// Navbar Background on Scroll
window.addEventListener("scroll", function () {
    const navbar = document.querySelector(".navbar");
    navbar.style.background = window.scrollY > 50 ? "rgba(25, 25, 25, 0.95)" : "rgba(25, 25, 25, 0.8)";
});
