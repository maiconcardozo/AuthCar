using Foundation.Domain.Interfaces.UnitOfWork;

namespace AuthCar.Domain.Interface.Repository
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IUsuarioRepository UsuarioRepository { get; }
        IVeiculoRepository VeiculoRepository { get; }
    }
}
