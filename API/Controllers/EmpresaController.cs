﻿using Domain.Entities;
using Domain.Interfaces;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class EmpresaController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public EmpresaController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [Route("Empresa/GetAll")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                var empresas = unitOfWork.EmpresaRepository.SelectAll(orderBy: a => a.OrderBy(b => b.Nome));

                if (!empresas.Any())
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Não foi encontrado nenhuma empresa registrada");
                return Request.CreateResponse(HttpStatusCode.OK, empresas);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
            }
        }

        [Route("Empresa/Insert")]
        [HttpPost]
        public HttpResponseMessage Post(Empresa empresa)
        {
            try
            {
                if (empresa == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent, "Os dados para registrar o funcionario estão incompletos");

                var result = unitOfWork.EmpresaRepository.SelectByParam(filter: a => a.Nome == empresa.Nome && a.Local == empresa.Local);

                if (result != null)
                    return Request.CreateResponse(HttpStatusCode.Conflict, "Já existe um funcionario registrado com o CPF passado");

                unitOfWork.EmpresaRepository.Insert(empresa);
                unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, "Dados do funcionario inseridos com sucesso");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
            }
        }

        [Route("Empresa/Update")]
        [HttpPut]
        public HttpResponseMessage Update(Empresa empresa)
        {
            try
            {
                if (empresa == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent, "Os dados para registrar o funcionario estão incompletos");

                var entity = unitOfWork.EmpresaRepository.SelectByParam(filter: a => a.IdEmpresa == empresa.IdEmpresa);

                if (entity == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent, "Não existe dados registrados para este funcionario");

                unitOfWork.EmpresaRepository.Update(entity, empresa);
                unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, "Dados do funcionario alterados com sucesso");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
            }
        }
    }
}
