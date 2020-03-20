using System;
using Dapper.Core.Attributes;

namespace Gomi.Infraestrutura.Models.Financeiro
{
    [Table("pme")]
    public class PME
    {
        [Key("pmesequence")]
        [IsNotNull]
        public int Id { get; set; }

        [IsNotNull]
        public DateTime Data { get; set; }

        [IsNotNull]
        public int Dias { get; set; }
    }
}