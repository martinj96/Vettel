var from_place,
    to_place;

function getPlaceFrom() {
    return from_place;
}

function getPlaceTo() {
    return to_place;
}

function setPlaceFrom(place) {
    from_place = place;
    document.getElementsByName("FromLongitude")[0].value = from_place.geometry.location.lng();
    document.getElementsByName("FromLatitude")[0].value = from_place.geometry.location.lat();
    document.getElementsByName("FromCityName")[0].value = from_place.name;
    //document.getElementsByName("FromCountryShortCode")[0].value = from_place.address_components[2].short_name;
}

function setPlaceTo(place) {
    to_place = place;
    document.getElementsByName("ToLongitude")[0].value = to_place.geometry.location.lng();
    document.getElementsByName("ToLatitude")[0].value = to_place.geometry.location.lat();
    document.getElementsByName("ToCityName")[0].value = to_place.name;
    //document.getElementsByName("ToCountryShortCode")[0].value = to_place.address_components[2].short_name;
}

//document.getElementById("btnSubmitSearch").addEventListener("click", function () {
//    var from = getPlaceFrom(),
//        to = getPlaceTo();

//    if (from === undefined || to === undefined) {
//        showWarningMessage("");
//        return false;
//    }

//    document.getElementById("submitSearch").click();
//});

function showWarningMessage(msg) {}

function introduction() {
    $header = $('.main-header');
    //var background = 'bg' + getRandomInt(1, 14);
    var background = 'bgmain';
    $header
        //.addClass(background)
        .removeClass('zoomed');

    $('#indexbg').removeClass('bgUnloaded').addClass('bgmain');
}

$(function () {
    introduction();
});

$.fn.form.settings.rules.HasOrigin = function (value) {
    var lat = $('[name="FromLatitude"').val().length > 0;
    var lon = $('[name="FromLongitude"').val().length > 0;
    var name = $('[name="FromCityName"').val().length > 0;
    return lat && lon && name;
}

$.fn.form.settings.rules.HasDestination = function (value) {
    var lat = $('[name="ToLatitude"').val().length > 0;
    var lon = $('[name="ToLongitude"').val().length > 0;
    var name = $('[name="ToCityName"').val().length > 0;
    return lat && lon && name;
}


$('.ui.form.findrideform').form({
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
        }
    },
    inline: true,
    onSuccess: function (e, data) {
        $('#btnSubmitSearch').addClass('loading');
    }
});