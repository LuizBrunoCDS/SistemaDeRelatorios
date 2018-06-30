using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class FuncionarioEmpresaController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public FuncionarioEmpresaController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [Route("FuncionarioEmpresa/GetAll")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var relatorio = unitOfWork.FuncionarioEmpresaRepository.SelectAll(orderBy: a => a.OrderBy(b => b.Data), includeProperties: "funcionario,empresa");

                if (!relatorio.Any())
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Não foi encontrado nenhum registro");

                return Request.CreateResponse(HttpStatusCode.OK, relatorio);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
            }
        }

        [Route("FuncionarioEmpresa/GetByFuncionario/{id}")]
        [HttpGet]
        public HttpResponseMessage GetByFuncionario(int id)
        {
            try
            {
                var relatorio = unitOfWork.FuncionarioEmpresaRepository.SelectAll(filter: a => a.IdFuncionario == id, orderBy: a => a.OrderBy(b => b.Data), includeProperties: "funcionario,empresa");

                if (!relatorio.Any())
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Não foi encontrado nenhum registro com o parametro passado");

                return Request.CreateResponse(HttpStatusCode.OK, relatorio);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
            }
        }

        [Route("FuncionarioEmpresa/GetByEmpresa/{id}")]
        [HttpGet]
        public HttpResponseMessage GetByEmpresa(int id)
        {
            try
            {
                var relatorio = unitOfWork.FuncionarioEmpresaRepository.SelectAll(filter: a => a.IdEmpresa == id, orderBy: a => a.OrderBy(b => b.Data), includeProperties: "funcionario,empresa");

                if (!relatorio.Any())
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Não foi encontrado nenhum registro com o parametro passado");

                return Request.CreateResponse(HttpStatusCode.OK, relatorio);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
            }
        }

        [Route("FuncionarioEmpresa/GetByDate/{day}/{month}/{year}")]
        [HttpGet]
        public HttpResponseMessage GetByMonth(int day, int month, int year)
        {
            try
            {
                DateTime date = new DateTime(year, month, day);
                var relatorio = unitOfWork.FuncionarioEmpresaRepository.SelectAll(filter: a => a.Data == date, orderBy: a => a.OrderBy(b => b.Data), includeProperties: "funcionario,empresa");

                if (!relatorio.Any())
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Não foi encontrado nenhum registro com os parametros passados");

                return Request.CreateResponse(HttpStatusCode.OK, relatorio);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
            }
        }

        [Route("FuncionarioEmpresa/GetByMonth/{month}/{year}")]
        [HttpGet]
        public HttpResponseMessage GetByMonth(int month, int year)
        {
            try
            {
                var relatorio = unitOfWork.FuncionarioEmpresaRepository.SelectAll(filter: a => a.Data.Month == month && a.Data.Year == year, orderBy: a => a.OrderBy(b => b.Data), includeProperties: "funcionario,empresa");

                if (!relatorio.Any())
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Não foi encontrado nenhum registro com os parametros passados");

                return Request.CreateResponse(HttpStatusCode.OK, relatorio);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
            }
        }

        [Route("FuncionarioEmpresa/GetByYear/{year}")]
        [HttpGet]
        public HttpResponseMessage GetByYear(int year)
        {
            try
            {
                var relatorio = unitOfWork.FuncionarioEmpresaRepository.SelectAll(filter: a => a.Data.Year == year, orderBy: a => a.OrderBy(b => b.Data), includeProperties: "funcionario,empresa");

                if (!relatorio.Any())
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Não foi encontrado nenhum registro com o parametro passado");

                return Request.CreateResponse(HttpStatusCode.OK, relatorio);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
            }
        }

        [Route("FuncionarioEmpresa/Insert")]
        [HttpPost]
        public HttpResponseMessage Insert(FuncionarioEmpresa funcionarioEmpresa)
        {
            try
            {
                if (funcionarioEmpresa == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent, "Os dados para registrar o relatorio estão incompletos");

                var result = unitOfWork.FuncionarioEmpresaRepository.SelectByParam(filter: a => a.IdFuncionario == funcionarioEmpresa.IdFuncionario && a.IdEmpresa == funcionarioEmpresa.IdEmpresa && a.Data == funcionarioEmpresa.Data);

                if (result != null)
                    return Request.CreateResponse(HttpStatusCode.Conflict, "Já existe um relatorio registrado com os dados passados");

                unitOfWork.FuncionarioEmpresaRepository.Insert(funcionarioEmpresa);
                unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, "Dados do relatorio inseridos com sucesso");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
            }
        }

        [Route("FuncionarioEmpresa/Update")]
        [HttpPut]
        public HttpResponseMessage Update(FuncionarioEmpresa funcionarioEmpresa)
        {
            try
            {
                if (funcionarioEmpresa == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent, "Os dados para registrar o relatorio estão incompletos");

                var entity = unitOfWork.FuncionarioEmpresaRepository.SelectByParam(filter: a => a.IdFuncionarioEmpresa == funcionarioEmpresa.IdFuncionarioEmpresa);

                if (entity == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent, "Não existe dados registrados para este relatorio");

                unitOfWork.FuncionarioEmpresaRepository.Update(entity, funcionarioEmpresa);
                unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, "Dados do relatorio alterados com sucesso");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
            }
        }

        [Route("FuncionarioEmpresa/Delete/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var funcionarioEmpresa = unitOfWork.FuncionarioEmpresaRepository.SelectByParam(filter: a => a.IdFuncionarioEmpresa == id);

                if (funcionarioEmpresa == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent, "Não existe relatorio registrado com esse ID");

                unitOfWork.FuncionarioEmpresaRepository.Delete(funcionarioEmpresa);
                unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, "Relatorio removido com sucesso");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
            }
        }
    }
}
