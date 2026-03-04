using AuthCar.Application.DTOs;
using AuthCar.Domain.Entities;
using AutoMapper;

namespace AuthCar.Application.Mappers
{
    public class AuthCarMapper : Profile
    {
        public AuthCarMapper()
        {
            CreateMap<Usuario, UsuarioResponseDTO>();
            CreateMap<Veiculo, VeiculoResponseDTO>();

            CreateMap<UsuarioResponseDTO, Usuario>();
            CreateMap<VeiculoResponseDTO, Veiculo>();
        }
    }
}