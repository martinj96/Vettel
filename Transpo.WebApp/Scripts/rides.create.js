$('.menu .item.create').addClass('active');
initMap();

$('#btnAddMoreWaypoints').on('click', function () {
    $('.ui.form.waypoints')
        .find('.field')
        .last()
        .after(
            '<div class="field">' +
                '<div class="ui icon input">' +
                    '<input type="text" class="waypoint" placeholder="Add a Waypoint">' +
                    '<input type="hidden" name="Waypoints[' + waypointsCounter + '].Latitude" />' +
                    '<input type="hidden" name="Waypoints[' + waypointsCounter + '].Longitude" />' +
                '</div>' +
            '</div>'
        )
    ;
    addSearchboxToElement($('.waypoint').last()[0]);

});

$('.checkbox').checkbox();

for (var i = 0; i < 24; i++) {
    var value = i;
    if (value < 10)
        value = "0" + value;
    $('.dropdown.hour')
        .append(
            '<option value="' + value + '">' + value + '</div>'
        )
    ;
}

for (var i = 0; i < 46; i+=15) {
    var value = i;
    if (value < 10)
        value = "0" + value;
    $('.dropdown.minutes')
        .append(
            '<option value="' + value + '">' + value + '</div>'
        )
    ;
}

$('#return').on('change', function () {
    if ($('#date-destination').attr('required')) {
        $('#date-destination').removeAttr('required');
        $('#hour-destination').removeAttr('required');
        $('#minute-destination').removeAttr('required');
    } else {
        $('#date-destination').attr('required', requred);
        $('#hour-destination').attr('required', requred);
        $('#minute-destination').attr('required', requred);
    }
    $('.return-date-header').fadeToggle();
    $('.return-date').fadeToggle();
});

addSearchboxToElement($('.waypoint')[0]);

$('#btnSubmitRide').on('click', function () {
    $('#submitCreateRide').submit();
})

