using Jose;
using System.Text;

namespace TorneioJJ_Usuarios.Services
{
    public class GerarTokenService
    {
        public string GenerateResetPasswordToken(int id)
        {
            var key = Encoding.UTF8.GetBytes("SeuSegredoCompartilhado"); // Mantenha isso em segredo!
            var payload = new
            {
                UserId = id,
                Expiry = DateTime.UtcNow.AddHours(2) // Define um prazo de validade de 2 horas.
            };

            var token = JWT.Encode(payload, key, JwsAlgorithm.HS256);

            return token;
        }
    }
}
