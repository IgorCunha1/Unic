
var app = angular.module('Unic', []);

app.factory('servicePessoa', ['$http', function ($http) {
    var service = {};

    service.ListarPessoas = function () {
        return $http.get('ListarPessoa');
    }

    return service;
}]);


app.controller('PessoaController', ['$scope', '$http', 'servicePessoa', function ($scope, $http, servicePessoa) {

    $scope.ListarPessoas = function () {
        servicePessoa.ListarPessoas().then(function (response) {
            $scope.Pessoas = response.data;
            console.log(response);
        });
    }


}]);