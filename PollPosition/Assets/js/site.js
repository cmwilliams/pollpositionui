document.addEventListener('DOMContentLoaded', function () {
    setupAutoComplete();
}, false);

function setupAutoComplete() {

    //Get Form Input
    var input = document.getElementById('place-address');

    //Bind to Input
    var autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.setFields(['address_components', 'geometry', 'icon', 'name']);

    //Handle Address
    autocomplete.addListener('place_changed', function () {

        var place = autocomplete.getPlace();
        if (!place.geometry) {
            window.alert("No details available for input: '" + place.name + "'");
            return;
        }

        var address = '';
        if (place.address_components) {
            address = [
                (place.address_components[0] && place.address_components[0].short_name || ''),
                (place.address_components[1] && place.address_components[1].short_name || ''),
                (place.address_components[2] && place.address_components[2].short_name || '')
            ].join(' ');
        }
        window.location.href = "/Search?Address=" + address;
    });
}
