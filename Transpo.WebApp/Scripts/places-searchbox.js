function initMapsSearch() {
    var options = {
        types: ['(cities)']
    }

    var fromInput = document.getElementById('searchbox-from');
    var fromSearchBox = new google.maps.places.Autocomplete(fromInput, options);

    var toInput = document.getElementById('searchbox-to');
    var toSearchbox = new google.maps.places.Autocomplete(toInput, options);

    fromSearchBox.addListener('place_changed', function () {
        var place = fromSearchBox.getPlace();
        if (!place.geometry) {
            return;
        }
        setPlaceFrom(place);
    });

    toSearchbox.addListener('place_changed', function () {
        var place = toSearchbox.getPlace();
        if (!place.geometry) {
            return;
        }
        setPlaceTo(place);
    });
}