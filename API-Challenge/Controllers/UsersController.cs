using API_Challenge.Modelos;
using API_Challenge.Modelos.DTO;
using API_Challenge.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        protected ResponseDto _response;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _response = new ResponseDto();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserDto userDto)
        {
            var respuesta = await _userRepository.RegisterUser(
                new User
                {
                    UserName = userDto.UserName
                },userDto.Password
                );
            if (respuesta == -1)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario ya existe";
                return BadRequest(Response);
            }

            if (respuesta == -500)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al crear usuario";
                return BadRequest(_response);
            }

                _response.DisplayMessage = "Usuario creado con exito";
                _response.Result = respuesta;
                return Ok(_response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserDto user)
        {
            var respuesta = await _userRepository.Login(user.UserName,user.Password);

            if (respuesta == "nouser") {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario no existe";
                return BadRequest(_response);
            }

            if (respuesta == "wrongpassword")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Password Incorrecto";
                return BadRequest(_response);
            }
            _response.Result=respuesta;
            _response.DisplayMessage = "Usuario conectado";
            return Ok(_response);
        }
    }
}
