using Foundation.Domain.Abstractions;

namespace AuthCar.Domain.Entities
{
    public class Usuario : Entity, IUsuario
    {
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
    }
}