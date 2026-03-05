using Foundation.Domain.Interfaces;

public interface IUsuario : IEntity
{
    string Login { get; }
    string Nome { get; }
    string Senha { get; }

    void SetNome(string nome);
    void SetLogin(string login);
    void SetSenha(string senhaClaro);
    bool ValidarSenha(string senhaClaro);
}