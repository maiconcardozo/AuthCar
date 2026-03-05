using Foundation.Domain.Abstractions;
using Foundation.Shared.Helpers;

namespace AuthCar.Domain.Entities
{
    public class Usuario : Entity, IUsuario
    {
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Nome { get; private set; }

        private Usuario() { }

        public Usuario(string nome, string login, string senhaClaro)
        {
            SetNome(nome);
            SetLogin(login);
            SetSenha(senhaClaro);
        }

        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser vazio.");
            Nome = nome;
        }

        public void SetLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login) || login.Length < 4)
                throw new ArgumentException("Login inválido.");
            Login = login;
        }

        public void SetSenha(string senhaClaro)
        {
            if (string.IsNullOrWhiteSpace(senhaClaro) || senhaClaro.Length < 6)
                throw new ArgumentException("Senha fraca.");
            Senha = StringHelper.ComputeArgon2Hash(senhaClaro);
        }

        public bool ValidarSenha(string senhaClaro)
        {
            return StringHelper.VerifyArgon2Hash(senhaClaro, Senha);
        }
    }
}