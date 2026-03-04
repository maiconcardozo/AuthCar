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

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByIdAsync(Guid id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario?> GetByLoginAsync(string login)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<IEnumerable<Usuario>> ListAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public Task AddAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var usuario = await GetByIdAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
        }

        public Usuario? Get(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario?> GetAsync(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> GetByLstId(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> GetByLstIdAsync(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> Find(Expression<Func<Usuario, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> FindAsync(Expression<Func<Usuario, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Usuario? SingleOrDefault(Expression<Func<Usuario, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario?> SingleOrDefaultAsync(Expression<Func<Usuario, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Usuario> lstEntity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Usuario> lstEntity)
        {
            throw new NotImplementedException();
        }

        public Usuario? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> GetAllIncludingDeleted()
        {
            throw new NotImplementedException();
        }

        public void HardDelete(Usuario entity)
        {
            throw new NotImplementedException();
        }
    }
}