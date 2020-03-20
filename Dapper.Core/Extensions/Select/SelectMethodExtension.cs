using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper.Core.Attributes;
using Dapper.Core.Enums;

namespace Dapper.Core.Extensions.Select
{
    public static class SelectMethodExtension
    {
        public static Type[] GetAllTypes<TEntity>()
        {
            var type = typeof(TEntity);
            var types = new List<Type> { type };
            GetTypes(typeof(TEntity), ref types);
            return types.ToArray();
        }
        private static void GetTypes(Type entityType, ref List<Type> types)
        {
            var entityTypes = GetReferenceProperties(entityType, ref types);
            foreach (var type in entityTypes) GetTypes(type, ref types);
        }

        public static IEnumerable<Type> GetReferenceProperties(Type entityTypes, ref List<Type> types)
        {
            var typesEntity = entityTypes.GetProperties().Where(property
                    => property.GetCustomAttributes(true).OfType<Reference>().Any())
                .Select(property => property.GetCustomAttributes(true).OfType<Reference>())
                .SelectMany(references => references.Select(reference => reference.ReferenceType));
            types.AddRange(typesEntity);
            return typesEntity;
        }

        public static IEnumerable<PropertyInfo> GetReferenceKeyProperties(Type entityType)
        {
            return entityType.GetProperties().Where(property
                => property.GetCustomAttributes(true).OfType<ReferenceKey>().Any());
        }

        public static IEnumerable<PropertyInfo> GetReferenceProperties(Type entityType)
        {
            return entityType.GetProperties().Where(property
                => property.GetCustomAttributes(true).OfType<Reference>().Any());
        }

        public static Type GetReference(PropertyInfo referenceKey)
        {
            return referenceKey.GetCustomAttributes(true)
                .OfType<ReferenceKey>().FirstOrDefault().ReferenceType;
        }

        public static IEnumerable<TEntity> MapAllEntities<TEntity>(IEnumerable<object[]> allObjs)
        {
            var allEntities = allObjs.SelectMany(a => a).Where(a => a != null);
            MapEntities(allEntities);
            var result = allEntities.OfType<TEntity>();
            return result;
        }

        private static void MapEntities(IEnumerable<object> allEntities)
        {
            foreach (var entity in allEntities)
            {
                MapEntityOneToOne(entity, allEntities);
                MapEntityOneToMany(entity, allEntities);
            }
        }

        private static void MapEntityOneToOne(object entity, IEnumerable<object> allEntities)
        {
            var referencesKeys = GetReferenceKeyProperties(entity);
            var references = GetReferenceProperties(entity, RelationType.OneToOne);

            foreach (var reference in references)
            {
                var referenceKey = referencesKeys.FirstOrDefault(r => r.GetCustomAttributes(true)
                    .OfType<ReferenceKey>().Any(k => k.ReferenceType == reference.PropertyType))
                    .GetValue(entity);

                var referenceValue = allEntities
                    .FirstOrDefault(e => e.GetType() == reference.PropertyType &&
                                         e.GetType().GetProperty("Id").GetValue(e).Equals(referenceKey));

                reference.SetValue(entity, referenceValue);
            }
        }

        private static void MapEntityOneToMany(object entity, IEnumerable<object> allEntities)
        {
            var references = GetReferenceProperties(entity, RelationType.OneToMany);

            foreach (var reference in references)
            {
                var referenceKey = entity.GetType().GetProperty("Id").GetValue(entity);

                var referenceValues = allEntities.Where(e => e.GetType() == reference.PropertyType.GetGenericArguments()[0] &&
                                                             GetReferenceKeyProperties(e, entity.GetType())
                                                                 .Any(r => r.GetValue(e).Equals(referenceKey)));

                reference.PropertyType.GetMethod("Add").Invoke(entity, referenceValues.ToArray());
            }
        }

        private static IEnumerable<PropertyInfo> GetReferenceKeyProperties(object entity, Type referenceType = null)
        {
            return entity.GetType().GetProperties().Where(property =>
                property.GetCustomAttributes(true).OfType<ReferenceKey>()
                    .Any(r =>
                    {
                        if (referenceType != null)
                            return r.ReferenceType == referenceType;
                        return true;
                    })).Select(property => property);
        }

        private static IEnumerable<PropertyInfo> GetReferenceProperties(object entity, RelationType relationType)
        {
            return entity.GetType().GetProperties().Where(property =>
                property.GetCustomAttributes(true).OfType<Reference>()
                    .Any(r => r.RelationType == relationType)).Select(property => property);
        }

        public static PropertyInfo GetSingleKey<T>(string method)
        {
            var type = typeof(T);
            var keys = GetKeyPropertiesCache(type).ToList();
            var keyCount = keys.Count;
            if (keyCount > 1)
                throw new DataException($"{method}<T> only supports an entity with a single [Key] property");
            if (keyCount == 0)
                throw new DataException($"{method}<T> only supports an entity with a [Key] property");

            return keys[0];
        }

        private static IEnumerable<PropertyInfo> GetTypeProperties(Type type)
        {
            var properties = type.GetProperties().ToArray();
            return properties.ToList();
        }

        private static IEnumerable<PropertyInfo> GetKeyPropertiesCache(Type type)
        {
            var allProperties = GetTypeProperties(type).ToList();
            var keyProperties = allProperties.Where(p => p.GetCustomAttributes(true).Any(a => a is Key)).ToList();

            if (keyProperties.Count != 0) return keyProperties;
            var idProp = allProperties.Find(p => string.Equals(p.Name, "id", StringComparison.CurrentCultureIgnoreCase));
            if (idProp != null)
                keyProperties.Add(idProp);

            return keyProperties;
        }

        public static string GetTableName(Type type)
        {
            var tableAttr = type.GetCustomAttributes(false)
                .SingleOrDefault(attr => attr.GetType() == typeof(TableAttribute)) as dynamic;
            return tableAttr.Name;
        }
    }
}