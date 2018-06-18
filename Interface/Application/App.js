var app = angular.module("App", ['ngRoute']);

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'Views/Home.html'
        })
        .when('/Empresas', {
            templateUrl: 'Views/Empresas.html',
            controller: 'EmpresasCtrl'
        })
        .when('/Funcionarios', {
            templateUrl: 'Views/Funcionarios.html',
            controller: 'FuncionariosCtrl'
        })
        .when('/Relatorios', {
            templateUrl: 'Views/Relatorios.html',
            controller: 'RelatoriosCtrl'
        })
        .when('/CadastroRelatorio', {
            templateUrl: 'Views/CadastroRelatorio.html',
            controller: 'CadastroRelatorioCtrl'
        })
        .otherwise({ redirectTo: '/' });
}]);