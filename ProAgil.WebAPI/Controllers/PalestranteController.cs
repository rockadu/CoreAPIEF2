using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    [Route("Controller")]
    public class PalestranteController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
        public PalestranteController(IProAgilRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                 var results = await _repo.GetAllPalestrantesAsyncByName(name, true);

                 return Ok(results);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar o Banco");
            }
        }
    }
}