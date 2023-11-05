using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TorneioJJ_Usuarios.Context;
using TorneioJJ_Usuarios.Models;
using TorneioJJ_Usuarios.Services;

namespace TorneioJJ_Usuarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly EsqueceuSenhaService _esqueceuSenhaService;

        public UsuariosController(UsuarioService usuarioService, EsqueceuSenhaService esqueceuSenhaService)
        {
            _usuarioService = usuarioService;
            _esqueceuSenhaService = esqueceuSenhaService;
        }

        [HttpPost]
        [Route("validar-login")]
        public IActionResult ValidarLogin([FromBody] UsuarioLogin usuarioLogin)
        {
            if(usuarioLogin.email == "admin@admin")
            {
                LoginResponse loginAdmin = new LoginResponse();
                loginAdmin.Id = 0;
                loginAdmin.Nome = "Admin";

                return Ok(loginAdmin);
            }

            // Verifica se o usuário com o email fornecido existe no banco de dados.
            var loginResponse = _usuarioService.Login(usuarioLogin.email, usuarioLogin.senha);

            if (loginResponse != null)
            {
                // Credenciais válidas
                return Ok(loginResponse); // Retorna o objeto LoginResponse
            }
            else
            {
                // Credenciais inválidas
                return Unauthorized();
            }
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

        [HttpPut]
        [Route("muda-senha")]
        public IActionResult MudaSenha([FromBody] UsuarioAlteraSenha usuarioAlteraSenha)
        {
            if(usuarioAlteraSenha.Id == 0)
            {
                return Ok(new { message = "Admin não muda senha." });
            }

            var sucesso = _usuarioService.AlteraSenha(usuarioAlteraSenha.Id, usuarioAlteraSenha.Senha_Antiga, usuarioAlteraSenha.Senha_Nova);

            if (sucesso)
            {
                return Ok(new { message = "Senha alterada com sucesso." });
            }
            else
            {
                return BadRequest(new { message = "Não foi possível alterar a senha. Verifique as informações fornecidas." });
            }
        }

        [HttpPost]
        [Route("esqueceu-senha")]
        public IActionResult EsqueceuSenha([FromBody] EsqueceuSenhaRequest request)
        {
            bool resultado = _esqueceuSenhaService.EnviarEmailRedefinicaoSenha(request.Email, request.id);
            if (resultado)
            {
                return Ok(new { message = "E-mail de redefinição de senha enviado com sucesso." });
            }
            else
            {
                return BadRequest(new { message = "Erro ao enviar o email!" });
            }
        }

        [HttpPost]
        [Route("redefine-senha")]
        public IActionResult RedefineSenha([FromBody] RedefineSenha request)
        {
            // Verifique se a solicitação é válida, por exemplo, se a nova senha é segura o suficiente.

            if (string.IsNullOrEmpty(request.senha))
            {
                return BadRequest(new { message = "A nova senha não pode estar em branco." });
            }

         
            int id = _esqueceuSenhaService.RecuperaId(request.Token);

            var sucesso = _usuarioService.RedefineSenha(id, request.senha);

            if (sucesso)
            {
                return Ok(new { message = "Senha redefinida com sucesso." });
            }
            else
            {
                return BadRequest(new { message = "Erro ao redefinir a senha." });
            }
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
