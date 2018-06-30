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
                    return Request.CreateResponse(HttpStatusCode.NoContent, "Não foi encontrado nenhum funcionario registrado");

                return Request.CreateResponse(HttpStatusCode.OK, funcionarios);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
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
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Não foi encontrado nenhum funcionario com este parametro");

                return Request.CreateResponse(HttpStatusCode.OK, funcionario);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
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
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Não foi encontrado nenhum funcionario ativo");

                return Request.CreateResponse(HttpStatusCode.OK, funcionarios);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
            }
        }

        [Route("Funcionario/Insert")]
        [HttpPost]
        public HttpResponseMessage Post(Funcionario funcionario)
        {
            try
            {
                if (funcionario == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent, "Os dados para registrar o funcionario estão incompletos");

                var result = unitOfWork.FuncionarioRepository.SelectByParam(filter: a => a.CPF == funcionario.CPF);

                if (result != null)
                    return Request.CreateResponse(HttpStatusCode.Conflict, "Já existe um funcionario registrado com o CPF passado");

                unitOfWork.FuncionarioRepository.Insert(funcionario);
                unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, "Dados do funcionario inseridos com sucesso");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no Serverside ao realizar esta ação");
            }
        }

        [Route("Funcionario/Update")]
        [HttpPut]
        public HttpResponseMessage Put(Funcionario funcionario)
        {
            try
            {
                if (funcionario == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent, "Os dados para registrar o funcionario estão incompletos");

                var entity = unitOfWork.FuncionarioRepository.SelectByParam(filter: a => a.IdFuncionario == funcionario.IdFuncionario);

                if (entity == null)
                    return Request.CreateResponse(HttpStatusCode.NoContent, "Não existe dados registrados para este funcionario");

                unitOfWork.FuncionarioRepository.Update(entity, funcionario);
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
