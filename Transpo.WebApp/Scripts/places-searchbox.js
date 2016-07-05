function initMapsSearch() {
    var options = {
        types: ['(cities)']
    }

    var fromInput = document.getElementById('searchbox-from');
    var fromSearchBox = new google.maps.places.Autocomplete(fromInput, options);

    var toInput = document.getElementById('searchbox-to');
    var toSearchbox = new google.maps.places.Autocomplete(toInput, options);
    
    var resubmitListener;

    var resubmitFormOnEnter = function () {
        google.maps.event.removeListener(resubmitListener);
        $('#btnSubmitSearch').click();
    }

    $(toInput).keydown(function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            resubmitListener = toSearchbox.addListener('place_changed', resubmitFormOnEnter);
            return false;
        }
    });

    fromSearchBox.addListener('place_changed', function () {
        var place = fromSearchBox.getPlace();
        if (place === undefined || !place.geometry) {
            return;
        }
        setPlaceFrom(place);
    });

    toSearchbox.addListener('place_changed', function (e) {
        var place = toSearchbox.getPlace();
        if (place === undefined || !place.geometry) {
            return;
        }
        setPlaceTo(place);
    });
}

function SearchSuccess() {
    $('#btnSubmitSearch').removeClass('loading');
}