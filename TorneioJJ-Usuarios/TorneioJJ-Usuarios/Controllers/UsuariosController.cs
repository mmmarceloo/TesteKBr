using Microsoft.AspNetCore.Mvc;
using TorneioJJ_Usuarios.Context;
using TorneioJJ_Usuarios.Models;

namespace TorneioJJ_Usuarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("salvar")]
        public IActionResult SalvarUsuario([FromBody] Usuario usuario)
        {
            _usuarioService.SalvarUsuario(usuario);
            return Ok(usuario);
        }

        [HttpGet]
        [Route("todos")]
        public IActionResult ObterTodosUsuarios()
        {
            // Serviço para buscar todos os usuários
            var usuarios = _usuarioService.ObterTodosUsuarios();

            if (usuarios != null && usuarios.Any())
            {
                return Ok(usuarios);
            }
            else
            {
                return NotFound("Nenhum usuário encontrado.");
            }
        }
    }
}
