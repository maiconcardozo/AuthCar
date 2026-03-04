using AuthCar.Domain.Enums;
using Foundation.Domain.Interfaces;

namespace AuthCar.Domain.Entities
{
    public interface IVeiculo : IEntity
    {
        public string Descricao { get; set; }
        public Marca Marca { get; set; }
        public string Modelo { get; set; }
        public decimal? Valor { get; set; }
    }
}