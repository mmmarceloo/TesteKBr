using System.ComponentModel.DataAnnotations;

namespace TorneioJJ_Usuarios.Models
{
    public class UsuarioLogin
    {
        public string email { get; set; }
        public string senha { get; set; }
    }
}
