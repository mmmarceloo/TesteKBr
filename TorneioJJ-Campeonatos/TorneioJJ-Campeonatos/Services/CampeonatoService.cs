using Microsoft.EntityFrameworkCore;
using TorneioJJ_Campeonatos.Context;
using TorneioJJ_Campeonatos.Models;
using System.IO;
using System;
using Microsoft.AspNetCore.Mvc;

namespace TorneioJJ_Campeonatos.Services
{
    public class CampeonatoService
    {
        private readonly CampeonatoDbContext _context;

        public CampeonatoService(CampeonatoDbContext context)
        {
            _context = context;
        }

        public bool CampeonatoDuplicado(Campeonato novoCampeonato)
        {
            return _context.Campeonatos.Any(c =>
                c.Codigo == novoCampeonato.Codigo ||
                c.Titulo == novoCampeonato.Titulo &&
                c.CidadeEstado == novoCampeonato.CidadeEstado &&
                c.Ginasio == novoCampeonato.Ginasio &&
                c.DataRealizacao == novoCampeonato.DataRealizacao
            );
        }

        public void SalvarCampeonato(Campeonato novoCampeonato)
        {
            // Prossiga com a criação do novo campeonato
            _context.Campeonatos.Add(novoCampeonato);
            _context.SaveChanges();
        }
        public ServiceResult SalvarFoto(IFormFile file)
        {
            ServiceResult result = new ServiceResult();

            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Fotos", fileName);
                path = path.Replace("//", "\\");

                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    result.Success = true;
                    result.Message = "Arquivo salvo com sucesso";
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = "Erro ao salvar o arquivo: " + ex.Message;
                }
            }
            else
            {
                result.Success = false;
                result.Message = "Nenhum arquivo foi enviado";
            }

            return result;
        }
        public Campeonato ObterCampeonatoPorId(int id)
        {
            return _context.Campeonatos.FirstOrDefault(u => u.Id == id);
        }
        public void AtualizarCampeonato(Campeonato campeonato)
        {
            try
            {
                _context.Entry(campeonato).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public bool VerificaDestaques(Campeonato campeonato)
        {
            // Verifica se já existem 8 campeonatos com "destaque" igual a true
            int campeonatosDestaqueCount = _context.Campeonatos.Count(c => c.Destaque == "true");

            if (campeonatosDestaqueCount >= 8)
            {
                return true;
            }

                return false;
        }

        public void ExcluirCampeonato(Campeonato campeonato)
        {
            if (campeonato != null)
            {
                _context.Campeonatos.Remove(campeonato);
                _context.SaveChanges();
            }
        }

        public int ContarTotalCampeonatoss()
        {
            return _context.Campeonatos.Count();
        }

        public IEnumerable<Campeonato> ObterCampeonatosPaginados(int pagina, int itensPorPagina)
        {
            return _context.Campeonatos
                .Skip((pagina - 1) * itensPorPagina)
                .Take(itensPorPagina)
                .ToList();
        }

        public IEnumerable<Campeonato> ObterCampeonatosFiltrados(string? destaque, string? status, string? fase, DateTime? de, DateTime? ate)
        {
            var query = _context.Campeonatos.AsQueryable();

            if (!string.IsNullOrEmpty(destaque))
            {
                query = query.Where(u => u.Destaque.Contains(destaque));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(u => u.Status == status);
            }


            if (!string.IsNullOrEmpty(fase))
            {
                query = query.Where(u => u.Fase == fase);
            }

            if (de.HasValue)
            {
                query = query.Where(u => u.DataRealizacao >= de.Value);
            }

            if (ate.HasValue)
            {
                query = query.Where(u => u.DataRealizacao <= ate.Value);
            }

            return query;
        }

        

    }
}
