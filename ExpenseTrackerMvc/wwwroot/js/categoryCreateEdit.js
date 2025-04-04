$(document).ready(function () {
    // Color picker functionality
    $('.color-option').click(function () {
        const color = $(this).data('color');
        $('#colorInput').val(color);
        $('#colorPreview').css('background-color', color);
        $('.color-option').removeClass('selected');
        $(this).addClass('selected');
    });

    $('#colorInput').on('input', function () {
        const color = $(this).val();
        $('#colorPreview').css('background-color', color);
    });

    // Icon picker functionality
    $('.icon-option').click(function () {
        const icon = $(this).data('icon');
        $('#iconInput').val(icon);
        $('#selectedIconPreview').html(`<i class="${icon}" style="font-size: 1.5rem;"></i>`);
        $('.icon-option').removeClass('selected');
        $(this).addClass('selected');
    });

    // Initialize selected state
    const currentColor = $('#colorInput').val();
    $(`.color-option[data-color="${currentColor}"]`).addClass('selected');

    const currentIcon = $('#iconInput').val();
    if (currentIcon) {
        $(`.icon-option[data-icon="${currentIcon}"]`).addClass('selected');
    }
});