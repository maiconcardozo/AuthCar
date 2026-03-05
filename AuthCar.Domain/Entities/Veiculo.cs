using AuthCar.Domain.Entities;
using AuthCar.Domain.Enums;
using Foundation.Domain.Abstractions;

public class Veiculo : Entity, IVeiculo
{
    public string Descricao { get; private set; }
    public Marca Marca { get; private set; }
    public string Modelo { get; private set; }
    public decimal? Valor { get; private set; }

    public Veiculo(string descricao, Marca marca, string modelo, decimal? valor)
    {
        SetDescricao(descricao);
        SetMarca(marca);
        SetModelo(modelo);
        SetValor(valor);
    }

    public void SetDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição inválida");
        Descricao = descricao;
    }

    public void SetMarca(Marca marca)
    {
        // Validação da marca, caso necessário
        Marca = marca;
    }

    public void SetModelo(string modelo)
    {
        if (string.IsNullOrWhiteSpace(modelo))
            throw new ArgumentException("Modelo inválido");
        Modelo = modelo;
    }

    public void SetValor(decimal? valor)
    {
        if (valor < 0)
            throw new ArgumentException("Valor não pode ser negativo.");
        Valor = valor;
    }
}