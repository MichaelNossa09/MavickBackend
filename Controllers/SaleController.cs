using MavickBackend.Models;
using MavickBackend.Models.Request;
using MavickBackend.Models.Response;
using MavickBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MavickBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _sale;

        public SaleController(ISaleService sale)
        {
            _sale = sale;
        }

        [HttpGet]
        public IActionResult GetSales()
        {
            var res = new Respuesta
            {
                Exito = 0
            };

            try
            {
                using var db = new MavickDBContext();
                var lst = db.Sales.ToList();
                res.Exito = 1;
                res.Data = lst;
            }
            catch(Exception ex)
            {
                res.Err = ex.Message;
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult GetSalesId(int id)
        {
            var res = new Respuesta
            {
                Exito = 0
            };

            try
            {
                using var db = new MavickDBContext();
                var sale = db.Sales.Find(id);
                if( sale == null)
                {
                    return NotFound(res);
                }
                res.Exito = 1;
                res.Data = sale;
            }
            catch (Exception ex)
            {
                res.Err = ex.Message;
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet("userId/{id}")]
        public IActionResult GetSalesUserId(long id)
        {
            var res = new Respuesta
            {
                Exito = 0
            };

            try
            {
                using var db = new MavickDBContext();
                var user = db.Users.Find(id);
                if (user == null) return NotFound($"Usuario {id} no válido");
                var lst = db.Sales.ToList().Where(d => d.IdUser == id);
                res.Exito = 1;
                res.Data = lst;
            }
            catch (Exception ex)
            {
                res.Err = ex.Message;
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Add(SaleRequest model)
        {
            var res = new Respuesta
            {
                Exito = 0
            };
            try
            {
                _sale.Add(model);
                res.Exito = 1;
            }
            catch (Exception ex)
            {
                res.Err = ex.Message;
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = new Respuesta
            {
                Exito = 0
            };

            try
            {
                using var db = new MavickDBContext();
                var sale = db.Sales.Find(id);
                if(sale == null)
                {
                    return NotFound(res);
                }
                var lst = db.Concepts.ToList().Where(d=> d.IdSale == id);
                foreach (var d in lst)
                {
                    db.Concepts.Remove(d);
                }
                db.Sales.Remove(sale);
                db.SaveChanges();
                res.Exito = 1;
                res.Data = sale;
            }
            catch (Exception ex)
            {
                res.Err = ex.Message;
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
