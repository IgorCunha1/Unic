
var app = angular.module('Unic', []);

app.factory('servicePessoa', ['$http', function ($http) {
    var service = {};

    service.ListarPessoas = function () {
        return $http.get('ListarPessoa');
    }

    service.AdicionarPessoa = function (pessoa) {
        return $http.post('AdicionarPessoa', pessoa)
    }

    service.AdicionarEndereco = function (endereco) {
        return $http.post('AdicionarEndereco', endereco)
    }

    service.PesquisaCep = function(cep){
        return $http.get('https://viacep.com.br/ws/'+cep+'/json/')
    }

    return service;
}]);


app.controller('PessoaController', ['$scope', '$http', 'servicePessoa', function ($scope, $http, servicePessoa) {

    $scope.ListarPessoas = function () {
        servicePessoa.ListarPessoas().then(function (response) {
            $scope.Pessoas = response.data;
            
        });
    }

    $scope.pesquisaCep = function (cep){
         servicePessoa.PesquisaCep(cep).then(function (response){
            $scope.end = response.data;
             $scope.logradouro = $scope.end.logradouro;
             $scope.bairro = $scope.end.bairro;
             $scope.cidade = $scope.end.localidade;
             $scope.estado = $scope.end.uf;
            console.log($scope.end);
        })
    }

    $scope.AdicionarPessoa = function (){
        $scope.pessoa = {};
        let data = new Date($scope.nascimento);
        $scope.pessoa.nomeCompleto = $scope.nomeCompleto;
        $scope.pessoa.cpf = $scope.cpf.replace(/[^a-z0-9]/gi, '');
        // $scope.pessoa.nascimento = data.toLocaleDateString('EN',{timezone: 'UTC'});
        console.log(data.toLocaleDateString('EN',{timezone: 'UTC'}));        
        
        $scope.endereco = {};
        $scope.endereco.logradouro = $scope.logradouro;
        $scope.endereco.cep = $scope.cep.replace(/[^a-z0-9]/gi,'');
        $scope.endereco.numero = $scope.numero;
        $scope.endereco.bairro = $scope.bairro;
        $scope.endereco.cidade = $scope.cidade;
        $scope.endereco.estado = $scope.estado;
        console.log($scope.endereco);

        servicePessoa.AdicionarEndereco($scope.endereco).then(function(response) {
            console.log(response);
            $scope.pessoa.enderecoId = response.data.id;
            
            servicePessoa.AdicionarPessoa($scope.pessoa).then(function (response){
                
            })
        })
        
    }


}]);