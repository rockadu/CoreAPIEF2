using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
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

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                 _repo.Add(model);

                 if(await _repo.SaveChangesAsync())
                 {
                     return Created($"/api/evento/{model.Id}", model);
                 }
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int EventoId, Evento model)
        {
            try
            {

                var evento = await _repo.GetEvetoAsyncById(EventoId, false);

                if(evento == null) return NotFound();

                 _repo.Update(model);

                 if(await _repo.SaveChangesAsync())
                 {
                     return Created($"/api/evento/{model.Id}", model);
                 }
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {

                var evento = await _repo.GetEvetoAsyncById(EventoId, false);

                if(evento == null) return NotFound();
                
                 _repo.Delete(evento);

                 if(await _repo.SaveChangesAsync())
                 {
                     return Ok();
                 }
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

            return BadRequest();
        }
    }
}