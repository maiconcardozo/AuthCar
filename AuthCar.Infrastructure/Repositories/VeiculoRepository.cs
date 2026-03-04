using AuthCar.Domain.Entities;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthCar.Infrastructure.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly AppDbContext _context;

        public VeiculoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Veiculo?> GetByIdAsync(Guid id)
        {
            return await _context.Veiculos.FindAsync(id);
        }

        public async Task<IEnumerable<Veiculo>> ListAsync()
        {
            return await _context.Veiculos.ToListAsync();
        }

        public Task AddAsync(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Veiculo veiculo)
        {
            _context.Veiculos.Update(veiculo);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var veiculo = await GetByIdAsync(id);
            if (veiculo != null)
            {
                _context.Veiculos.Remove(veiculo);
            }
        }

        public Veiculo? Get(Veiculo entity)
        {
            throw new NotImplementedException();
        }

        public Task<Veiculo?> GetAsync(Veiculo entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Veiculo> GetByLstId(Veiculo entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Veiculo>> GetByLstIdAsync(Veiculo entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Veiculo> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Veiculo>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Veiculo> Find(Expression<Func<Veiculo, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Veiculo>> FindAsync(Expression<Func<Veiculo, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Veiculo? SingleOrDefault(Expression<Func<Veiculo, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Veiculo?> SingleOrDefaultAsync(Expression<Func<Veiculo, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(Veiculo entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Veiculo> lstEntity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Veiculo entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Veiculo> lstEntity)
        {
            throw new NotImplementedException();
        }

        public Veiculo? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Veiculo?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Veiculo entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Veiculo> GetAllIncludingDeleted()
        {
            throw new NotImplementedException();
        }

        public void HardDelete(Veiculo entity)
        {
            throw new NotImplementedException();
        }
    }
}