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

    public Usuario ObterUsuarioPorId(int id)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Id == id);
    }

    public void AtualizarUsuario(Usuario usuario)
    {
        _context.Entry(usuario).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public IEnumerable<Usuario> ObterTodosUsuarios()
    {
        return _context.Usuarios.ToList();
    }

    public int ContarTotalUsuarios()
    {
        return _context.Usuarios.Count();
    }

    public IEnumerable<Usuario> ObterUsuariosPaginados(int pagina, int itensPorPagina)
    {
        return _context.Usuarios
            .Skip((pagina - 1) * itensPorPagina)
            .Take(itensPorPagina)
            .ToList();
    }
    public void ExcluirUsuario(Usuario usuario)
    {
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Usuario> ObterUsuariosFiltrados(string search, string status, DateTime? de, DateTime? ate)
    {
        var query = _context.Usuarios.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(u => u.usuario.Contains(search) || u.email.Contains(search));
        }

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(u => u.status == status);
        }

        if (de.HasValue)
        {
            query = query.Where(u => u.data_cadastro >= de.Value);
        }

        if (ate.HasValue)
        {
            query = query.Where(u => u.data_cadastro <= ate.Value);
        }

        return query.ToList();
    }

}
