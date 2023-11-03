using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TorneioJJ_Usuarios.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string usuario { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string perfil { get; set; }
        public string status { get; set; }
        public DateTime data_cadastro { get; set; }

        public Usuario()
        {
        }

        public Usuario(string usuario, string email, string senha, string perfil, string status, DateTime data_cadastro)
        {
            this.usuario = usuario;
            this.email = email;
            this.senha = senha;
            this.perfil = perfil;
            this.status = status;
            this.data_cadastro = data_cadastro;
        }
    }
}
