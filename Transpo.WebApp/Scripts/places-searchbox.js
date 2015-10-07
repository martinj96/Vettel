// This example adds a search box to a map, using the Google Place Autocomplete
// feature. People can enter geographical searches. The search box will return a
// pick list containing a mix of places and predicted search terms.

function initMapsSearch() {
    // Create the search box and link it to the UI element.
    var options = {
        types: ['(cities)']
    }

    var fromInput = document.getElementById('searchbox-from');
  var fromSearchBox = new google.maps.places.SearchBox(fromInput, options);

  var toInput = document.getElementById('searchbox-to');
  var toSearchbox = new google.maps.places.SearchBox(toInput, options);
}