(function () {
    'use strict';

    angular
        .module('app')
        .controller('userController', userController);

    userController.$inject = ['$location', '$window', '$log', "userService"];

    function userController($location, $window, $log, userService) {
        /* jshint validthis:true */
        var vm = this;
        vm.token = '';      

        vm.authenticate = function () {
            vm.message = '';
            userService.authenticate({
                username: vm.userName,
                password: vm.password,
                client_id: vm.clientid,
                client_secret: "secret",
                grant_type: vm.granttype,
                scope: vm.scope
            })
                .then(function (d) {
                    vm.token = d.data.access_token;
                    vm.expiresIn = d.data.expires_in;
                    vm.tokenType = d.data.token_type;

                    var base64Url = vm.token.split('.')[1];
                    var base64 = base64Url.replace('-', '+').replace('_', '/');
                    vm.tokenData = JSON.parse($window.atob(base64));

                    $log.info(d.data);
                    $log.info(vm.tokenData);
                },
                function (err) {
                    vm.message = "Error authenticating: " + err.data.error;
                    $log.error(err);
                });
        };

        activate();

        function activate() {
            vm.userName = 'leonardop';
            vm.password = 'swordfish987';
            vm.clientid = 'apiv1';
            vm.granttype = 'password';
            vm.scope = 'read';
        };
    }
})();