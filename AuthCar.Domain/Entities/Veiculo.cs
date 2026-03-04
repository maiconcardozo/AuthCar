using AuthCar.Domain.Enums;
using Foundation.Domain.Abstractions;

namespace AuthCar.Domain.Entities
{
    public class Veiculo : Entity, IVeiculo
    {
        public string Descricao { get; set; }
        public Marca Marca { get; set; }
        public string Modelo { get; set; }
        public decimal? Valor { get; set; }
    }
}
