app.service("SistemaDeRelatorios", function ($http) {
    // realiza o login no sistema
    this.Login = function (usuario) {
        var response = $http({
            url: 'http://localhost:52141/Usuarios/Login',
            method: 'post',
            data: JSON.stringify(usuario),
            dataType: 'json'
        });
        return response;
    }

    // obtem todos as empresas
    this.getEmpresas = function () {
        var response = $http({
            method: 'get',
            url: 'http://localhost:52141/Empresa/GetAll'
        });
        return response;
    };

    // adicionar empresa
    this.adicionarEmpresa = function (empresa) {
        var response = $http({
            url: 'http://localhost:52141/Empresa/Insert',
            method: 'post',
            data: JSON.stringify(empresa),
            dataType: 'json'
        });
        return response;
    }

    // update empresa
    this.atualizarEmpresa = function (empresa) {
        var response = $http({
            url: 'http://localhost:52141/Empresa/Update',
            method: 'put',
            data: JSON.stringify(empresa),
            dataType: 'json'
        });
        return response;
    }

    // obtem todos os funcionarios
    this.getFuncionarios = function () {
        var response = $http({
            method: 'get',
            url: 'http://localhost:52141/Funcionario/GetAll'
        });
        return response;
    };

    // obtem todos os funcionarios ativos
    this.getFuncionariosAtiv = function () {
        var response = $http({
            method: 'get',
            url: 'http://localhost:52141/Funcionario/GetByStatus'
        });
        return response;
    };

    // obtem candidato por id
    this.getFuncionario = function (funcId) {
        var response = $http({
            method: 'get',
            url: 'http://localhost:52141/Funcionario/GetById/' + funcId
        });
        return response;
    }

    // atualizar funcionario
    this.atualizarFuncionario = function (funcionario) {
        var response = $http({
            url: 'http://localhost:52141/Funcionario/Update',
            method: 'put',
            data: JSON.stringify(funcionario),
            dataType: 'json'
        });
        return response;
    }

    // adicionar funcionario
    this.adicionarFuncionario = function (funcionario) {
        var response = $http({
            url: 'http://localhost:52141/Funcionario/Insert',
            method: 'post',
            data: JSON.stringify(funcionario),
            dataType: 'json'
        });
        return response;
    }

    // obtem todos os relatorios
    this.getRelatorios = function () {
        var response = $http({
            method: 'get',
            url: 'http://localhost:52141/FuncionarioEmpresa/GetAll/'
        });
        return response;
    }

    // relatorio por funcionario
    this.relatorioFuncionario = function (funcionarioId) {
        var response = $http({
            url: 'http://localhost:52141/FuncionarioEmpresa/GetByFuncionario/' + funcionarioId,
            method: 'get'
        });
        return response;
    }

    // relatorio por empresa
    this.relatorioEmpresa = function (empresaId) {
        var response = $http({
            url: 'http://localhost:52141/FuncionarioEmpresa/GetByEmpresa/' + empresaId,
            method: 'get'
        });
        return response;
    }

    // relatorio por data
    this.relatorioData = function (day, month, year) {
        var response = $http({
            url: 'http://localhost:52141/FuncionarioEmpresa/GetByDate/' + day + '/' + month + '/' + year,
            method: 'get'
        });
        return response;
    }

    // relatorio por mes
    this.relatorioMes = function (mes, ano) {
        var response = $http({
            url: 'http://localhost:52141/FuncionarioEmpresa/GetByMonth/' + mes + '/' + ano,
            method: 'get'
        });
        return response;
    }

    // relatorio por ano
    this.relatorioAno = function (ano) {
        var response = $http({
            url: 'http://localhost:52141/FuncionarioEmpresa/GetByYear/' + ano,
            method: 'get'
        });
        return response;
    }

    // adicionar relatorio
    this.adicionarRelatorio = function (relatorio) {
        var response = $http({
            url: 'http://localhost:52141/FuncionarioEmpresa/Insert',
            method: 'post',
            data: JSON.stringify(relatorio),
            dataType: 'json'
        });
        return response;
    }

    // update relatorio
    this.atualizarRelatorio = function (relatorio) {
        var response = $http({
            url: 'http://localhost:52141/FuncionarioEmpresa/Update',
            method: 'put',
            data: JSON.stringify(relatorio),
            dataType: 'json'
        });
        return response;
    }

    // delete relatorio
    this.deleteRelatorio = function (relatorioId) {
        var response = $http({
            url: 'http://localhost:52141/FuncionarioEmpresa/Delete/' + relatorioId,
            method: 'delete'
        });
        return response;
    }
});