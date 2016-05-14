(function () {
    'use strict';

    var app = angular.module('app',
    [
        // Angular modules
        'ngRoute'

        // Custom modules

        // 3rd Party Modules
    ]);
    //.config(["$routeProvider", function ($routeProvider) {
    //    $routeProvider.when("/",
    //    {
    //        templateUrl: "",
    //        controller: ""
    //    })
    //}])
    app.run(["$rootScope", "$injector", function ($rootScope, $injector) {
        $injector.get("$http").defaults.transformRequest = function (data, headersGetter) {
            if ($rootScope.oauth)
                headersGetter()['Authorization'] = "Bearer " + $rootScope.oauth.access_token;
            if (data) {
                return angular.toJson(data);
            }
        };
    }]);
})();