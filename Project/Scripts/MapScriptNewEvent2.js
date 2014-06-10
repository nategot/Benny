 
        var geocoder;
        var map;
        var marker;
        var directionsDisplay;
        var directionsService = new google.maps.DirectionsService();
        var watchGeoMarkerProcess;
        var routeStatus = 0;
        var routePointA;
        var routePointB;
        var markers = [];
        var FinalPositon;
        var addressTXT;
        var flag = true;
    
  
       

//        window.onload = start();
//        


        function start() {
           
            showMap();
           
           
            document.getElementById("getPosition").addEventListener("click", showTown);
        }

          function initialize() {
    
    var input = document.getElementById('locationTB');
    var autocomplete = new google.maps.places.Autocomplete(input);

    google.maps.event.addListener(autocomplete, 'place_changed', function () {
//        infowindow.close();
        marker.setVisible(false);
        input.className = '';
        var place = autocomplete.getPlace();
        if (!place.geometry) {
            // Inform the user that the place was not found and return.
            input.className = 'notfound';
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

//        infowindow.setContent('<div><strong>' + place.name + '</strong><br>' + address);

    });

//    setupClickListener('changetype-geocode', ['geocode']);
        
        }


        function watchPositionStop() {
            if (navigator.geolocation) {
                navigator.geolocation.clearWatch(watchGeoMarkerProcess);
            }
            else { alert("Geolocation is not supported by this browser."); }
        }

        function watchPosition() {
            var myOptions = {
                zoom: 14,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                mapTypeControl: false,
                navigationControlOptions: { style: google.maps.NavigationControlStyle.SMALL }
            };
            //to create only once! and not inside the changePostion() function!
            map = new google.maps.Map(document.getElementById("mapholder"), myOptions);

            if (navigator.geolocation) {
                watchGeoMarkerProcess = navigator.geolocation.watchPosition(changePostion, showError);
            }
            else { alert("Geolocation is not supported by this browser."); }
        }

        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition, showError);
            }
            else { alert("Geolocation is not supported by this browser."); }
                  

                   
        }

       


        function showLat() {
            var lat = document.getElementById("latRange").value;
            var long = document.getElementById("longRange").value;
            LatValue.innerText = lat;
            ShowPositionLatLong(lat, long);
        }

        function showLong() {
            var lat = document.getElementById("latRange").value;
            var long = document.getElementById("longRange").value;
            LongValue.innerText = long;
            ShowPositionLatLong(lat, long);
        }

         //use in the ripple. need the var map definition inside the function!
        function showPosition(position) {
            lat = position.coords.latitude;
            lon = position.coords.longitude;
            latlon = new google.maps.LatLng(lat, lon);
           
            var myOptions = {
                center: latlon,
                zoom: 14,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                mapTypeControl: false,
                navigationControlOptions: { style: google.maps.NavigationControlStyle.SMALL }
            };
            var map = new google.maps.Map(document.getElementById("mapholder"), myOptions);
            var marker = new google.maps.Marker({ position: latlon, map: map, title: "You are here!" });
          
        }
        function showMap() {
            directionsDisplay = new google.maps.DirectionsRenderer({
                suppressMarkers: true
            });
           
            latlon = new google.maps.LatLng(32.343202656449272, 34.910878837108612);
            var myOptions = {
                center: latlon, zoom: 14,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                mapTypeControl: false,
                navigationControlOptions: { style: google.maps.NavigationControlStyle.SMALL }
            };
         
            map = new google.maps.Map(document.getElementById("mapholder"), myOptions);
            
            google.maps.event.addListener(map, 'click', function (e) {
                placeMarker(e.latLng, map, true);
            });
            
             
            geocoder = new google.maps.Geocoder();
            directionsDisplay.setMap(map);
            initialize();
        }

        //dont use in the ripple
        function ShowPositionLatLong(lat, lon) {

            latlon = new google.maps.LatLng(lat, lon);
            map.setCenter(latlon);
            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
            });
        }

        function placeMarker(position, map, flag) {

            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(null);
            }

            if (routeStatus == 1) {
                iconImage = "a.png";
                routePointA = position;
                routeStatus++;
            }
            else if (routeStatus == 2) {
                iconImage = "b.png";
                routePointB = position;
                routeStatus = 0;
                var request = {
                    origin: routePointA,
                    destination: routePointB,
                    travelMode: google.maps.DirectionsTravelMode.DRIVING
                };

                directionsService.route(request, function (response, status) {
                    if (status == google.maps.DirectionsStatus.OK) {
                        directionsDisplay.setDirections(response);
                    }
                });
            }

            var marker = new google.maps.Marker({
                position: position,
                map: map,
            });

            FinalPositon = position;
           
            markers.push(marker);
            map.panTo(position);

            var address = "???";
            geocoder.geocode({ 'latLng': position }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        address = results[0].formatted_address;
                        if (flag) {
                            FinalPositon = address;             
                        }

                    } else {

                        address = "No address found";
                    }
                    var contentStr =
                    '<div id="markerDiv" style="color:blue;font-family:Aharoni;font-weight:700">' +

                    //  '<div id="latlongMarkerDiv" style="color:red;font-family:david;font-weight:500;width:120px;">' + position + '</div>' +
                        '<div id="addressMarkerDiv" style="color:black;font-family:Arial;font-weight:800;width:120px;">' + address + '</div>' +
                    '</div>'
                      var b = document.getElementById("MainContent_CityHIde");
                      b.value = address;
                    var infowindow = new google.maps.InfoWindow({
                        content: contentStr
                    });

                    google.maps.event.addListener(marker, 'click', function () {
                        infowindow.open(map, marker);
                    });
                } else {
                    alert('Geocoder failed due to: ' + status);
                }
            });
            var a = document.getElementById("MainContent_LatLOngHIde");
            a.value = FinalPositon;
           
        }

        function showTown() {
            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(null);
            }
            var e = document.getElementById("locationTB");
            addressTXT = e.value;

            geocoder.geocode({ 'address': addressTXT }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    map.setCenter(results[0].geometry.location);
                    placeMarker(results[0].geometry.location, map, false);
                } else {
                    alert('insert an Address or click on the map ');
                }
            });
        }

        function startRoute() {
            routeStatus++;
        }
        //only to set the center and marker not to create the map obejct again and again!
        function changePostion(position) {
            if (marker != null) {
                marker.setMap(null);
            }
            var pos = new google.maps.LatLng(position.coords.latitude,
                                                position.coords.longitude);
            map.setCenter(pos);
            marker = new google.maps.Marker({ position: pos, map: map, title: "You are here!" });
        }

        function showError(error) {
            switch (error.code) {
                case error.PERMISSION_DENIED:
                    alert("User denied the request for Geolocation.");
                    break;
                case error.POSITION_UNAVAILABLE:
                    alert("Location information is unavailable.");
                    break;
                case error.TIMEOUT:
                    alert("The request to get user location timed out.");
                    break;
                case error.UNKNOWN_ERROR:
                    alert("An unknown error occurred.");
                    break;
                default:
                    alert("An unknown error occurred.(default)");
                    break;
            }
        }
