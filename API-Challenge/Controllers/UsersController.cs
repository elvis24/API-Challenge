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
                }, userDto.Password
                );
            if (respuesta == "existe")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario ya existe";
                return BadRequest(Response);
            }

            if (respuesta == "error")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al crear usuario";
                return BadRequest(_response);
            }

            _response.DisplayMessage = "Usuario creado con exito";
            //_response.Result = respuesta;
            JwTPackage jpt = new JwTPackage();
            jpt.UserName = userDto.UserName;
            jpt.Token = respuesta;
            _response.Result = jpt;
            return Ok(_response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserDto user)
        {
            var respuesta = await _userRepository.Login(user.UserName, user.Password);

            if (respuesta == "nouser")
            {
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
            //_response.Result = respuesta;
            JwTPackage jpt = new JwTPackage();
            jpt.UserName = user.UserName;
            jpt.Token = respuesta;
            _response.Result = jpt;

            _response.DisplayMessage = "Usuario conectado";
            return Ok(_response);
        }
    }
    public class JwTPackage
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
