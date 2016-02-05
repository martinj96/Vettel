var directionsService = new google.maps.DirectionsService;
var directionsDisplay = new google.maps.DirectionsRenderer;
var waypointsCounter = 0;

function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 2,
        center: { lat: 41.85, lng: -87.65 }
    });

    directionsDisplay.setMap(map);
    initMapsSearch(directionsService, directionsDisplay);

    $('body').on('place_changed', '.waypoint', function () {
        calculateAndDisplayRoute(directionsService, directionsDisplay);
    })
}

function calculateAndDisplayRoute(directionsService, directionsDisplay) {
    var waypts = [];
    var inputArray = document.getElementsByClassName('waypoint');
    for (var i = 0; i < inputArray.length; i++) {
        if (inputArray[i] && inputArray[i].value.length > 0) {
            waypts.push({
                location: inputArray[i].value,
                stopover: true
            });
        }
    }

    var origin = document.getElementById('searchbox-from').value,
        destination =  document.getElementById('searchbox-to').value;

    if (origin.length === 0 || destination.length === 0)
        return;

    directionsService.route({
        origin: origin,
        destination: destination,
        waypoints: waypts,
        optimizeWaypoints: true,
        travelMode: google.maps.TravelMode.DRIVING
    }, function (response, status) {
        if (status === google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
            //var route = response.routes[0];
            //var summaryPanel = document.getElementById('directions-panel');
            //summaryPanel.innerHTML = '';
            //// For each route, display summary information.
            //for (var i = 0; i < route.legs.length; i++) {
            //    var routeSegment = i + 1;
            //    summaryPanel.innerHTML += '<b>Route Segment: ' + routeSegment +
            //        '</b><br>';
            //    summaryPanel.innerHTML += route.legs[i].start_address + ' to ';
            //    summaryPanel.innerHTML += route.legs[i].end_address + '<br>';
            //    summaryPanel.innerHTML += route.legs[i].distance.text + '<br><br>';
            //}
        } else {
            window.alert('Directions request failed due to ' + status);
        }
    });
}

function initMapsSearch(directionsService, directionsDisplay) {
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
        document.getElementsByName("StartPoint.Longitude")[0].value = place.geometry.location.lng();
        document.getElementsByName("StartPoint.Latitude")[0].value = place.geometry.location.lat();
        document.getElementsByName("StartPoint.Name")[0].value = $(place.adr_address).text();
        calculateAndDisplayRoute(directionsService, directionsDisplay);
    });

    toSearchbox.addListener('place_changed', function () {
        var place = toSearchbox.getPlace();
        if (!place.geometry) {
            return;
        }
        document.getElementsByName("EndPoint.Longitude")[0].value = place.geometry.location.lng();
        document.getElementsByName("EndPoint.Latitude")[0].value = place.geometry.location.lat();
        document.getElementsByName("EndPoint.Name")[0].value = $(place.adr_address).text();
        calculateAndDisplayRoute(directionsService, directionsDisplay);
    });
}

function addSearchboxToElement(input) {
    var options = {
        types: ['(cities)']
    }

    var searchbox = new google.maps.places.Autocomplete(input, options);

    searchbox.addListener('place_changed', function () {
        var place = searchbox.getPlace();
        if (!place.geometry) {
            return;
        }
        document.getElementsByName("Waypoints[" + (waypointsCounter - 1) + "].Longitude")[0].value = place.geometry.location.lng();
        document.getElementsByName("Waypoints[" + (waypointsCounter - 1) + "].Latitude")[0].value = place.geometry.location.lat();
        document.getElementsByName("Waypoints[" + (waypointsCounter - 1) + "].Name")[0].value = $(place.adr_address).text();
        calculateAndDisplayRoute(directionsService, directionsDisplay);
    });
    waypointsCounter++;

}