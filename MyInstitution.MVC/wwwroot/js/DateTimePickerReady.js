
$(function () {
    $.datetimepicker.setLocale('de');
    $.datetimepicker.setDateFormatter('moment');
    $(".datetimefield").datetimepicker({
        format: 'DD.MM.YYYY HH:mm',
        formatTime: 'HH:mm'
    });
});

