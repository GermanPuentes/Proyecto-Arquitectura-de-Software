using Microsoft.AspNetCore.Mvc;
using Publicador.Dtos;
using Publicador.Services;

namespace Publicador.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagoDeNomina : ControllerBase
    {
        private readonly PagoNominaServices _service;

        public PagoDeNomina(PagoNominaServices service)
        {
            _service = service;
        }
        [HttpPost()]
        public async Task<IActionResult> EnviarPago([FromBody] PagoNominaRequest request)
        {
            await _service.PublishAsync(request);

            return Ok(new { status = "Mensaje Enviado", request });
        }

    }
}
