using System.Collections.Generic;
using Gomi.Infraestrutura.Models.Pessoas;

namespace Gomi.Infraestrutura.ValueObjects
{
    public class UFsValueObject
    {
        public static IEnumerable<Uf> Dados => new HashSet<Uf>
        {
            new Uf (11, "Rondônia", "RO"),
            new Uf (12, "Acre", "AC"),
            new Uf (13, "Amazonas", "AM"),
            new Uf (14, "Roraima", "RR"),
            new Uf (15, "Pará", "PA"),
            new Uf (16, "Amapá", "AP"),
            new Uf (17, "Tocantins", "TO"),
            new Uf (21, "Maranhão", "MA"),
            new Uf (22, "Piauí", "PI"),
            new Uf (23, "Ceará", "CE"),
            new Uf (24, "Rio Grande do Norte", "RN"),
            new Uf (25, "Paraíba", "PB"),
            new Uf (26, "Pernambuco", "PE"),
            new Uf (27, "Alagoas", "AL"),
            new Uf (28, "Sergipe", "SE"),
            new Uf (29, "Bahia", "BA"),
            new Uf (31, "Minas Gerais", "MG"),
            new Uf (32, "Espírito Santo", "ES"),
            new Uf (33, "Rio de Janeiro", "RJ"),
            new Uf (35, "São Paulo", "SP"),
            new Uf (41, "Paraná", "PR"),
            new Uf (42, "Santa Catarina", "SC"),
            new Uf (43, "Rio Grande do Sul", "RS"),
            new Uf (50, "Mato Grosso do Sul", "MS"),
            new Uf (51, "Mato Grosso", "MT"),
            new Uf (52, "Goiás", "GO"),
            new Uf (53, "Distrito Federal", "DF")
        };
    }
}
