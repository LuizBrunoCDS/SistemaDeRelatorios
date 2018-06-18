app.controller('LoginCtrl', function ($scope, SistemaDeRelatorios) {
    // faz o login e armazena numa session se retornar OK/Logado
    // TODO: nao é a melhor forma de realizar autenticação, mas é provisório  
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
            $scope.logado = false;
        } else {
            $scope.navBar = true;
            $scope.formLogin = false;
            $scope.logado = true;
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
                alert('Dados da empresa alterados/inserido');
                window.location.reload();
            }, function () {
                alert('Erro ao tentar atualizar/adicionar os registros da empresa');
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
                alert('Dados do funcionario alterados/inserido');
                window.location.reload();
            }, function () {
                alert('Erro ao tentar atualizar/adicionar os registros do funcionario');
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
        }, function () {
            $scope.listaDeRelatorios = false;
            alert('Não foi encontrado nenhum registro com o parametro passado.');
        });
    }

    // relatorio por empresa
    $scope.relatorioPorEmpresa = function () {
        var result = SistemaDeRelatorios.relatorioEmpresa($scope.Parametro.IdEmpresa);
        result.then(function (lista) {
            $scope.lstRelatorios = lista.data;
            $scope.listaDeRelatorios = true;
        }, function () {
            $scope.listaDeRelatorios = false;
            alert('Não foi encontrado nenhum registro com o parametro passado.');
        });
    }

    // relatorio por mes/ano
    $scope.relatorioPorMesAno = function () {
        var result = SistemaDeRelatorios.relatorioMes($scope.Parametro.Mes, $scope.Parametro.Ano);
        result.then(function (lista) {
            $scope.lstRelatorios = lista.data;
            $scope.listaDeRelatorios = true;
        }, function () {
            $scope.listaDeRelatorios = false;
            alert('Não foi encontrado nenhum registro com os parametros passados.');
        });
    }

    // relatorio por ano
    $scope.relatorioPorAno = function () {
        var result = SistemaDeRelatorios.relatorioAno($scope.Parametro.PorAno);
        result.then(function (lista) {
            $scope.lstRelatorios = lista.data;
            $scope.listaDeRelatorios = true;
        }, function () {
            $scope.listaDeRelatorios = false;
            alert('Não foi encontrado nenhum registro com o parametro passado.');
        });
    }

    function Empresas() {
        var result = SistemaDeRelatorios.getEmpresas();
        result.then(function (empresas) {
            $scope.lstEmpresas = empresas.data;
        });
    }

    function Funcionarios() {
        var result = SistemaDeRelatorios.getFuncionarios();
        result.then(function (funcionarios) {
            $scope.lstFuncionarios = funcionarios.data;
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
                alert('Dados do funcionario alterados/inserido');
                window.location.reload();
            }, function () {
                alert('Erro ao tentar atualizar/adicionar os registros do funcionario');
            });
        }
    }

    // deleta um relatorio de um funcionaro
    $scope.removerRelatorio = function (relatorio) {
        var res = confirm('Deseja apagar os dados do relatorio? ' +
            '\n Nome: ' + relatorio.funcionario.Nome + '' +
            '\n Empresa: ' + relatorio.empresa.Nome + '' +
            '\n Data: ' + relatorio.Data + '' +
            '\n Inicio: ' + relatorio.HorarioInicio + '' +
            '\n Termino: ' + relatorio.HorarioTermino + '');
        if (res == true) {
            var result = SistemaDeRelatorios.deleteRelatorio(relatorio.IdFuncionarioEmpresa);
            result.then(function (msg) {
                alert('Relatorio removido');
                window.location.reload();
            }, function () {
                alert('Erro ao tentar apagar o relatorio');
            });
        }
    }

    // lista os relatorios
    function Relatorios() {
        var result = SistemaDeRelatorios.getRelatorios();
        result.then(function (relatorios) {
            $scope.lstRelatorios = relatorios.data;
            $scope.listaDeRelatorios = true;
        }, function () {
            $scope.listaDeRelatorios = false;
        });
    }

    // lista as empresas
    function Empresas() {
        var result = SistemaDeRelatorios.getEmpresas();
        result.then(function (empresas) {
            $scope.lstEmpresas = empresas.data;
        });
    }

    // lista os funcionarios
    function Funcionarios() {
        var result = SistemaDeRelatorios.getFuncionarios();
        result.then(function (funcionarios) {
            $scope.lstFuncionarios = funcionarios.data;
        });
    }
});