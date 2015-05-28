'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', 'localStorageService', function ($scope, $location, authService, localStorageService) {

    $scope.doctor = localStorageService.get('doctorData');

    $scope.logOut = function () {
        $scope.doctor = null;
        authService.logOut();
        $location.path('/home');
    }

    $scope.authentication = authService.authentication;

    $scope.$on('event:loginSuccess', function (ev, user) {
        $scope.doctor = localStorageService.get('doctorData');
    });
}]);