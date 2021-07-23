using System;
using System.Net;
using System.Web.Http;
using TesteDotz.Core;

namespace TesteDotz.API
{
    [ApiPrefix("login", Version = "1")]
    public class LoginController
    {
        #region DEPENDENCIAS

        //internal ILogarCliente LogarCliente
        //{
        //    get { return _logarCliente ?? (_logarCliente = ServiceLocator.Resolve<ILogarCliente>()); }
        //    set { _logarCliente = value; }
        //}
        //ILogarCliente _logarCliente;

        #endregion

        //[HttpPost]
        //[Route(Name = "Api.Login.Realizar")]
        //public IHttpActionResult Realizar([FromBody] RequisicaoLoginDTO loginModel)
        //{
        //    loginModel.Senha = loginModel.Senha.Criptografar();
        //    var resultado = LogarCliente.Entrar(loginModel);

        //    if (resultado.ContemErros || resultado.RecuperarSenha)
        //        return BadRequest(resultado);

        //    return Ok(resultado.Cliente);
        //}
    }
}
