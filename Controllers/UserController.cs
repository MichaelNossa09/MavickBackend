using MavickBackend.Models;
using MavickBackend.Models.Request;
using MavickBackend.Models.Response;
using MavickBackend.Services;
using MavickBackend.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MavickBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        //  [Authorize]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                oRespuesta.Data = _userService.Get();
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Err = ex.Message;
                return BadRequest(oRespuesta);
            }
            return Ok(oRespuesta);
        }

        [HttpGet("{Email}")]
        //  [Authorize]
        public IActionResult GetByEmail(string Email)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                var user = _userService.GetByEmail(Email);
                if (user == null)
                {
                    oRespuesta.Err = "Usuario no encontrado.";
                    return NotFound(oRespuesta);
                }
                oRespuesta.Exito = 1;
                oRespuesta.Data = user;
            }
            catch (Exception ex)
            {
                oRespuesta.Err = ex.Message;
                return BadRequest(oRespuesta);
            }
            return Ok(oRespuesta);
        }

        [HttpPost("login")]
        public IActionResult Auth([FromBody] AuthRequest model)
        {
            Respuesta respuesta = new();
            var userResponse = _userService.Auth(model);
            if (userResponse == null)
            {
                respuesta.Exito = 0;
                respuesta.Err = "Usuario o contraseña incorrecta";
                return BadRequest(respuesta);
            }
            respuesta.Exito = 1;
            respuesta.Data = userResponse;
            return Ok(respuesta);
        }


        [HttpPost("register")]
        public IActionResult Add(UserRequets oModel)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                var user = _userService.AddUser(oModel);
                if (user == null)
                {
                    oRespuesta.Err = "Correo o Teléfono Existentes";
                    return BadRequest(oRespuesta);
                }
                oRespuesta.Exito = 1;
                oRespuesta.Data = user;
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
        public IActionResult Edit(UserRequets oModel)
        {
            Respuesta oRespuesta = new()
            {
                Exito = 0
            };
            try
            {
                oRespuesta.Data = _userService.UpdateUser(oModel);
                if(oRespuesta.Data == null)
                {
                    oRespuesta.Err = "Usuario no válido";
                    return NotFound(oRespuesta);
                }
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
                oRespuesta.Data = _userService.DeleteUser(Id);
                if(oRespuesta.Data == null)
                {
                    oRespuesta.Err = "Usuario no válido";
                    return NotFound(oRespuesta);
                }
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
