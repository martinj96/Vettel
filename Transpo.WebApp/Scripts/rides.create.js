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
    if ($("#returnRide").attr("value") == "true") {
        $("#returnRide").attr("value", "false")
    } else {
        $("#returnRide").attr("value", "true")
    }
    $('.return-date-header').fadeToggle();
    $('.return-date').fadeToggle();
});

addSearchboxToElement($('.waypoint')[0]);

//$('#btnSubmitRide').on('click', function () {
//    document.getElementsByName("DepartureDate")[0].value = $('#departureCalendar').calendar('get date').toDateString();
//    var returnDate = $('#returnCalendar').calendar('get date');
//    if (returnDate instanceof Date)
//        document.getElementsByName("ReturnDepartureDate")[0].value = returnDate.toDateString();
//    $('#submitCreateRide').submit();
//})

function UpdateDistance(distanceLegs) {
    var distance = distanceLegs.distance.value;
    var PRICE_FACTOR = 7.38;
    //document.getElementsByName('Length')[0].value = distance / 1000;
    var distance_val_km = distanceLegs.distance.text.split(" ")[0];
    var distance_text = distanceLegs.distance.text.split(" ")[1];
    var seats = document.getElementsByName('SeatsLeft')[0].value;
    var duration_val = distanceLegs.duration.text.split(" ")[0];
    var duration_text = distanceLegs.duration.text.split(" ")[1];
    $('#duration-text').text(duration_text);
    $('#duration-val').text(duration_val);
    $('#distance-text').text(distance_text);
    $('#distance-val').text(distance_val_km);
    $('#route-info').text('Информации за патувањето');
    document.getElementsByName('PricePerPassenger')[0].value = Math.ceil(distance * PRICE_FACTOR / 1000 / seats / 10) * 10;
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

$.fn.form.settings.rules.HasOrigin = function (value) {
    var lat = $('[name="StartPoint.Latitude"').val().length > 0;
    var lon = $('[name="StartPoint.Longitude"').val().length > 0;
    var name = $('[name="StartPoint.Name"').val().length > 0;
    return lat && lon && name;
}

$.fn.form.settings.rules.HasDestination = function (value) {
    var lat = $('[name="EndPoint.Latitude"').val().length > 0;
    var lon = $('[name="EndPoint.Longitude"').val().length > 0;
    var name = $('[name="EndPoint.Name"').val().length > 0;
    return lat && lon && name;
}

$.fn.form.settings.rules.ValidateDepartureDate = function (value) {
    var date = $('#departureCalendar').calendar('get date');
    if (!(date instanceof Date))
        return false;
    document.getElementsByName("DepartureDate")[0].value = date.toDateString();
    return $('[name="DepartureDate"]').val().length > 0;
}

$.fn.form.settings.rules.ReturnDepartureDateValidator = function (value) {
    if ($('#returnRide').val() == "false")
        return true;

    var returnDate = $('#returnCalendar').calendar('get date');
    if (returnDate instanceof Date) {
        document.getElementsByName("ReturnDepartureDate")[0].value = returnDate.toDateString();
    }
    return $('[name="ReturnDepartureDate"]').val().length > 0;
}

$.fn.form.settings.rules.ReturnDepartureHourValidator = function (value) {
    if ($('#returnRide').val() == "false")
        return true;
    return $('[name="ReturnTimeDeparture.Hour"]').val().length > 0;
}

$.fn.form.settings.rules.ReturnDepartureTimeValidator = function (value) {
    if ($('#returnRide').val() == "false")
        return true;
    return $('[name="ReturnTimeDeparture.Minutes"]').val().length > 0;
}

$.fn.form.settings.rules.ReturnValidator = function (value) {
    if ($('#returnRide').val() == "false")
        return true;
    var departureDate = $('#departureCalendar').calendar('get date');
    var departureHour = $('[name="TimeDeparture.hour"]').val();
    var departureMinutes = $('[name="TimeDeparture.minutes"]').val();
    var date = $('#returnCalendar').calendar('get date');
    var hour = $('[name="ReturnTimeDeparture.Hour"]').val();
    var minutes = $('[name="ReturnTimeDeparture.Minutes"]').val();
    if ((departureDate instanceof Date) 
        && (date instanceof Date) 
        && hour.length > 0 
        && minutes.length > 0
        && departureHour.length > 0
        && departureMinutes.length > 0) {
        departureDate.setHours(departureHour);
        departureDate.setMinutes(departureMinutes);
        date.setHours(hour);
        date.setMinutes(minutes);
        if (date > departureDate)
            return true;
        else
            return false;
    } else {
        return true;
    }
}


$('.ui.form.createrideform').form({
    fields: {
        origin: {
            identifier: 'Origin',
            rules: [
                {
                    type: 'HasOrigin',
                    prompt: 'Внесете место на поаѓање'
                }
            ]
        },
        destination: {
            identifier: 'Destination',
            rules: [
                {
                    type: 'HasDestination',
                    prompt: 'Внесете дестинација'
                }
            ]
        },
        departureDate: {
            identifier: 'DepartureDate',
            rules: [
            {
                type: 'ValidateDepartureDate',
                prompt: 'Внесете датум на поаѓање'
            }
            ]
        },
        departureHours: {
            identifier: 'TimeDeparture.hour',
            rules: [
                {
                    type: 'empty',
                    prompt: 'Внесете време на поаѓање'
                }
            ]
        },
        departureTime: {
            identifier: 'TimeDeparture.minutes',
            rules: [
                {
                    type: 'empty',
                    prompt: 'Внесете време на поаѓање'
                }
            ]
        },
        returnDepartureDate: {
            identifier: 'ReturnDepartureDate',
            rules: [
                {
                    type: 'ReturnDepartureDateValidator',
                    prompt: 'Внесете датум на враќање'
                },
                {
                    type: 'ReturnValidator',
                    prompt: 'Враќањето мора да биде по поаѓањето'
                }
            ]
        },
        returnDepartureHours: {
            identifier: 'ReturnTimeDeparture.Hour',
            rules: [
                {
                    type: 'ReturnDepartureHourValidator',
                    prompt: 'Внесете време на враќање'
                },
                {
                    type: 'ReturnValidator',
                    prompt: 'Враќањето мора да биде по поаѓањето'
                }
            ]
        },
        returnDepartureTime: {
            identifier: 'ReturnTimeDeparture.Minutes',
            rules: [
                {
                    type: 'ReturnDepartureTimeValidator',
                    prompt: 'Внесете време на враќање'
                },
                {
                    type: 'ReturnValidator',
                    prompt: 'Враќањето мора да биде по поаѓањето'
                }
            ]
        },
        pricePerPassenger: {
            identifier: 'PricePerPassenger',
            rules: [
                {
                    type: 'empty',
                    prompt: 'Внесете цена по патник'
                }
            ]
        },
        seatsLeft: {
            identifier: 'SeatsLeft',
            rules: [
                {
                    type: 'empty',
                    prompt: 'Внесете број на слободни седишта'
                }
            ]
        }

    },
    inline: true,
    onSuccess: function (e, data) {
        $('#btnSubmitRide').addClass('loading');
    }
});