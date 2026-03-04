namespace AuthCar.Application.DTOs
{
    public class VeiculoRequestDTO
    {
        public string Descricao { get; set; }
        public int Marca { get; set; }
        public string Modelo { get; set; }
        public decimal? Valor { get; set; }
    }
}