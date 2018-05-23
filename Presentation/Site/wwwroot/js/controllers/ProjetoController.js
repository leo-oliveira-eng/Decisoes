angular.module("decisoes", []).controller("ProjetoController", function ($scope, $http, $interval) {

    $scope.Projeto = null;
    $scope.Alternativas = [];
    $scope.ErroAoCarregarProjeto = false;
    $scope.ErroAoCarregarAlternativas = false;
    $scope.NomeCriterio = null;
    $scope.TipoCriterio = null;
    $scope.Peso = 1;
    $scope.idProjeto = null;
    $scope.Criterios = [];
    $scope.ativarGeracaoMatriz = false;
    $scope.MatrizExiste = false;
    $scope.Matriz = [];
    $scope.Valor = 0;
    $scope.ItensGrafico = [];

    var carregouCriterios = false;
    var carregouAlternativas = false;

    var Init = function () {
        var contador = function () {
            $scope.ativarGeracaoMatriz = podeGerarMatriz();
        };
        $interval(contador, 1000);
        $scope.idProjeto = $("#idProjeto").val();
        CarregarDados();
    };

    var CarregarDados = function () {
        CarregarProjeto();
        CarregarAlternativas();
        carregarCriterios();
        consultarExistenciaDaMatriz();
        carregarMatriz();
    };

    var CarregarProjeto = function () {
        LimparErros();
        var url = "/Projeto/" + $scope.idProjeto;
        $http({
            method: "GET",
            url: url
        }).then(
            function Sucesso(response) {
                $scope.Projeto = response.data;
            },
            function Erro(response) {
                console.log(response);
                $scope.ErroAoCarregarProjeto = true;
            }
        );
    };

    var LimparErros = function() {
        $scope.ErroAoCarregarProjeto = false;
        $scope.ErroAoCarregarAlternativas = false;
    };

    var CarregarAlternativas = function() {
        LimparErros();
        var url = "/Alternativas/" + $scope.idProjeto;
        $http({
            method: "GET",
            url: url
        }).then(
            function Sucesso(response) {
                var items = response.data;
                $scope.Alternativas = items;
                carregouAlternativas = true;
                var alternativas = [];
                for(var i = 0; i < items.length; i++)
                    $(".tagsInput").tagsinput("add", items[i].Nome);
                $(".tagsInput").unbind();
                $(".tagsInput").on("beforeItemRemove", function (event) {
                    excluirAlternativa(event.item);
                });
                $(".tagsInput").on("beforeItemAdd", function (event) {
                    if (!event.options || !event.options.preventPost) {
                        adicionarAlternativa(event.item);
                    }
                });
                carregarMatriz();
                if ($scope.Alternativas.length > 0)
                    montarGrafico();
            },
            function Erro(response) {
                console.log(response);
                $scope.ErroAoCarregarAlternativas = true;
            }
        );
    };

    var excluirAlternativas = function () {
        var rq = {
            method: "DELETE",
            url: "/Alternativas/" + $scope.idProjeto + "/Excluir",
        };
        $http(rq).then(
            function Sucesso(response) {
                CarregarAlternativas();
            },
            function Erro(response) {
                console.log(response);
            }
        );
    };

    var carregarCriterios = function () {
        var rq = {
            method: "GET",
            url: "/Criterios/" + $scope.idProjeto
        };
        $http(rq).then(
            function Sucesso(response) {
                $scope.Criterios = response.data;
                carregouCriterios = true;
            },
            function Erro(response) {
                console.log(response);
            }
        );
    };

    var salvarCriterio = function () {
        if (podeSalvarCriterio()) {
            var rq = {
                method: "POST",
                url: "/Criterio/Cadastrar/",
                data: {
                    IdProjeto: $scope.idProjeto,
                    Nome: $scope.NomeCriterio,
                    TipoDeCriterio: $scope.TipoCriterio,
                    Peso: $scope.Peso
                }
            };
            $http(rq).then(
                function Sucesso(response) {
                    if (response.data === "Ok") {
                        carregarCriterios();
                        limparFormularioDeCriterios();
                    }
                },
                function Erro(response) {
                    console.log(response);
                }
            );
        }
    };

    var podeSalvarCriterio = function () {
        return $scope.NomeCriterio !== null
            && $scope.NomeCriterio.length > 0
            && $scope.NomeCriterio.length <= 50
            && $scope.TipoCriterio !== null
            && String($scope.TipoCriterio).length > 0
            && $scope.Peso !== null
            && String($scope.Peso).length > 0
    };

    var limparFormularioDeCriterios = function () {
        $scope.NomeCriterio = null;
        $scope.TipoCriterio = null;
        $scope.Peso = 1;
    };

    var excluirCriterio = function (idCriterio) {
        var rq = {
            method: "DELETE",
            url: "/Criterio/Deletar/" + idCriterio
        };
        $http(rq).then(
            function Sucesso(response) {
                if (response.data === "Ok") {
                    carregarCriterios();
                }
            },
            function Erro(response) {
                console.log(response);
            }
        );
    };

    var podeGerarMatriz = function () {
        return carregouAlternativas
            && carregouCriterios
            && $scope.Criterios.length > 0
            && $scope.Alternativas.length > 0;
    };

    var gerarMatriz = function () {
        if (podeGerarMatriz()) {
            var rq = {
                method: "POST",
                url: "/Matriz/" + $scope.idProjeto + "/Gerar"
            };
            $http(rq).then(
                function Sucesso(response) {
                    console.log(response);
                },
                function Erro(response) {
                    console.log(response);
                }
            );
        } else {
            alert("É preciso que existam critérios e alternativas cadastradas!");
        }
    };

    var consultarExistenciaDaMatriz = function () {
        var rq = {
            method: "GET",
            url: "/Matriz/" + $scope.idProjeto + "/Existe"
        };
        $http(rq).then(
            function Sucesso(response) {
                $scope.matrizExiste = response.data;
            },
            function Erro(response) {
                console.log(response);
            }
        );
    };

    var carregarMatriz = function () {
        var rq = {
            method: "GET",
            url: "/Matriz/" + $scope.idProjeto
        };
        $http(rq).then(
            function Sucesso(response) {
                $scope.Matriz = response.data;
                carregarValoresDaMatriz();
                habilitarMatriz();
            },
            function Erro(response) {
                console.log(response);
            }
        );
    };

    var carregarValoresDaMatriz = function () {
        for (var i = 0; i < $scope.Matriz.length; i++) {
            var idProjeto = $scope.Matriz[i].Projeto.Id;
            var idAlternativa = $scope.Matriz[i].Alternativa.Id;
            var idCriterio = $scope.Matriz[i].Criterio.Id;
            $("#" + idProjeto + "_" + idAlternativa + "_" + idCriterio).val($scope.Matriz[i].Valor);
        }
    };

    var adicionarAlternativa = function (alternativa) {
        var idAlternativa = obterIdAlternativa(alternativa);
        var rq = {
            method: "POST",
            url: "/Alternativa/Cadastrar/",
            data: {
                Id: $scope.idProjeto,
                Alternativa: alternativa
            }
        };
        $http(rq).then(
            function Sucesso(response) {
                if (response.data === "Ok") {
                    CarregarAlternativas();
                }
            },
            function Erro(response) {
                console.log(response);
            }
        );
    };

    var excluirAlternativa = function (alternativa) {
        var idAlternativa = obterIdAlternativa(alternativa);
        var rq = {
            method: "DELETE",
            url: "/Alternativa/" + idAlternativa + "/Excluir"
        };
        $http(rq).then(
            function Sucesso(response) {
                if (response.data === "Ok") {
                    carregarAlternativas();
                }
            },
            function Erro(response) {
                console.log(response);
            }
        );
    };

    var obterIdAlternativa = function (alternativa) {
        return Enumerable.from($scope.Alternativas)
            .where(function (x) { return x.Nome === alternativa; })
            .select(function (x) { return x.Id })
            .toArray();
    };

    var habilitarMatriz = function () {
        return $scope.Matriz.length > 0;
    };

    var salvarMatriz = function () {
        var lista = obterItensParaSalvar();
        if (lista.length > 0) {
            var rq = {
                method: "PUT",
                url: "/Matriz/Salvar/",
                data: { Matriz: lista }
            };
            $http(rq).then(
                function Sucesso(response) {
                    console.log(response);
                },
                function Erro(response) {
                    console.log(response);
                }
            );
        }
    };

    var obterItensParaSalvar = function () {
        var lista = [];
        var itens = $(".inputValor");
        for (var i = 0; i < itens.length; i++)
            lista.push({
                IdProjeto: itens[i].id.split("_")[0],
                IdAlternativa: itens[i].id.split("_")[1],
                IdCriterio: idCriterio = itens[i].id.split("_")[2],
                Valor: itens[i].value
            });
        return lista;
    };

    var gerarResultado = function () {
        var rq = {
            method: "POST",
            url: "/Matriz/" + $scope.idProjeto + "/GerarResultado/",
        };
        $http(rq).then(
            function Sucesso(response) {
                CarregarAlternativas();
            },
            function Erro(response) {
                console.log(response);
            }
        );
    };

    var montarGrafico = function () {
        google.charts.load('current', { packages: ['corechart', 'bar'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            google.charts.load("current", { packages: ['corechart'] });
            google.charts.setOnLoadCallback(drawChart);

            function drawChart() {
                var data = google.visualization.arrayToDataTable(obterInformacaoParaGrafico());

                var view = new google.visualization.DataView(data);
                view.setColumns(
                    [
                        0, 1,
                    {
                        calc: "stringify",
                        sourceColumn: 1,
                        type: "string",
                        role: "annotation"
                    }]);

                var options = {
                    title: "RESULTADOS TOPSIS",
                    width: 1000,
                    height: 400,
                    bar: { groupWidth: "50%" },
                    legend: { position: "none" },
                };

                var chart = new google.visualization.ColumnChart(document.getElementById("chart_div"));
                chart.draw(view, options);
            }
        }
    };

    var obterInformacaoParaGrafico = function () {
        var dados = [];
        dados.push(["Alternativa", "Score"]);
        var alternativas = Enumerable
            .from($scope.Alternativas)
            .orderByDescending(function(x) { return x.Score })
            .toArray();
        for (var i = 0; i < alternativas.length; i++)
            dados.push([alternativas[i].Nome, alternativas[i].Score]);
        return dados;
    };

    var obterItensDoGrafico = function () {
        return Enumerable
            .from($scope.Alternativas)
            .select(function (alternativa) {
                return Math.round(alternativa.Score * 100);
            })
            .toArray();
    };

    var obterNomeDasAlternativas = function () {
        return Enumerable
            .from($scope.Matriz)
            .select(function (x) {
                return x.Alternativa.Nome
            })
            .distinct()
            .toArray();
    };

    $scope.Init = Init;
    $scope.salvarCriterio = salvarCriterio;
    $scope.excluirCriterio = excluirCriterio;
    $scope.gerarMatriz = gerarMatriz;
    $scope.habilitarMatriz = habilitarMatriz;
    $scope.salvarMatriz = salvarMatriz;
    $scope.gerarResultado = gerarResultado;

    window.$scope = $scope;

});
