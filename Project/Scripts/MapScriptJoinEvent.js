function initialize() {

    // center the map in Ruppin
    var ruppinPos = new Object();
    var a = document.getElementById("MainContent_latHF");
    var latH = a.value;
    var b = document.getElementById("MainContent_lngHF");
    var lngH = b.value;

    ruppinPos.lat = latH;
    ruppinPos.long = lngH;
    var myLatlng = new google.maps.LatLng(ruppinPos.lat, ruppinPos.long);
    var mapOptions = {
        zoom: 17,
        center: myLatlng,
        mapTypeId: google.maps.MapTypeId.Map
        
    }
    map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

    var marker1 = new google.maps.Marker({
        position: myLatlng,
        map: map,
        title: 'Ruppin'
    });

}

google.maps.event.addDomListener(window, 'load', initialize);




function findme(pathList2) {
    pos = new google.maps.LatLng(pathList2[0].Lat, pathList2[0].Lng);
    map.setCenter(pos);
}
    