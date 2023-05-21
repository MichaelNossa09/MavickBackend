using MavickBackend.Models;
using MavickBackend.Models.Request;
using MavickBackend.Models.Response;
using MavickBackend.Services;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MavickBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _product;

        public ProductController(IProductService product)
        {
            _product = product;
        }

        [HttpGet("{Id}")]
        public IActionResult GetProductById(int Id)
        {
            Respuesta res = new();
            try
            {
                res.Data = _product.GetProductById(Id);
                if (res.Data == null) return NotFound(new Respuesta { Exito=0, Err=$"Producto con id: {Id} no existe."});
                res.Exito = 1;
            }
            catch (Exception ex)
            {
                res.Err = ex.Message;
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet("categ/{cate}")]
        public IActionResult GetProductWithCategory(string cate)
        {
            var res = new Respuesta();
            try
            {
                res.Data = _product.GetProductWithCategory(cate);
                if (res.Data == null) return NotFound($"Categoría {cate} no existente");
                res.Exito = 1;    
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new Respuesta { Exito = 0, Err=$"{ex.Message}"});
            }
        }
        [HttpGet("size/{size}")]
        public IActionResult GetProductWithSize(string size)
        {
            var res = new Respuesta();
            try
            {
                res.Data = _product.GetProductWithSize(size);
                if (res.Data == null) return NotFound($"Size {size} no existente");
                res.Exito = 1;
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new Respuesta { Exito = 0, Err = $"{ex.Message}" });
            }
        }

        [HttpGet("categ/{cate}/size/{size}")]
        public IActionResult GetProductWithCategoryAndSize(string cate, string size)
        {
            Respuesta res = new();
            try
            {
                res.Data = _product.GetProductWithCategoryAndSize(cate, size);
                if (res.Data == null) return NotFound($"Categoría {cate} o Size {size} No existen.");
                res.Exito = 1;
            }
            catch (Exception ex)
            {
                res.Err = ex.Message;
                return BadRequest(res);
            }
            return Ok(res);
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
                oRespuesta.Data = _product.GetAll();
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
        public IActionResult Add(ProductRequest model)
        {
            Respuesta res = new()
            {
                Exito = 0
            };
            try
            {
                _product.Add(model);
                res.Exito = 1;
                res.Data = model;
            }
            catch (Exception ex)
            {
                res.Err = ex.Message;
                if(ex.Message == "Error")
                {
                    return BadRequest(res);
                }
                return NotFound(res);
            }

            return Ok(res);
        }

        [HttpPut]
            public IActionResult Update(ProductRequest model)
            {
                Respuesta res = new()
                {
                    Exito = 0
                };
                try
                {
                    res.Data = _product.Update(model);
                    res.Exito = 1;
                }
                catch (Exception ex)
                {
                    res.Err = ex.Message;
                    if (ex.Message == "Error")
                    {
                        return BadRequest(res);
                    }
                    return NotFound(res);
                }
                return Ok(res);
            }

        [HttpDelete("{Id}")]
        // [Authorize]
        public IActionResult Delete(int Id)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                oRespuesta.Data = _product.DeleteProduct(Id);
                if (oRespuesta.Data == null) return NotFound($"ProductoID: {Id} no existente.");
                oRespuesta.Exito = 1;
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
