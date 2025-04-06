$(document).ready(function () {
    $('.welcome-slider').slick({
        dots: false,
        arrows: false,
        infinite: true,
        speed: 800,
        fade: false,
        cssEase: 'cubic-bezier(0.645, 0.045, 0.355, 1)',
        autoplay: true,
        autoplaySpeed: 5000,
        pauseOnHover: true,
        pauseOnFocus: false
    });

    $('.welcome-slider').on('beforeChange', function (event, slick, currentSlide, nextSlide) {
        $('.slick-active .slide-content').css({
            'opacity': 0,
            'transform': 'translateY(20px)'
        });
    });

    $('.welcome-slider').on('afterChange', function (event, slick, currentSlide) {
        $('.slick-active .slide-content').css({
            'opacity': 1,
            'transform': 'translateY(0)'
        });
    });
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