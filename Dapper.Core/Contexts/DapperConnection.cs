using System.Data;

namespace Dapper.Core.Contexts
{
    public abstract class DapperConnection
    {
        protected string NomeUsuario;
        protected string Senha;
        protected string Host;
        protected int Porta;
        protected string NomeBaseDeDados;

        public DapperConnection SetarUsuario(string nomeUsuario)
        {
            NomeUsuario = nomeUsuario;
            return this;
        }

        public DapperConnection SetarSenha(string senha)
        {
            Senha = senha;
            return this;
        }

        public DapperConnection SetarHost(string host)
        {
            Host = host;
            return this;
        }

        public DapperConnection SetarPorta(int porta)
        {
            Porta = porta;
            return this;
        }

        public DapperConnection SetarNomeBaseDeDados(string nomeBaseDeDados)
        {
            NomeBaseDeDados = nomeBaseDeDados;
            return this;
        }

        public abstract IDbConnection Criar();
    }
}