using Domain.Entities;
using Domain.Interfaces;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class FuncionarioController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public FuncionarioController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [Route("Funcionario/GetAll")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var funcionarios = unitOfWork.FuncionarioRepository.SelectAll(orderBy: a => a.OrderBy(b => b.Nome));

                if (!funcionarios.Any())
                    return Request.CreateResponse(HttpStatusCode.NoContent);

                return Request.CreateResponse(HttpStatusCode.OK, funcionarios);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route("Funcionario/GetById/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var funcionario = unitOfWork.FuncionarioRepository.SelectByParam(filter: a => a.IdFuncionario == id);

                if (funcionario == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, funcionario);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route("Funcionario/GetByStatus")]
        [HttpGet]
        public HttpResponseMessage GetByStatus()
        {
            try
            {
                var funcionarios = unitOfWork.FuncionarioRepository.SelectAll(filter: a => a.Status == "Ativo", orderBy: a => a.OrderBy(b => b.Nome));

                if (!funcionarios.Any())
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, funcionarios);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route("Funcionario/Insert")]
        [HttpPost]
        public HttpResponseMessage Post(Funcionario funcionario)
        {
            try
            {
                if (funcionario == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent);

                var result = unitOfWork.FuncionarioRepository.SelectByParam(filter: a => a.CPF == funcionario.CPF);

                if (result != null)
                    return Request.CreateResponse(HttpStatusCode.Conflict);

                unitOfWork.FuncionarioRepository.Insert(funcionario);
                unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, "Inserido");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route("Funcionario/Update")]
        [HttpPut]
        public HttpResponseMessage Put(Funcionario funcionario)
        {
            try
            {
                if (funcionario == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent);

                var entity = unitOfWork.FuncionarioRepository.SelectByParam(filter: a => a.IdFuncionario == funcionario.IdFuncionario);

                if (entity == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent);

                unitOfWork.FuncionarioRepository.Update(entity, funcionario);
                unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, "Alterado");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
