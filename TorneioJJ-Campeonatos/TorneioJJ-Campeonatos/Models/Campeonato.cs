using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TorneioJJ_Campeonatos.Models
{
    public class Campeonato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Codigo { get; set; }

        public string Titulo { get; set; }

        public string Imagem { get; set; }

        public string CidadeEstado { get; set; }

        public DateTime DataRealizacao { get; set; }

        public string SobreEvento { get; set; }

        public string Ginasio { get; set; }

        public string InformacoesGerais { get; set; }

        public string EntradaPublico { get; set; }

        public string Destaque { get; set; }
        public string Tipo { get; set; }

        public string Fase { get; set; }

        public string Status { get; set; }
    }
}
