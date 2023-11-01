using Microsoft.EntityFrameworkCore;
using TorneioJJ_Usuarios.Context;
using TorneioJJ_Usuarios.Models;

public class UsuarioService
{
    private readonly MyDbContext _context;

    public UsuarioService(MyDbContext context)
    {
        _context = context;
    }

    public void SalvarUsuario(Usuario usuario)
    {
        // Adiciona a entidade Usuario ao contexto e, em seguida, chama SaveChanges para efetuar a inserção no banco de dados.
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public IEnumerable<Usuario> ObterTodosUsuarios()
    {
        return _context.Usuarios.ToList();
    }
}
