document.addEventListener('DOMContentLoaded', function () {
    // Color picker functionality
    const colorOptions = document.querySelectorAll('.color-option');
    const colorInput = document.getElementById('colorInput');
    const colorPreview = document.getElementById('colorPreview');

    colorOptions.forEach(option => {
        option.addEventListener('click', function () {
            const color = this.getAttribute('data-color');
            colorInput.value = color;
            colorPreview.style.backgroundColor = color;

            colorOptions.forEach(opt => opt.classList.remove('active'));
            this.classList.add('active');
        });
    });

    // Set initial active color if it matches
    if (colorInput.value) {
        const matchingOption = Array.from(colorOptions).find(opt =>
            opt.getAttribute('data-color').toLowerCase() === colorInput.value.toLowerCase());
        if (matchingOption) {
            matchingOption.classList.add('active');
        }
    }

    // Icon picker functionality
    const iconOptions = document.querySelectorAll('.icon-option');
    const iconInput = document.getElementById('iconInput');
    const selectedIconPreview = document.getElementById('selectedIconPreview');

    iconOptions.forEach(option => {
        option.addEventListener('click', function () {
            const icon = this.getAttribute('data-icon');
            iconInput.value = icon;
            selectedIconPreview.innerHTML = `<i class="${icon}" style="font-size: 1.5rem;"></i>`;

            iconOptions.forEach(opt => opt.classList.remove('selected'));
            this.classList.add('selected');
        });
    });

    // Add ripple effect to buttons
    document.querySelectorAll('.btn').forEach(button => {
        button.addEventListener('click', function (e) {
            let x = e.pageX - this.offsetLeft;
            let y = e.pageY - this.offsetTop;

            let ripple = document.createElement('span');
            ripple.classList.add('ripple');
            ripple.style.left = `${x}px`;
            ripple.style.top = `${y}px`;

            this.appendChild(ripple);

            setTimeout(() => {
                ripple.remove();
            }, 1000);
        });
    });
});