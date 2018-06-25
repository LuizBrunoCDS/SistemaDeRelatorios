using Domain.Entities;
using Domain.Interfaces;
using Domain.Helpers;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class UsuarioController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public UsuarioController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [Route("Usuarios/Login")]
        [HttpPost]
        public HttpResponseMessage Logar(Usuario user)
        {
            try
            {
                var senha = Encryption.GenerateMD5Hash(user.Senha);

                var usuario = unitOfWork.UsuarioRepository.SelectByParam(filter: a => a.Login == user.Login);

                if (usuario == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Usuario invalido");
                else if (usuario.Senha != senha)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Senha inválida");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, "Logado");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
