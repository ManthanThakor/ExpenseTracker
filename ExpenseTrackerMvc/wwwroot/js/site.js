// Add this to your site.js file
$(document).ready(function () {
    // Handle logout button click
    $("#logoutBtn").click(function (e) {
        e.preventDefault();

        // Create a form element
        var form = $('<form></form>');
        form.attr('method', 'post');
        form.attr('action', '/Auth/Logout');

        // Add anti-forgery token
        var token = $('input[name="__RequestVerificationToken"]').val();
        if (token) {
            var tokenInput = $('<input type="hidden" name="__RequestVerificationToken" />');
            tokenInput.val(token);
            form.append(tokenInput);
        }

        // Append form to body and submit
        $('body').append(form);
        form.submit();
    });
});

// Mobile sidebar toggle functionality
document.addEventListener('DOMContentLoaded', function () {
    // Create sidebar toggle button
    const sidebarToggle = document.createElement('button');
    sidebarToggle.className = 'btn btn-link d-lg-none';
    sidebarToggle.innerHTML = '<i class="fas fa-bars"></i>';
    sidebarToggle.style.fontSize = '1.5rem';
    sidebarToggle.style.color = 'var(--primary-color)';
    sidebarToggle.style.marginLeft = '0.5rem';

    // Find navbar brand and insert after
    const navbarBrand = document.querySelector('.navbar-brand');
    if (navbarBrand) {
        navbarBrand.parentNode.insertBefore(sidebarToggle, navbarBrand.nextSibling);
    }

    const sidebar = document.querySelector('.sidebar');
    const overlay = document.createElement('div');
    overlay.className = 'sidebar-overlay';

    document.body.appendChild(overlay);

    // Toggle sidebar when button clicked
    sidebarToggle.addEventListener('click', function () {
        sidebar.classList.toggle('active');
        overlay.classList.toggle('active');
    });

    // Close sidebar when overlay clicked
    overlay.addEventListener('click', function () {
        sidebar.classList.remove('active');
        overlay.classList.remove('active');
    });

    // Close sidebar when clicking a link (for single page apps)
    if (sidebar) {
        sidebar.addEventListener('click', function (e) {
            if (e.target.closest('a')) {
                sidebar.classList.remove('active');
                overlay.classList.remove('active');
            }
        });
    }

    // Handle window resize
    function handleResize() {
        if (window.innerWidth > 991.98) {
            sidebar.classList.remove('active');
            overlay.classList.remove('active');
        }
    }

    window.addEventListener('resize', handleResize);

    // Close dropdowns when clicking outside
    document.addEventListener('click', function (e) {
        if (!e.target.closest('.dropdown') && !e.target.closest('.dropdown-menu')) {
            const openDropdowns = document.querySelectorAll('.dropdown-menu.show');
            openDropdowns.forEach(function (dropdown) {
                dropdown.classList.remove('show');
            });
        }
    });

    // Initialize tooltips
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Initialize popovers
    const popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });
});