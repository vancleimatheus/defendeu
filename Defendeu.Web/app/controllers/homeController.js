'use strict';
app.controller('homeController',  ['$scope', 'contactService', function ($scope, contactService) {
    $scope.message = '';
    $scope.contactMessage = ''
    $scope.sendingContact = false;

    $scope.sendContact = function()
    {
        $scope.sendingContact = true;
        contactService.create($scope.contact).then(function (results) {
            $scope.sendingContact = false;
            $scope.contactMessage = 'Mensagem enviada com sucesso. Em breve entraremos em contato.';
        });
    }
}]);