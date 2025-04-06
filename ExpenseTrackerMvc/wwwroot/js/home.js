document.addEventListener('DOMContentLoaded', function () {
    // Slider functionality
    const pagination = document.getElementById('pagination');
    const buttons = pagination.querySelectorAll('button');
    const sliderImages = document.querySelectorAll('.slider-image');
    const slideTitle = document.getElementById('slide-title');
    const slideStatus = document.getElementById('slide-status');
    const sliderContent = document.getElementById('slider-content');
    let currentSlide = 0;
    let isAnimating = false;

    // Initialize slider
    function initSlider() {
        // Set first slide as active
        sliderImages[0].classList.add('active');

        // Setup initial animations for content
        slideTitle.style.opacity = 0;
        slideStatus.style.opacity = 0;

        // Add transition styles
        slideTitle.style.transition = 'opacity 0.8s ease, transform 0.8s cubic-bezier(0.215, 0.61, 0.355, 1)';
        slideStatus.style.transition = 'opacity 0.8s ease, transform 0.8s cubic-bezier(0.215, 0.61, 0.355, 1)';

        // Add transition to images
        sliderImages.forEach(image => {
            image.style.transition = 'opacity 1s ease, transform 1.2s ease';
        });

        // Initial entrance animation
        setTimeout(() => {
            slideTitle.style.opacity = 1;
            slideTitle.style.transform = 'translateY(0)';
            slideStatus.style.opacity = 1;
            slideStatus.style.transform = 'translateX(0)';
        }, 300);

        // Add button hover effects
        initButtonEffects();
    }

    // Function to add button hover effects
    function initButtonEffects() {
        buttons.forEach((button) => {
            button.addEventListener('mouseenter', function () {
                if (!this.classList.contains('active')) {
                    this.style.transform = 'scale(1.3)';
                }
            });

            button.addEventListener('mouseleave', function () {
                if (!this.classList.contains('active')) {
                    this.style.transform = 'scale(1)';
                }
            });
        });
    }

    // Function to change slide with improved animations
    function changeSlide(slideId) {
        if (isAnimating || currentSlide === slideId) return;
        isAnimating = true;

        // Update pagination
        buttons[currentSlide].classList.remove('active');
        buttons[slideId].classList.add('active');

        // Start exit animation for content with slight delay between elements
        slideTitle.style.opacity = 0;
        slideTitle.style.transform = 'translateY(20px)';

        setTimeout(() => {
            slideStatus.style.opacity = 0;
            slideStatus.style.transform = 'translateX(-20px)';
        }, 100);

        // Image transition with slightly different timing
        setTimeout(() => {
            sliderImages[currentSlide].classList.remove('active');

            setTimeout(() => {
                sliderImages[slideId].classList.add('active');

                // Update content
                updateSlideContent(slideId);

                // Complete transition with staggered timing
                setTimeout(() => {
                    slideTitle.style.opacity = 1;
                    slideTitle.style.transform = 'translateY(0)';

                    setTimeout(() => {
                        slideStatus.style.opacity = 1;
                        slideStatus.style.transform = 'translateX(0)';

                        currentSlide = slideId;
                        isAnimating = false;
                    }, 200);
                }, 300);
            }, 300);
        }, 400);
    }

    // Function to update slide content
    function updateSlideContent(slideId) {
        const nextSlideTitle = document.querySelector(`[data-slide-title="${slideId}"]`).innerHTML;
        const nextSlideStatus = document.querySelector(`[data-slide-status="${slideId}"]`).innerHTML;

        slideTitle.innerHTML = nextSlideTitle;
        slideStatus.innerHTML = nextSlideStatus;
    }

    // Set up event listeners for pagination buttons
    buttons.forEach((button) => {
        button.addEventListener('click', function () {
            const slideId = parseInt(this.dataset.slide, 10);
            changeSlide(slideId);
        });
    });

    // Auto-rotate slides with slightly longer interval
    let slideInterval = setInterval(() => {
        let nextSlide = (currentSlide + 1) % sliderImages.length;
        changeSlide(nextSlide);
    }, 7000);

    // Pause auto-rotation when hovering over slider
    const slider = document.getElementById('slider');
    slider.addEventListener('mouseenter', () => {
        clearInterval(slideInterval);
    });

    slider.addEventListener('mouseleave', () => {
        slideInterval = setInterval(() => {
            let nextSlide = (currentSlide + 1) % sliderImages.length;
            changeSlide(nextSlide);
        }, 7000);
    });

    // Add keyboard navigation
    document.addEventListener('keydown', (e) => {
        if (e.key === 'ArrowRight') {
            let nextSlide = (currentSlide + 1) % sliderImages.length;
            changeSlide(nextSlide);
        } else if (e.key === 'ArrowLeft') {
            let prevSlide = (currentSlide - 1 + sliderImages.length) % sliderImages.length;
            changeSlide(prevSlide);
        }
    });

    // Initialize slider
    initSlider();
});


//Finance How-It-Works Section Animation

document.querySelectorAll('.magnetic-wrap').forEach(button => {
    button.addEventListener('mousemove', (e) => {
        const rect = button.getBoundingClientRect();
        const x = e.clientX - rect.left;
        const y = e.clientY - rect.top;

        const centerX = rect.width / 2;
        const centerY = rect.height / 2;

        const distanceX = x - centerX;
        const distanceY = y - centerY;

        const btn = button.querySelector('.btn-magnetic');
        btn.style.transform = `translate(${distanceX * 0.2}px, ${distanceY * 0.2}px)`;
    });

    button.addEventListener('mouseleave', () => {
        const btn = button.querySelector('.btn-magnetic');
        btn.style.transform = 'translate(0, 0)';
    });
});


//Testimonials Section

// Initialize Slick Slider with custom settings
$(document).ready(function () {
    $('.testimonials-carousel').slick({
        centerMode: true,
        centerPadding: '0',
        slidesToShow: 3,
        arrows: true,
        dots: true,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    slidesToShow: 2
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 1,
                    centerMode: false
                }
            }
        ]
    });
});