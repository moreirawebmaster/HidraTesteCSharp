using System.Threading;
using System.Threading.Tasks;
using Bank.Domain.Lancamentos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
{

    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [Route("api/[controller]")]
    public class LancamentosController : ControllerBase
    {

        private readonly ILancamentoServiceWrite _serviceWrite;

        public LancamentosController(ILancamentoServiceWrite serviceWrite)
        {
            _serviceWrite = serviceWrite;
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> Post([FromBody] Domain.Lancamentos.Commands.CriarNovaOperacao.Request request)
        {
            var response = await _serviceWrite.CriarNovoLancamento(request, CancellationToken.None);
            return Ok(response);
        }

    }
}