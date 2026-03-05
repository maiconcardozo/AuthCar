using System;

namespace AuthCar.Application.DTOs
{
    public class UsuarioResponseDTO
    {
        public Guid Codigo { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
    }
}