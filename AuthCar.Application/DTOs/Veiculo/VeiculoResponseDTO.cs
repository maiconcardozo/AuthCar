using AuthCar.Domain.Enums;

namespace AuthCar.Application.DTOs
{
    public class VeiculoResponseDTO
    {
        public Guid Codigo { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Marca Marca { get; set; }
        public string Modelo { get; set; } = string.Empty;
        public decimal? Valor { get; set; }
    }
}