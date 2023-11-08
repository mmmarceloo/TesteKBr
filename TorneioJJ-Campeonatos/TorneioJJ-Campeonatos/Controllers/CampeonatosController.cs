using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using TorneioJJ_Campeonatos.Models;
using TorneioJJ_Campeonatos.Services;
//using TorneioJJ_Campeonatos.Services;

namespace TorneioJJ_Campeonatos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampeonatosController : Controller
    {
        private readonly CampeonatoService _campeonatoService;

        public CampeonatosController(CampeonatoService campeonatoService)
        {
            _campeonatoService = campeonatoService;
        }

        [HttpPost]
        [Route("cadastrar-campeonato")]
        public IActionResult CadastrarCampeonato([FromBody] JsonElement novoCampeonato)
        {
            Campeonato campeonato = JsonSerializer.Deserialize<Campeonato>(novoCampeonato.GetRawText());
            bool resposta = _campeonatoService.VerificaDestaques(campeonato);
            if(resposta)
            {
                return BadRequest("Número de campeonatos em destaque excedido");
            }
            string pathFoto = campeonato.Imagem;
            campeonato.Imagem = pathFoto;
            // Verificar se o campeonato é duplicado
            if (_campeonatoService.CampeonatoDuplicado(campeonato))
            {
                return BadRequest("Campeonato duplicado. Já existe um campeonato com as mesmas propriedades.");
            }

            _campeonatoService.SalvarCampeonato(campeonato);
            return Ok("Campeonato cadastrado com sucesso.");
        }

        [HttpPost]
        [Route("cadastrar-campeonato-foto")]
        public IActionResult CadastrarFoto()
        {
            try
            {
                var file = Request.Form.Files[0]; // Obtenha o arquivo de imagem
                var result = _campeonatoService.SalvarFoto(file);

                if (result.Success)
                {
                    return Ok(result.Message); // Retorna uma mensagem de sucesso (código 200 OK)
                }
                else
                {
                    return BadRequest(result.Message); // Retorna uma mensagem de erro (código 400 Bad Request)
                }
            }
            catch (Exception e)
            {

                throw;
            }

        }

        [HttpGet]
        [Route("todos")]
        public IActionResult ObterTodosCampeonatos(int pagina, int itensPorPagina)
        {
            if (pagina == 0)
            {
                pagina = 1;
            }
            if (itensPorPagina == 0)
            {
                itensPorPagina = 10;
            }
            // buscar todos os campeonatos
            var campeonatos = _campeonatoService.ObterCampeonatosPaginados(pagina, itensPorPagina);
            var totalCampeonatos = _campeonatoService.ContarTotalCampeonatoss();

            if (campeonatos != null && campeonatos.Any())
            {
                var resposta = new
                {
                    TotalCampeonatos = totalCampeonatos,
                    Campeonatos = campeonatos
                };
                return Ok(resposta);
            }
            else
            {
                return NotFound("Nenhum campeonato encontrado.");
            }
        }
        [HttpDelete]
        [Route("excluir/{id}")]
        public IActionResult ExcluirCampeonato(int id)
        {
            var campeonato = _campeonatoService.ObterCampeonatoPorId(id);

            if (campeonato == null)
            {
                return NotFound("Campeonato não encontrado.");
            }

            _campeonatoService.ExcluirCampeonato(campeonato);

            return Ok("Usuário excluído com sucesso.");
        }

        [HttpGet]
        [Route("buscar-campeonato/{id}")]
        public IActionResult BuscarCampeonato(int id)
        {
            var campeonato = _campeonatoService.ObterCampeonatoPorId(id);

            if (campeonato == null)
            {
                return NotFound("Campeonato não encontrado.");
            }

            return Ok(campeonato);
        }

        [HttpGet]
        [Route("filtrar")]
        public IActionResult ObterCampeonatosFiltrados(string? destaque, string? status, string? fase, DateTime? de, DateTime? ate)
        {

            var campeonatosFiltrados = _campeonatoService.ObterCampeonatosFiltrados(destaque, status,fase, de, ate);
            var campeonatosFiltradosList = campeonatosFiltrados.ToList();
            if (campeonatosFiltradosList != null && campeonatosFiltradosList.Any())
            {
                return Ok(campeonatosFiltrados);
            }
            else
            {
                return NotFound("Nenhum usuário encontrado com os filtros especificados.");
            }
        }

        [HttpGet]
        [Route("exibeImagem")]
        public IActionResult ExibeImagem(string arquivo)
            {
            string pathFoto = "Fotos\\" + arquivo;
            try
            {
                if (System.IO.File.Exists(pathFoto))
                {
                    Byte[] foto = System.IO.File.ReadAllBytes(pathFoto);
                    return File(foto, "image/*");
                }
                else
                {
                    // Lida com o caso em que o arquivo de imagem não foi encontrado
                    return NotFound();
                }
            }
            catch (Exception e)
            {

                throw;
            }

        }

        [HttpPut]
        [Route("atualizar")]
        public IActionResult AtualizarCampeonato([FromBody] Campeonato campeonato)
        {
            var campeonatoExistente = _campeonatoService.ObterCampeonatoPorId(campeonato.Id);
            
            if (campeonato == null)
            {
                return NotFound("Campeonato não encontrado");
            }

            bool resposta = _campeonatoService.VerificaDestaques(campeonato);
            if(resposta)
            {
                return BadRequest("Número de campeonatos em destaque excedido");
            }
            //Atualização dos campos
            campeonatoExistente.Codigo = campeonato.Codigo;
            campeonatoExistente.Titulo = campeonato.Titulo;
            campeonatoExistente.CidadeEstado = campeonato.CidadeEstado;
            campeonatoExistente.DataRealizacao = campeonato.DataRealizacao;
            campeonatoExistente.SobreEvento = campeonato.SobreEvento;
            campeonatoExistente.Ginasio = campeonato.Ginasio;
            campeonatoExistente.InformacoesGerais = campeonato.InformacoesGerais;
            campeonatoExistente.EntradaPublico = campeonato.EntradaPublico;
            campeonatoExistente.Destaque = campeonato.Destaque;
            campeonatoExistente.Tipo = campeonato.Tipo;
            campeonatoExistente.Fase = campeonato.Fase;
            campeonatoExistente.Status = campeonato.Status;

            _campeonatoService.AtualizarCampeonato(campeonatoExistente);

            return Ok("Campeonato atualizado!");
        }
    }
}
