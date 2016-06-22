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
                    '<input type="hidden" name="Waypoints[' + waypointsCounter + '].Name" />' +
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
        $("#returnRide").attr("value", "false")
    } else {
        $('#date-destination').attr('required', 'required');
        $('#hour-destination').attr('required', 'required');
        $('#minute-destination').attr('required', 'required');
        $("#returnRide").attr("value", "true")
    }
    $('.return-date-header').fadeToggle();
    $('.return-date').fadeToggle();
});

addSearchboxToElement($('.waypoint')[0]);

$('#btnSubmitRide').on('click', function () {
    document.getElementsByName("DepartureDate")[0].value = $('#departureCalendar').calendar('get date').toDateString();
    var returnDate = $('#returnCalendar').calendar('get date');
    if (returnDate instanceof Date)
        document.getElementsByName("ReturnDepartureDate")[0].value = returnDate.toDateString();
    $('#submitCreateRide').submit();
})

function UpdateDistance(distance) {
    var PRICE_FACTOR = 7.38;
    document.getElementsByName('Length')[0].value = distance / 1000;
    var seats = document.getElementsByName('SeatsLeft')[0].value;
    document.getElementsByName('PricePerPassenger')[0].value = Math.ceil(distance * PRICE_FACTOR / 1000 / seats / 10)*10;
}
var calendar_options = {
    type: 'date',
    text: {
        days: ["Н", "П", "В", "С", "Ч", "П", "С"],
        months: ["јануари", "февруари", "март", "април", "мај", "јуни", "јули", "август", "септември", "октомври", "ноември", "декември"],
        monthsShort: ["јан", "фев", "март", "април", "мај", "јуни", "јули", "авг", "септ", "окт", "ноем", "дек"],
        today: "Денес",
        now: "Сега",
        am: "AM",
        pm: "PM"
    },
    minDate: new Date()
}

$('#departureCalendar').calendar(calendar_options);
$('#returnCalendar').calendar(calendar_options);

