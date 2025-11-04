$(document).ready(function () {
    // Mobile sidebar toggle
    $('#mobileToggle').click(function () {
        $('#sidebar').toggleClass('mobile-open');
    });

    // Set active menu item
    var currentPath = window.location.pathname;
    $('.sidebar .nav-link').each(function () {
        if ($(this).attr('href') === currentPath) {
            $(this).addClass('active');
        }
    });

    // Close sidebar when clicking outside on mobile
    $(document).click(function (e) {
        if ($(window).width() <= 576) {
            if (!$(e.target).closest('#sidebar').length &&
                !$(e.target).closest('#mobileToggle').length &&
                $('#sidebar').hasClass('mobile-open')) {
                $('#sidebar').removeClass('mobile-open');
            }
        }
    });

    // Menu toggle functionality
    $('.menu-item > .nav-link').click(function (e) {
        e.preventDefault();
        var submenu = $(this).siblings('.submenu');
        var toggleIcon = $(this).find('.menu-toggle');

        // Close other open submenus
        $('.submenu').not(submenu).removeClass('show');
        $('.menu-toggle').not(toggleIcon).removeClass('rotated');

        // Toggle current submenu
        submenu.toggleClass('show');
        toggleIcon.toggleClass('rotated');
    });
});