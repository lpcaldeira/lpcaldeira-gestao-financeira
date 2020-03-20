using System.Data;
using Dapper.Core.Contexts;
using Npgsql;

namespace Dapper.PostgreSql.Contexts
{
    public class PostgresConnection : DapperConnection
    {

        public static PostgresConnection Init()
        {
            return new PostgresConnection();
        }

        public override IDbConnection Criar()
        {
            return new NpgsqlConnection($"User ID={NomeUsuario}; " +
                                        $"Password={Senha}; " +
                                        $"Host={Host}; " +
                                        $"Port={Porta}; " +
                                        $"Database={NomeBaseDeDados}; " +
                                        "Pooling=true; "+
                                        "MinPoolSize=0; "+
                                        "MaxPoolSize=1000; "+
                                        "ConnectionIdleLifetime=0; ");
        }
    }
}