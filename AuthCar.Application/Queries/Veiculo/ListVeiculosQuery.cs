using AuthCar.Application.DTOs;
using MediatR;

namespace AuthCar.Application.Queries
{
    public class ListVeiculosQuery : IRequest<IEnumerable<VeiculoResponseDTO>>
    {
    }
}