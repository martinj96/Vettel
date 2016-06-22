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
    var date = $("#calendar").calendar('get date');
    if (date instanceof Date)
    document.getElementsByName("Date")[0].value = date.toDateString();
    document.getElementById("submitSearch").click();
});

$('#calendar').calendar({
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
});
