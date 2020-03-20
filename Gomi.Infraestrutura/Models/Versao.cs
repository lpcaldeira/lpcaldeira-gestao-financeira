using Dapper.Core.Attributes;

namespace Gomi.Infraestrutura.Models
{
    [Table("versao")]
    public class Versao
    {
        [Key("versaosequence")]
        [IsNotNull]
        public int Id { get; set; }

        [IsNotNull] 
        public int Valor { get; set; }
    }
}