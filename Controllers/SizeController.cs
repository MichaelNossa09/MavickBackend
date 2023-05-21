using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MavickBackend.Models.Request;
using MavickBackend.Models.Response;
using MavickBackend.Models;
using MavickBackend.Services;

namespace MavickBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _size;

        public SizeController(ISizeService size)
        {
            _size = size;
        }
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                oRespuesta.Data = _size.Get();
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Err = ex.Message;
                return BadRequest(oRespuesta);
            }
            return Ok(oRespuesta);
        }

        [HttpPost]
        public IActionResult Add(SizeRequest oModel)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                using (MavickDBContext db = new MavickDBContext())
                {
                    var aux = db.Sizes.Where(d => d.Name == oModel.Name).FirstOrDefault();
                    if(aux != null)
                    {
                        oRespuesta.Err = "Talla Existente.";
                        throw new Exception(oRespuesta.Err);
                    }

                    Size oSize = new()
                    {
                        Name = oModel.Name
                    };
                    db.Sizes.Add(oSize);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = oSize;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Err = ex.Message;
            }

            return Ok(oRespuesta);
        }

        [HttpPut]
        public IActionResult Edit(SizeRequest oModel)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                using (MavickDBContext db = new MavickDBContext())
                {
                    Size oSize = db.Sizes.Find(oModel.Id);
                    oSize.Name = oModel.Name;
                    db.Entry(oSize).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = oSize;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Err = ex.Message;
            }

            return Ok(oRespuesta);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                using (MavickDBContext db = new MavickDBContext())
                {
                    Size oSize = db.Sizes.Find(Id);
                    db.Remove(oSize);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Err = ex.Message;
            }

            return Ok(oRespuesta);
        }
    }
}
