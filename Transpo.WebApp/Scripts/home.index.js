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
    document.getElementsByName("FromCountryShortCode")[0].value = from_place.address_components[2].short_name;
}

function setPlaceTo(place) {
    to_place = place;
    document.getElementsByName("ToLongitude")[0].value = to_place.geometry.location.lng();
    document.getElementsByName("ToLatitude")[0].value = to_place.geometry.location.lat();
    document.getElementsByName("ToCityName")[0].value = to_place.name;
    document.getElementsByName("ToCountryShortCode")[0].value = to_place.address_components[2].short_name;
}

document.getElementById("btnSubmitSearch").addEventListener("click", function () {
    var from = getPlaceFrom(),
        to = getPlaceTo();

    if (from === undefined || to === undefined) {
        showWarningMessage("");
        return false;
    }

    document.getElementById("submitSearch").click();

});

function showWarningMessage(msg) {}

function introduction() {
    $header = $('.main-header');
    var background = 'bg' + getRandomInt(1, 14);
    $header
        .addClass(background)
        .removeClass('zoomed');
}

$(function () {
    introduction();
});
