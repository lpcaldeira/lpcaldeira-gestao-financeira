using System;
using System.Linq;
using Dapper.Core.Attributes;
using Dapper.Core.Contexts.Database.Tables;

namespace Dapper.Core.Contexts.Database
{
    public class DapperTable
    {
        public static Table Criar(Type type)
        {
            var fields = type.GetProperties()
                .Where(property => !property.GetCustomAttributes(true).OfType<Reference>().Any() &&
                                   !property.GetCustomAttributes(true).OfType<Ignore>().Any())
                .Select(DapperField.Criar);

            var key = type.GetProperties()
                .Where(property => property.GetCustomAttributes(true).OfType<Key>().Any())
                .SelectMany(property => property.GetCustomAttributes(true).OfType<Key>()).FirstOrDefault();

            if (key == null)
                throw new Exception("Chave primária não foi definida");

            return new Table(type.Name, key.SequenceName, fields);
        }
    }
}