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

        public void Add(Veiculo entity)
        {
            _context.Veiculos.Add(entity);
        }

        public Task AddAsync(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
            return Task.CompletedTask;
        }

        public void AddRange(IEnumerable<Veiculo> lstEntity)
        {
            _context.Veiculos.AddRange(lstEntity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo != null)
            {
                _context.Veiculos.Remove(veiculo);
            }
        }

        public IEnumerable<Veiculo> Find(Expression<Func<Veiculo, bool>> predicate)
        {
            return _context.Veiculos.Where(predicate).ToList();
        }

        public async Task<IEnumerable<Veiculo>> FindAsync(Expression<Func<Veiculo, bool>> predicate)
        {
            return await _context.Veiculos.Where(predicate).ToListAsync();
        }

        public Veiculo? Get(Veiculo entity)
        {
            return _context.Veiculos.FirstOrDefault(v => v.Id == entity.Id);
        }

        public IEnumerable<Veiculo> GetAll()
        {
            return _context.Veiculos.ToList();
        }

        public async Task<IEnumerable<Veiculo>> GetAllAsync()
        {
            return await _context.Veiculos.ToListAsync();
        }

        public IEnumerable<Veiculo> GetAllIncludingDeleted()
        {
            return _context.Veiculos.IgnoreQueryFilters().ToList();
        }

        public async Task<Veiculo?> GetAsync(Veiculo entity)
        {
            return await _context.Veiculos.FirstOrDefaultAsync(v => v.Id == entity.Id);
        }

        public Task<Veiculo?> GetByCodigoAsync(Guid codigo)
        {
            return _context.Veiculos.FirstOrDefaultAsync(v => v.Codigo == codigo);
        }

        public Veiculo? GetById(int id)
        {
            return _context.Veiculos.FirstOrDefault(v => v.Id == id);
        }

        public async Task<Veiculo?> GetByIdAsync(Guid id)
        {
            return await _context.Veiculos.FindAsync(id);
        }

        public async Task<Veiculo?> GetByIdAsync(int id)
        {
            return await _context.Veiculos.FirstOrDefaultAsync(v => v.Id == id);
        }

        public IEnumerable<Veiculo> GetByLstId(Veiculo entity)
        {
            return new List<Veiculo>();
        }

        public async Task<IEnumerable<Veiculo>> GetByLstIdAsync(Veiculo entity)
        {
            return await Task.FromResult(new List<Veiculo>());
        }

        public void HardDelete(Veiculo entity)
        {
            _context.Veiculos.Remove(entity);
        }

        public async Task<IEnumerable<Veiculo>> ListAsync()
        {
            return await _context.Veiculos.ToListAsync();
        }

        public void Remove(Veiculo entity)
        {
            _context.Veiculos.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Veiculo> lstEntity)
        {
            _context.Veiculos.RemoveRange(lstEntity);
        }

        public Veiculo? SingleOrDefault(Expression<Func<Veiculo, bool>> predicate)
        {
            return _context.Veiculos.SingleOrDefault(predicate);
        }

        public async Task<Veiculo?> SingleOrDefaultAsync(Expression<Func<Veiculo, bool>> predicate)
        {
            return await _context.Veiculos.SingleOrDefaultAsync(predicate);
        }

        public void Update(Veiculo entity)
        {
            _context.Veiculos.Update(entity);
        }

        public Task UpdateAsync(Veiculo veiculo)
        {
            _context.Veiculos.Update(veiculo);
            return Task.CompletedTask;
        }
    }
}