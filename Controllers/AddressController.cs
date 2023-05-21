using MavickBackend.Models;
using MavickBackend.Models.Request;
using MavickBackend.Models.Response;
using MavickBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MavickBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {

        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpGet("{id}")]
        public IActionResult GetAddressById(long id)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                oRespuesta.Data = _addressService.GetAddress(id);
                if (oRespuesta.Data == null) return NotFound($"AddressID {id} no existe.");
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
        public IActionResult AddAddress(AddressRequest oModel)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                oRespuesta.Data = _addressService.AddAddress(oModel);
                if (oRespuesta.Data == null) return BadRequest("Address Exist.");
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Err = ex.Message;
                return BadRequest(oRespuesta);
            }
            return Ok(oRespuesta);
        }

        [HttpPut]
        // [Authorize]
        public IActionResult Edit(AddressRequest oModel)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                oRespuesta.Data = _addressService.EditAddress(oModel);
                if (oRespuesta.Data == null) return NotFound($"AddressID {oModel.Id} No Existe");
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Err = ex.Message;
                return BadRequest(oRespuesta);
            }
            return Ok(oRespuesta);
        }

        [HttpDelete("{Id}")]
        // [Authorize]
        public IActionResult Delete(long Id)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                oRespuesta.Data = _addressService.DeleteAddress(Id);
                if (oRespuesta.Data == null) return NotFound($"AddressID {Id} Not Exist");
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
