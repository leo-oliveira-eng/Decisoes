angular.module("decisoes", []).controller("HomeController", function ($scope, $http) {

    $scope.UltimoId = null;
    $scope.Projetos = [];
    $scope.ErroAoCarregarProjetos = false;
    $scope.ErroCadastroDeProjeto = false;
    $scope.Nome = null;
    $scope.Cadastrar = false;

    var LimparErros = function() {
        $scope.ErroAoCarregarProjetos = false;
        $scope.ErroCadastroDeProjeto = false;
    };

    $scope.CancelarCadastro = function() {
        $scope.Cadastrar = false;
    };

    $scope.CarregarProjetos = function() {
        LimparErros();
        var url = "/Projetos/Carregar/{ultimoId?}";
        url = TratarUrl($scope, url);
        $http({
            method: "GET",
            url: url
        }).then(
            function Sucesso(response) {
                $scope.Projetos = response.data;
            },
            function Erro(response) {
                console.log(response);
                $scope.ErroAoCarregarProjetos = true;
            }
        );
    };

    $scope.FiltrarProjetosPorNome = function() {
        LimparErros();
        if($scope.Nome == null || $scope.Nome.length == 0)
            $scope.CarregarProjetos();
        else {
            var url = "/Projetos/"+$scope.Nome;
            url = TratarUrl($scope, url);
            $http({
                method: "GET",
                url: url
            }).then(
                function Sucesso(response) {
                    $scope.Projetos = response.data;
                },
                function Erro(response) {
                    console.log(response);
                    $scope.ErroAoCarregarProjetos = true;
                }
            );
        }
    };

    $scope.CadastrarProjeto = function() {
        LimparErros();
        var _nome = document.getElementById("NomeDoProjeto").value;
        if(_nome != null && _nome.length > 0) {
            $.ajax({
                url: "/Projeto/Cadastrar/",
                type: "POST",
                data: { 
                    Nome: _nome, 
                    Descricao: null
                },
                success: function(jqXHR, exception) {
                    if(jqXHR == "Ok") {
                        $scope.Cadastrar = false;
                        $scope.CarregarProjetos();
                    } else 
                        $scope.ErroCadastroDeProjeto = true;    
                }, 
                error: function (jqXHR, exception) {
                    $scope.ErroCadastroDeProjeto = true;
                    console.log(jqXHR);
                }
            });
        }
    }

    var TratarUrl = function($scope, url) {
        if ($scope.UltimoId != null && $scope.UltimoId > 0)
            url = url.replace("{ultimoId?}", $scope.UltimoId);
        else
            url = url.replace("{ultimoId?}", "");
        return url;
    };

});
