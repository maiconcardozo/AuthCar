using AuthCar.Domain.Entities;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthCar.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly AppDbContext _context;

        public void Add(Usuario entity)
        {
            _context.Usuarios.Add(entity);
        }

        public Task AddAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            return Task.CompletedTask;
        }

        public void AddRange(IEnumerable<Usuario> lstEntity)
        {
            _context.Usuarios.AddRange(lstEntity);
        }

        public async Task DeleteAsync(Guid codigo)
        {
            var usuario = await _context.Usuarios.FindAsync(codigo);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
        }

        public IEnumerable<Usuario> Find(Expression<Func<Usuario, bool>> predicate)
        {
            return _context.Usuarios.Where(predicate).ToList();
        }

        public async Task<IEnumerable<Usuario>> FindAsync(Expression<Func<Usuario, bool>> predicate)
        {
            return await _context.Usuarios.Where(predicate).ToListAsync();
        }

        public Usuario? Get(Usuario entity)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == entity.Id);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public IEnumerable<Usuario> GetAllIncludingDeleted()
        {
            return _context.Usuarios.IgnoreQueryFilters().ToList();
        }

        public async Task<Usuario?> GetAsync(Usuario entity)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == entity.Id);
        }

        public async Task<Usuario?> GetByCodigoAsync(Guid codigo)
        {
            return await _context.Usuarios.FindAsync(codigo);
        }

        public Usuario? GetById(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> GetByLoginAsync(string login)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Login == login);
        }

        public IEnumerable<Usuario> GetByLstId(Usuario entity)
        {
            return new List<Usuario>();
        }

        public async Task<IEnumerable<Usuario>> GetByLstIdAsync(Usuario entity)
        {
            return await Task.FromResult(new List<Usuario>());
        }

        public void HardDelete(Usuario entity)
        {
            _context.Usuarios.Remove(entity);
        }

        public async Task<IEnumerable<Usuario>> ListAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public void Remove(Usuario entity)
        {
            _context.Usuarios.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Usuario> lstEntity)
        {
            _context.Usuarios.RemoveRange(lstEntity);
        }

        public Usuario? SingleOrDefault(Expression<Func<Usuario, bool>> predicate)
        {
            return _context.Usuarios.SingleOrDefault(predicate);
        }

        public async Task<Usuario?> SingleOrDefaultAsync(Expression<Func<Usuario, bool>> predicate)
        {
            return await _context.Usuarios.SingleOrDefaultAsync(predicate);
        }

        public void Update(Usuario entity)
        {
            _context.Usuarios.Update(entity);
        }

        public Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            return Task.CompletedTask;
        }
    }
}