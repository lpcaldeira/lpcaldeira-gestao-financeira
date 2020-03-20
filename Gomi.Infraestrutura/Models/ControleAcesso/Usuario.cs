using System;
using Dapper.Core.Attributes;
using Dapper.Core.Enums;
using Gomi.Infraestrutura.Services;

namespace Gomi.Infraestrutura.Models.ControleAcesso
{
    [Table("usuario")]
    public class Usuario
    {
        [Key("usuariosequence")]
        [IsNotNull]
        public int Id { get; set; }

        [Length(255)]
        [IsNotNull]
        public string Nome { get; set; }

        [Length(255)]
        [IsNotNull]
        public string NomeAcesso { get; set; }

        [Length(255)]
        [IsNotNull]
        public string Senha { get; set; }

        [Ignore]
        public string NovaSenha { get; set; }

        [Ignore]
        public string ConfirmaSenha { get; set; }

        [Reference(typeof(Perfil), RelationType.OneToOne)]
        public Perfil Perfil { get; set; }

        [Ignore]
        public virtual void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                throw new Exception("Nome não informado");
            if (string.IsNullOrEmpty(NomeAcesso))
                throw new Exception("Id do usuário não informado");
            if (NovaSenha != ConfirmaSenha)
                throw new Exception("Senhas não conferem");
        }

        [Ignore]
        public virtual void DefinirSenha()
        {
            if (string.IsNullOrEmpty(ConfirmaSenha)) return;
            Senha = HashService.Calcular(ConfirmaSenha);
        }

        [Ignore]
        public virtual void Atualizar(string nome, string nomeAcesso, string novaSenha, string confirmaSenha)
        {
            Nome = nome;
            NomeAcesso = nomeAcesso;
            NovaSenha = novaSenha;
            ConfirmaSenha = confirmaSenha;
        }
    }
}
