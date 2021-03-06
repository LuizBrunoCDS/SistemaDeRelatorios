﻿app.controller('LoginCtrl', function ($scope, SistemaDeRelatorios) {
    // faz o login e armazena numa session se retornar OK/Logado
    // OBS: nao é a melhor forma de realizar autenticação, mas é provisório  
    $scope.Usuario = {};
    checkSession();

    $scope.loginUser = function () {
        var result = SistemaDeRelatorios.Login($scope.Usuario);
        result.then(function (msg) {
            window.sessionStorage.setItem('credentials', msg);
            window.location.reload();
        }, function (msg) {
            alert('Erro: ' + msg.data);
        });
    }

    function checkSession() {
        if (window.sessionStorage.length < 1) {
            $scope.navBar = false;
            $scope.formLogin = true;
            $scope.credentials = false;
        } else {
            $scope.navBar = true;
            $scope.formLogin = false;
            $scope.credentials = true;
        }
    }
});

app.controller('EmpresasCtrl', function ($scope, SistemaDeRelatorios) {
    Empresas();
    $scope.Empresa = {};
    $scope.Flag = '';

    // preenche uma empresa no scope
    $scope.visualizarEmpresa = function (index, flag) {
        $scope.Empresa = $scope.lstEmpresas[index];
        $scope.Flag = flag;
    }

    // atualiza/adiciona uma empresa
    $scope.atualizarEmpresa = function () {
        var res = confirm('Deseja adicionar/atualizar os dados da empresa?');
        if (res == true) {
            if ($scope.Flag === 'update') {
                var result = SistemaDeRelatorios.atualizarEmpresa($scope.Empresa);
            } else {
                var result = SistemaDeRelatorios.adicionarEmpresa($scope.Empresa);
            }
            result.then(function (msg) {
                alert(msg.data);
                window.location.reload();
            }, function (msg) {
                alert('Erro: ' + msg.data);
            });
        }
    }

    // lista as empresas
    function Empresas() {
        var result = SistemaDeRelatorios.getEmpresas();
        result.then(function (empresas) {
            $scope.lstEmpresas = empresas.data;
            $scope.listaDeEmpresas = true;
        }, function () {
            $scope.listaDeEmpresas = false;
        });
    }
});

app.controller('FuncionariosCtrl', function ($scope, SistemaDeRelatorios) {
    Funcionarios();
    $scope.Funcionario = {};
    $scope.Flag = '';

    // preenche um funcionario no scope
    $scope.visualizarFuncionario = function (index, flag) {
        $scope.Funcionario = $scope.lstFuncionarios[index];
        $scope.Flag = flag;
    }

    // atualiza/adiciona um funcionario
    $scope.atualizarFuncionario = function () {
        var res = confirm('Deseja adicionar/atualizar os dados do funcionario?');
        if (res == true) {
            if ($scope.Flag === 'update') {
                var result = SistemaDeRelatorios.atualizarFuncionario($scope.Funcionario);
            } else {
                var result = SistemaDeRelatorios.adicionarFuncionario($scope.Funcionario);
            }
            result.then(function (msg) {
                alert(msg.data);
                window.location.reload();
            }, function (msg) {
                alert('Erro: ' + msg.data);
            });
        }
    }

    // lista os funcionarios
    function Funcionarios() {
        var result = SistemaDeRelatorios.getFuncionarios();
        result.then(function (funcionarios) {
            $scope.lstFuncionarios = funcionarios.data;
            $scope.listaDeFuncionarios = true;
        }, function () {
            $scope.listaDeFuncionarios = false;
        });
    }
});

app.controller('RelatoriosCtrl', function ($scope, SistemaDeRelatorios) {
    Empresas();
    Funcionarios();
    $scope.Meses = ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'];
    $scope.Anos = ['2010', '2011', '2012', '2013', '2014', '2015', '2016', '2017', '2018', '2019'];
    $scope.Parametro = {};
    $scope.listaDeRelatorios = false;

    // relatorio por funcionario
    $scope.relatorioPorFuncionario = function () {
        var result = SistemaDeRelatorios.relatorioFuncionario($scope.Parametro.IdFuncionario);
        result.then(function (lista) {
            $scope.lstRelatorios = lista.data;
            $scope.listaDeRelatorios = true;
        }, function (msg) {
            $scope.listaDeRelatorios = false;
            alert('Erro: ' + msg.data);
        });
    }

    // relatorio por empresa
    $scope.relatorioPorEmpresa = function () {
        var result = SistemaDeRelatorios.relatorioEmpresa($scope.Parametro.IdEmpresa);
        result.then(function (lista) {
            $scope.lstRelatorios = lista.data;
            $scope.listaDeRelatorios = true;
        }, function (msg) {
            $scope.listaDeRelatorios = false;
            alert('Erro: ' + msg.data);
        });
    }

    // relatorio por data
    $scope.relatorioPorData = function () {
        var day = ($scope.Parametro.Data).getUTCDate();
        var month = ($scope.Parametro.Data).getUTCMonth() + 1;
        var year = ($scope.Parametro.Data).getFullYear();
        var result = SistemaDeRelatorios.relatorioData(day, month, year);
        result.then(function (lista) {
            $scope.lstRelatorios = lista.data;
            $scope.listaDeRelatorios = true;
        }, function (msg) {
            $scope.listaDeRelatorios = false;
            alert('Erro: ' + msg.data);
        });
    }

    // relatorio por mes/ano
    $scope.relatorioPorMesAno = function () {
        var result = SistemaDeRelatorios.relatorioMes($scope.Parametro.Mes, $scope.Parametro.Ano);
        result.then(function (lista) {
            $scope.lstRelatorios = lista.data;
            $scope.listaDeRelatorios = true;
        }, function (msg) {
            $scope.listaDeRelatorios = false;
            alert('Erro: ' + msg.data);
        });
    }

    // relatorio por ano
    $scope.relatorioPorAno = function () {
        var result = SistemaDeRelatorios.relatorioAno($scope.Parametro.PorAno);
        result.then(function (lista) {
            $scope.lstRelatorios = lista.data;
            $scope.listaDeRelatorios = true;
        }, function (msg) {
            $scope.listaDeRelatorios = false;
            alert('Erro: ' + msg.data);
        });
    }

    function Empresas() {
        var result = SistemaDeRelatorios.getEmpresas();
        result.then(function (empresas) {
            $scope.lstEmpresas = empresas.data;
        }, function (msg) {
            alert('Erro: ' + msg.data);
        });
    }

    function Funcionarios() {
        var result = SistemaDeRelatorios.getFuncionarios();
        result.then(function (funcionarios) {
            $scope.lstFuncionarios = funcionarios.data;
        }, function (msg) {
            alert('Erro: ' + msg.data);
        });
    }
});

app.controller('CadastroRelatorioCtrl', function ($scope, SistemaDeRelatorios) {
    Empresas();
    Funcionarios();
    Relatorios();
    $scope.FuncionarioEmpresa = {};
    $scope.Flag = '';

    // visualiza o relatorio
    $scope.visualizarRelatorio = function (relatorio, flag) {
        $scope.FuncionarioEmpresa = relatorio;
        $scope.FuncionarioEmpresa.Data = new Date(relatorio.Data);
        $scope.Flag = flag
    }

    // adiciona/atualiza um relatorio de um funcionario
    $scope.adicionarRelatorio = function () {
        var res = confirm('Deseja adicionar/atualizar os dados do relatorio?');
        if (res == true) {
            if ($scope.Flag === 'update') {
                var result = SistemaDeRelatorios.atualizarRelatorio($scope.FuncionarioEmpresa);
            } else {                
                var result = SistemaDeRelatorios.adicionarRelatorio($scope.FuncionarioEmpresa);
            }
            result.then(function (msg) {
                alert(msg.data);
                window.location.reload();
            }, function (msg) {
                alert('Erro: ' + msg.data);
            });
        }
    }

    // deleta um relatorio de um funcionaro
    $scope.removerRelatorio = function (relatorio) {
        var res = confirm('Deseja apagar os dados do relatorio?');
        if (res == true) {
            var result = SistemaDeRelatorios.deleteRelatorio(relatorio.IdFuncionarioEmpresa);
            result.then(function (msg) {
                alert(msg.data);
                window.location.reload();
            }, function (msg) {
                alert('Erro: ' + msg.data);
            });
        }
    }

    // lista os relatorios
    function Relatorios() {
        var result = SistemaDeRelatorios.getRelatorios();
        result.then(function (relatorios) {
            $scope.lstRelatorios = relatorios.data;
            $scope.listaDeRelatorios = true;
        }, function (msg) {
            $scope.listaDeRelatorios = false;
            alert('Erro: ' + msg.data)
        });
    }

    // lista as empresas
    function Empresas() {
        var result = SistemaDeRelatorios.getEmpresas();
        result.then(function (empresas) {
            $scope.lstEmpresas = empresas.data;
        }, function (msg) {
            alert('Erro: ' + msg.data)
        });
    }

    // lista os funcionarios
    function Funcionarios() {
        var result = SistemaDeRelatorios.getFuncionarios();
        result.then(function (funcionarios) {
            $scope.lstFuncionarios = funcionarios.data;
        }, function (msg) {
            alert('Erro: ' + msg.data)
        });
    }
});