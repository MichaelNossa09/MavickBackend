using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MavickBackend.Models.Response;
using MavickBackend.Models.Request;
using MavickBackend.Models;

namespace MavickBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (MavickDBContext db = new MavickDBContext())
                {
                    var lst = db.Categories.ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Err = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpPost]
        public IActionResult Add(CategoryRequest oModel)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                using (var db = new MavickDBContext())
                {
                    var aux = db.Categories.Where(d => d.Name == oModel.Name).FirstOrDefault();
                    if(aux != null)
                    {
                        oRespuesta.Err = "Categoría existente.";
                        throw new Exception(oRespuesta.Err);
                    }

                    Category oCategory = new()
                    {
                        Name = oModel.Name
                    };
                    db.Categories.Add(oCategory);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = oCategory;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Err = ex.Message;
            }

            return Ok(oRespuesta);
        }

        [HttpPut]
        public IActionResult Edit(CategoryRequest oModel)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                using (MavickDBContext db = new MavickDBContext())
                {
                    Category oCategory = db.Categories.Find(oModel.Id);
                    oCategory.Name = oModel.Name;
                    db.Entry(oCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = oCategory;
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
                    Category oCategory = db.Categories.Find(Id);
                    db.Remove(oCategory);
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
