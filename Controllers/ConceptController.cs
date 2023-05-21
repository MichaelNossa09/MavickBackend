using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MavickBackend.Models.Response;
using MavickBackend.Models;

namespace MavickBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                using var db = new MavickDBContext();
                var lst = db.Concepts.ToList();
                oRespuesta.Exito = 1;
                oRespuesta.Data = lst;
            }
            catch (Exception ex)
            {
                oRespuesta.Err = ex.Message;
                return BadRequest(oRespuesta);
            }
            return Ok(oRespuesta);
        }

        [HttpGet("{id}")]
        public IActionResult GetBySaleID(int id)
        {
            Respuesta oRespuesta = new ()
            {
                Exito = 0
            };
            try
            {
                using var db = new MavickDBContext();
                var lst = db.Concepts.ToList().Where(d => d.IdSale == id);
                if(lst.Any() == false)
                {
                    oRespuesta.Err = "Sale no encontrada";
                    return NotFound(oRespuesta);
                }
                oRespuesta.Exito = 1;
                oRespuesta.Data = lst;
            }
            catch (Exception ex)
            {
                oRespuesta.Err = ex.Message;
                return BadRequest(oRespuesta);
            }
            return Ok(oRespuesta);
        }

    }

}
