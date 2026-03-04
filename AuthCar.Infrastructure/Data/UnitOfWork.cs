using AuthCar.Domain.Interface.Repository;

namespace AuthCar.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public IUsuarioRepository UsuarioRepository { get; }
        public IVeiculoRepository VeiculoRepository { get; }

        public UnitOfWork(AppDbContext context,
                          IUsuarioRepository usuarioRepository,
                          IVeiculoRepository veiculoRepository)
        {
            _context = context;
            UsuarioRepository = usuarioRepository;
            VeiculoRepository = veiculoRepository;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void ExecuteInTransaction(Action action)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                action();
                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task ExecuteInTransactionAsync(Func<Task> actionAsync)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await actionAsync();
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}