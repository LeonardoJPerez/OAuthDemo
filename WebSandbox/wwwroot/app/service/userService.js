(function () {
    'use strict';

    angular
        .module('app')
        .factory('userService', userService);

    userService.$inject = ['$http', "$log"];

    function userService($http, $log) {
        var apiUrl = "http://localhost:50551/";
        var authServerUrl = "https://localhost:44365";

        var service = {
            getData: getData,
            authenticate: authenticate
        };

        return service;

        function authenticate(payload) {
            $log.info("Authenticating User against Auth Server...");
 
            return $http({
                method: 'POST',
                url: authServerUrl + '/connect/token',
                data: payload,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                }
            });
        };

        function getData() {
            $log.info("Getting User information!");
            $http({
                method: 'GET', url: apiUrl + '/api/user/',
                headers: {
                    'Authorization': 'Bearer ' + token
                }
            }).then(function (response) {
                $log.info(response);
                return response;
            });                
        };
    }
})();