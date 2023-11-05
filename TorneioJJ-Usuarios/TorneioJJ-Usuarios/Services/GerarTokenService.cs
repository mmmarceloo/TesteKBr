using Jose;
using System.IdentityModel.Tokens.Jwt;
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

        public int DecodeJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Recupere a carga útil (payload) do token.
            var payload = jwtToken.Payload;

            if (payload.TryGetValue("userId", out var userId))
            {
                int userIdValue = int.Parse(userId.ToString());
                return userIdValue;
            }

            return 0;
        }
    }
}
