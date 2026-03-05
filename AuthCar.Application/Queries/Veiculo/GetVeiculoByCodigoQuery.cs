using AuthCar.Application.DTOs;
using MediatR;

namespace AuthCar.Application.Queries
{
    public class GetVeiculoByCodigoQuery : IRequest<VeiculoResponseDTO?>
    {
        public Guid Codigo { get; set; }
    }
}