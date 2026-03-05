using Foundation.Domain.Interfaces;

namespace AuthCar.Domain.Entities
{
    public interface IUsuario : IEntity
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
    }
}