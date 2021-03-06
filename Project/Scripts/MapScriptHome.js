﻿var url = 'http://proj.ruppin.ac.il/bgroup14/prod/tar6/WebService.asmx/';
        
function initialize() {
    var poiList;
    // center the map in Ruppin
    var ruppinPos = new Object();
    ruppinPos.lat = 32.343193;
    ruppinPos.long = 34.911908;
    myLatlng = new google.maps.LatLng(ruppinPos.lat, ruppinPos.long);
    var mapOptions = {
        zoom: 6,
        center: myLatlng,
        mapTypeId: google.maps.MapTypeId.Map
    }
    map = new google.maps.Map(document.getElementById('mapholder'), mapOptions);

    var marker1 = new google.maps.Marker({
        position: myLatlng,
        map: map,
        title: 'Ruppin'
    });
    getPOIList();

}

google.maps.event.addDomListener(window, 'load', initialize);

//-----------------------------------------------------------------------
// get the getPOIList 
//-----------------------------------------------------------------------
function getPOIList() {
    var dataString = '{ss:"sssd"}'; ;
    $.ajax({ // ajax call starts
        url: url+'getEvents',   // server side method
        // parameters passed to the server
        type: 'POST',
        dataType: 'json', // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8',
        success: function (data) // Variable data contains the data we get from server side
        {
            poiList = $.parseJSON(data.d);

            // run on all the POIs and display them
            for (i = 0; i < poiList.length; i++) {
                showPOI(poiList[i]);
            }
        }, // end of success
        error: function (e) {
            alert("failed in getTarget :( " + e.responseText);
        } // end of error
    }) // end of ajax call
}

//--------------------------------------
// show the POI on the map
//--------------------------------------
function showPOI(poiPoint) {

    var poiLatlng = new google.maps.LatLng(poiPoint.Point.Lat, poiPoint.Point.Lng);
    image = poiPoint.ImageUrl;
    var marker = new google.maps.Marker({
        position: poiLatlng,
        map: map,
        title: poiPoint.Name,
        icon: image

    });

    var contentString = '<div id="content" > <img src ="' + poiPoint.ImageUrl + '" style="width: 80px"/></br><h1>' + poiPoint.Description + '</h1><div id="bodyContent" style="color:Black">'
              + '<p>Age Range: ' + poiPoint.MaxAge + '-' + poiPoint.MinAge + '</p>' + '<p>Address: ' + poiPoint.Address + '</p>' + '<p>Date & Time: ' + poiPoint.DateTimeStr + '</p>' + '</div><input type="button" class="myButton" onclick="JoinEvent(' + poiPoint.EventNum + ',' + poiPoint.Point.Lat + ',' + poiPoint.Point.Lng + ')" id="btnJoinMap" value="Join"/><p>' + '</div>';

    var infowindow1 = new google.maps.InfoWindow({
        content: contentString
    });

    google.maps.event.addListener(marker, 'click', function () {
        infowindow1.open(map, marker);
    });

}


function CloseInfo()
{ infowindow2.close(map); }





