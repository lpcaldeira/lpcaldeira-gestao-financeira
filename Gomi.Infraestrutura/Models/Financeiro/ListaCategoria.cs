using System;
namespace Gomi.Infraestrutura.Models.Financeiro
{
    public class ListaCategoria
    {
        public ListaCategoria( int id, string descricao, string tipomovimento, string categoria)
        {
            Id = id;
            Descricao = descricao;
            Tipomovimento = tipomovimento;
            Categoria = categoria;

        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Tipomovimento { get; set; }
        public string Categoria { get; set; }
        
    }
}
