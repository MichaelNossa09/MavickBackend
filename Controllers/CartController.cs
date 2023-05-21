using MavickBackend.Models.Response;
using MavickBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MavickBackend.Models.Request;
using MavickBackend.Services;

namespace MavickBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                oRespuesta.Data = _cartService.GetCartById(Id);
                if (oRespuesta.Data == null) return NotFound($"CartID {Id} Not Exist");
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Err = ex.Message;
                return BadRequest(oRespuesta);
            }
            return Ok(oRespuesta);
        }

        [HttpGet("userID/{Id}")]
        public IActionResult GetAllCartsUser(long Id)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                oRespuesta.Data = _cartService.GetAllCartsUser(Id);
                if (oRespuesta.Data == null) return NotFound($"UserID {Id} Not Exist");
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
        public IActionResult Add(CartRequest oModel)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };

            try
            {
                oRespuesta.Data = _cartService.AddCart(oModel);
                if (oRespuesta.Data == null) return NotFound($"ProductoID {oModel.IdProduct} Not Exist.");
                oRespuesta.Exito = 1;
            }
            catch (Exception ex) {
                oRespuesta.Err = ex.Message;
                return BadRequest(oRespuesta);
            }
            return Ok(oRespuesta);
        }

        [HttpPut]
        public IActionResult Edit(CartRequest model)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };

            try
            {
                oRespuesta.Data = _cartService.UpdateCart(model);
                if (oRespuesta.Data == null) return NotFound($"Carrito No Existente.");
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Err = ex.Message;
                return BadRequest(oRespuesta);
            }
            return Ok(oRespuesta);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };

            try
            {
                oRespuesta.Data = _cartService.DeleteCart(id);
                if (oRespuesta.Data == null) return NotFound($"CartId {id} Not Exist.");
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Err = ex.Message;
                return BadRequest(oRespuesta);
            }
            return Ok(oRespuesta);
        }
        [HttpDelete("userID/{id}")]
        public IActionResult DeleteAll(long id)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };

            try
            {
                oRespuesta.Data = _cartService.DeleteAll(id);
                if (oRespuesta.Data == null) return NotFound($"UserID {id} Not Exist.");
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
