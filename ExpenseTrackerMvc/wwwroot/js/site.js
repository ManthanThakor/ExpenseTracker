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