function toggleMenu() {
    document.querySelector('.nav-links').classList.toggle('active');
    document.querySelector('.burger').classList.toggle('active');
}

document.querySelector(".dropdown > a").addEventListener("click", function (e) {
    if (window.innerWidth <= 768) {
        e.preventDefault();
        document.querySelector(".dropdown-menu").classList.toggle("active");
    }
});

window.addEventListener("scroll", function () {
    const navbar = document.querySelector(".navbar");
    navbar.style.background = window.scrollY > 50 ? "rgba(13, 17, 23, 0.95)" : "rgba(13, 17, 23, 0.8)";
});


// Show loading animation on form submit and page navigation
$(document).ready(function () {
    var loading = $('<div class="loading"><div class="loading-spinner"></div></div>');

    $('form').on('submit', function () {
        if ($(this).valid()) {
            $('body').append(loading);
        }
    });

    $('a:not([data-bs-toggle])').on('click', function () {
        var href = $(this).attr('href');
        if (href && href !== '#' && !href.startsWith('javascript') && !$(this).hasClass('no-loading')) {
            $('body').append(loading);
        }
    });
});