using AuthCar.Application.DTOs;
using AuthCar.Domain.Enums;
using MediatR;

namespace AuthCar.Application.Commands.Veiculo
{
    public class AddVeiculoCommand : IRequest<VeiculoResponseDTO>
    {
        public string Descricao { get; set; } = string.Empty;
        public Marca Marca { get; set; }
        public string Modelo { get; set; } = string.Empty;
        public decimal? Valor { get; set; }
    }
}