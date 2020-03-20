using System;
using System.Collections.Generic;
using System.Linq;
using Dapper.Core.Attributes;
using Gomi.Infraestrutura.Enums;
using Gomi.Infraestrutura.Helpers;
using Gomi.Infraestrutura.ValueObjects;
using StringLength = System.ComponentModel.DataAnnotations.StringLengthAttribute;

namespace Gomi.Infraestrutura.Models.Pessoas
{
    [Table("pessoa")]
    public class Pessoa
    {
        [Key("pessoasequence")]
        [IsNotNull]
        public int Id { get; set; }

        [IsNotNull]
        public int IdOrigem { get; set; }

        [Length(3)]
        [IsNotNull]
        public TipoPessoa TipoPessoaValor { get; set; }

        [Length(50)]
        [IsNotNull]
        public string TipoPessoa
        {
            get => TipoPessoaValor.Description();
            set => TipoPessoaValor = EnumHelper.GetValueFromDescription<TipoPessoa>(value);
        }

        [Length(3)]
        [IsNotNull]
        public Transacionador TransacionadorValor { get; set; }

        [Length(50)]
        [IsNotNull]
        public string Transacionador
        {
            get => TransacionadorValor.Description();
            set => TransacionadorValor = EnumHelper.GetValueFromDescription<Transacionador>(value);
        }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Nome é obrigatório")]
        [Length(255)]
        [IsNotNull]
        public string Nome { get; set; }

        [Length(255)]
        public string RazaoSocial { get; set; }

        [Length(14)]
        public string Cnpj { get; set; }

        [Length(11)]
        public string Cpf { get; set; }

        [Length(20)]
        public string Ie { get; set; }

        [Length(20)]
        public string Im { get; set; }

        [Length(20)]
        public string Telefone { get; set; }

        [Length(20)]
        public string Celular { get; set; }

        [Length(255)]
        public string PessoaContato { get; set; }

        [Length(255)]
        public string Email { get; set; }

        [Length(255)]
        public string Endereco { get; set; }

        [Length(255)]
        public string Numero { get; set; }

        [Length(255)]
        public string Bairro { get; set; }

        [Length(255)]
        public string Cep { get; set; }

        [Length(255)]
        public string Complemento { get; set; }

        [Length(255)]
        public string Cidade { get; set; }

        [Length(255)]
        public string Uf { get; set; }

        [Length(255)]
        public string Pais { get; set; }

        [IsNotNull]
        public int OrigemInformacao { get; set; }

        [Ignore]
        public bool RegistroNovo => Id == 0;

        [Ignore]
        public IEnumerable<string> TiposPessoa => EnumHelper.ToList<TipoPessoa>().Select(x => x.Value);

        [Ignore]
        public string Documento => TipoPessoaValor == Enums.TipoPessoa.Fisica
            ? Convert.ToUInt64(Cpf).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(Cnpj).ToString(@"00\.000\.000\/0000\-00");

        [Ignore]
        public IEnumerable<Uf> ListaUFs => UFsValueObject.Dados.OrderBy(x => x.Sigla);

        public Pessoa SetarTransacionador(Transacionador transacionador)
        {
            TransacionadorValor = transacionador;
            return this;
        }
    }
}
