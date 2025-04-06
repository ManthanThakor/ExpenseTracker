document.addEventListener('DOMContentLoaded', function () {
    // Slider functionality
    const pagination = document.getElementById('pagination');
    const buttons = pagination.querySelectorAll('button');
    const sliderImages = document.querySelectorAll('#slider > img');
    const slideTitle = document.getElementById('slide-title');
    const slideStatus = document.getElementById('slide-status');
    let currentSlide = 0;
    let isAnimating = false;

    // Function to change slide
    function changeSlide(slideId) {
        if (isAnimating || currentSlide === slideId) return;

        isAnimating = true;

        // Update pagination
        buttons[currentSlide].classList.remove('active');
        buttons[slideId].classList.add('active');

        // Fade out current slide
        sliderImages[currentSlide].style.opacity = 0;

        // Fade in next slide
        setTimeout(() => {
            sliderImages[slideId].style.opacity = 1;

            // Update slide content with animation
            updateSlideContent(slideId);

            currentSlide = slideId;
            isAnimating = false;
        }, 500);
    }

    // Function to update slide content
    function updateSlideContent(slideId) {
        // Fade out current content
        slideTitle.style.opacity = 0;
        slideStatus.style.opacity = 0;

        setTimeout(() => {
            // Update content
            const nextSlideTitle = document.querySelector(`[data-slide-title="${slideId}"]`).innerHTML;
            const nextSlideStatus = document.querySelector(`[data-slide-status="${slideId}"]`).innerHTML;

            slideTitle.innerHTML = nextSlideTitle;
            slideStatus.innerHTML = nextSlideStatus;

            // Fade in new content
            slideTitle.style.opacity = 1;
            slideStatus.style.opacity = 1;
        }, 300);
    }

    // Set up event listeners for pagination buttons
    buttons.forEach((button) => {
        button.addEventListener('click', function () {
            const slideId = parseInt(this.dataset.slide, 10);
            changeSlide(slideId);
        });
    });

    // Auto-rotate slides every 5 seconds
    let slideInterval = setInterval(() => {
        let nextSlide = (currentSlide + 1) % sliderImages.length;
        changeSlide(nextSlide);
    }, 5000);

    // Pause auto-rotation when hovering over slider
    const slider = document.getElementById('slider');
    slider.addEventListener('mouseenter', () => {
        clearInterval(slideInterval);
    });

    slider.addEventListener('mouseleave', () => {
        slideInterval = setInterval(() => {
            let nextSlide = (currentSlide + 1) % sliderImages.length;
            changeSlide(nextSlide);
        }, 5000);
    });

    // Add transition styling to slider content
    slideTitle.style.transition = 'opacity 0.3s ease';
    slideStatus.style.transition = 'opacity 0.3s ease';
});