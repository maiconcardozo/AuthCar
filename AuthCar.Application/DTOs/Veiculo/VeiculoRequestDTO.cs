using AuthCar.Domain.Enums;

namespace AuthCar.Application.DTOs
{
    public class VeiculoRequestDTO
    {
        public string Descricao { get; set; }
        public Marca Marca { get; set; }
        public string Modelo { get; set; }
        public decimal? Valor { get; set; }
    }
}