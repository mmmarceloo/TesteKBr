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
            usuario.data_cadastro = DateTime.Now;
            _usuarioService.SalvarUsuario(usuario);
            return Ok(usuario);
        }

        [HttpPut]
        [Route("atualizar")]
        public IActionResult AtualizarUsuario([FromBody] Usuario usuario)
        {
            var usuarioExistente = _usuarioService.ObterUsuarioPorId(usuario.Id);

            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado");
            }

            //Atualização dos campos
            usuarioExistente.usuario = usuario.usuario;
            usuarioExistente.email = usuario.email;
            usuarioExistente.perfil = usuario.perfil;
            usuarioExistente.status = usuario.status;

            if (usuario.senha != "" || usuario.senha != null)
            {
                usuarioExistente.senha = usuario.senha;
            }

            _usuarioService.AtualizarUsuario(usuarioExistente);

            return Ok(usuarioExistente);
        }

        [HttpDelete]
        [Route("excluir/{id}")]
        public IActionResult ExcluirUsuario(int id)
        {
            var usuario = _usuarioService.ObterUsuarioPorId(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _usuarioService.ExcluirUsuario(usuario);

            return Ok("Usuário excluído com sucesso.");
        }


        [HttpGet]
        [Route("todos")]
        public IActionResult ObterTodosUsuarios(int pagina, int itensPorPagina)
        {
            if(pagina ==0 )
            {
                pagina = 1;
            }
            if(itensPorPagina ==0 )
            {
                itensPorPagina = 10;
            }
            // Serviço para buscar todos os usuários
            var usuarios = _usuarioService.ObterUsuariosPaginados(pagina, itensPorPagina);
            var totalUsuarios = _usuarioService.ContarTotalUsuarios();

            if (usuarios != null && usuarios.Any())
            {
                var resposta = new
                {
                    TotalUsuarios = totalUsuarios,
                    Usuarios = usuarios
                };
                return Ok(resposta);
            }
            else
            {
                return NotFound("Nenhum usuário encontrado.");
            }
        }
        [HttpGet]
        [Route("filtrar")]
        public IActionResult ObterUsuariosFiltrados(string? search, string? status, DateTime? de, DateTime? ate)
        {

            var usuariosFiltrados = _usuarioService.ObterUsuariosFiltrados(search, status, de, ate);

            if (usuariosFiltrados != null && usuariosFiltrados.Any())
            {
                return Ok(usuariosFiltrados);
            }
            else
            {
                return NotFound("Nenhum usuário encontrado com os filtros especificados.");
            }
        }
    }
}
