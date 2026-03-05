using AuthCar.Domain.Enums;
using Foundation.Domain.Interfaces;

public interface IVeiculo : IEntity
{
    string Descricao { get; }
    Marca Marca { get; }
    string Modelo { get; }
    decimal? Valor { get; }

    void SetDescricao(string descricao);
    void SetMarca(Marca marca);
    void SetModelo(string modelo);
    void SetValor(decimal? valor);
}