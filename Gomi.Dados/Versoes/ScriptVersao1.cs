using System.Collections.Generic;

namespace Gomi.Dados.Versoes
{
    public static class ScriptVersao1
    {
        public static int Versao => 1;
        
        public static IEnumerable<string> Scripts => new List<string>
        {
            "insert into empresa (IdOrigem, Razao, Fantasia, Cnpj, Telefone, Email, Endereco, Numero, Bairro, Cep, Complemento, Cidade, Uf, Pais, OrigemInformacao)" +
            "       values (0, 'Biard Sistemas', 'Biard Sistemas', '07170459000182', '4934412345', 'biard@biard.com.br', 'Rua Marechal Deodoro', '774', 'Centro', " +
            "               '89700003', 'Não tem', 'Concórdia', 'SC', 'Brasil', 0);",
            "insert into conta (IdOrigem, Descricao, SaldoInicial, OrigemInformacao) values (0, 'Livro caixa', 0, 0);",
            "insert into planoconta (IdOrigem, Nivel, Descricao, OrigemInformacao) values (0, '1', 'Padrão', 0);",
            "insert into centrocusto (IdOrigem, Nivel, Descricao, OrigemInformacao) values (0, '1', 'Padrão', 0);",
            "insert into usuario (Nome, NomeAcesso, Senha) values ('Gomen', 'gomen@gomi.com.br', '123456');",
            "insert into perfil (IdUsuario, IdEmpresa, TipoPerfil) values (1, 1, 'Administrador');"
        };
    }
}