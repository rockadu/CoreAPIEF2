using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
        public EventoController(IProAgilRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                 var results = await _repo.GetAllEvetoAsync(true);

                 return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> GetById(int EventoId)
        {
            try
            {
                 var result = await _repo.GetEvetoAsyncById(EventoId, true);

                 return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
        }

        [HttpGet("getByTema/{Tema}")]
        public async Task<IActionResult> GetById(string Tema)
        {
            try
            {
                 var result = await _repo.GetAllEvetoAsyncByTema(Tema, true);

                 return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
        }
    }
}