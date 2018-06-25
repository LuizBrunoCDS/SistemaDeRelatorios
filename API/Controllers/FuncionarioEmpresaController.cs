﻿using Domain.Entities;
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
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, relatorio);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
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
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, relatorio);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
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
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, relatorio);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
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
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, relatorio);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
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
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, relatorio);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
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
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, relatorio);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route("FuncionarioEmpresa/Insert")]
        [HttpPost]
        public HttpResponseMessage Insert(FuncionarioEmpresa funcionarioEmpresa)
        {
            try
            {
                if (funcionarioEmpresa == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent);

                var result = unitOfWork.FuncionarioEmpresaRepository.SelectByParam(filter: a => a.IdFuncionario == funcionarioEmpresa.IdFuncionario && a.IdEmpresa == funcionarioEmpresa.IdEmpresa && a.Data == funcionarioEmpresa.Data);

                if (result != null)
                    return Request.CreateResponse(HttpStatusCode.Conflict);

                unitOfWork.FuncionarioEmpresaRepository.Insert(funcionarioEmpresa);
                unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, "Inserido");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route("FuncionarioEmpresa/Update")]
        [HttpPut]
        public HttpResponseMessage Update(FuncionarioEmpresa funcionarioEmpresa)
        {
            try
            {
                if (funcionarioEmpresa == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent);

                var entity = unitOfWork.FuncionarioEmpresaRepository.SelectByParam(filter: a => a.IdFuncionarioEmpresa == funcionarioEmpresa.IdFuncionarioEmpresa);

                if (entity == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent);

                unitOfWork.FuncionarioEmpresaRepository.Update(entity, funcionarioEmpresa);
                unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, "Alterado");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
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
                    return Request.CreateResponse(HttpStatusCode.NoContent);

                unitOfWork.FuncionarioEmpresaRepository.Delete(funcionarioEmpresa);
                unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, "Removido");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
