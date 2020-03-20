using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dapper.Core.Attributes;
using Dapper.Core.Enums;

namespace Dapper.Core.Extensions
{
    public static class MethodExtension
    {
        private static PropertyInfo GetKeyProperty<TEntity>(this TEntity entity)
        {
            var keyProperty = entity.GetType().GetProperties()
                .FirstOrDefault(property => property.GetCustomAttributes(true).Any(attribute => attribute is Key));

            if (keyProperty == null) throw new Exception("Table don't have key.");

            return keyProperty;
        }

        private static IEnumerable<PropertyInfo> GetReferenceProperties<TEntity>(this TEntity entity, Type referenceType)
        {
            return entity.GetType().GetProperties().Where(property =>
                property.GetCustomAttributes(true).OfType<ReferenceKey>()
                    .Any(x => x.ReferenceType == referenceType))
                .Select(property => property);
        }

        private static IEnumerable<PropertyInfo> GetReferenceKeyProperties(object entity, Type referenceType = null)
        {
            return entity.GetType().GetProperties().Where(property =>
                property.GetCustomAttributes(true).OfType<ReferenceKey>().Any(r =>
                    {
                        if (referenceType != null)
                            return r.ReferenceType == referenceType;
                        return true;
                    })).Select(property => property);
        }

        private static IEnumerable<object> GetReferenceEntities<TEntity>(this TEntity entity, RelationType relationType)
        {
            return entity.GetType().GetProperties().Where(property => property.GetCustomAttributes(true)
                    .OfType<Reference>().Any(att => att.RelationType == relationType && att.CascadeType == CascadeType.CascadeAll))
                .Select(property => property.GetValue(entity))
                .Where(property => property != null);
        }

        public static IEnumerable<object> GetAllEntities<TEntity>(this TEntity entity)
        {
            var entitiesOneToOne = new List<object>();
            var entitiesOneToMany = new List<object>();

            GetEntities(entity, ref entitiesOneToOne, ref entitiesOneToMany);

            entitiesOneToOne.Add(entity);

            entitiesOneToOne.AddRange(entitiesOneToMany);

            return entitiesOneToOne;
        }

        private static void GetEntities<TEntity>(TEntity entity, ref List<object> entitiesOneToOne,
            ref List<object> entitiesOneToMany)
        {
            var referenceEntitiesOneToOne = GetReferenceEntities(entity, RelationType.OneToOne);
            var referenceEntitiesOneToMany = GetReferenceEntities(entity, RelationType.OneToMany);

            entitiesOneToOne.AddRange(referenceEntitiesOneToOne);

            foreach (var referencesEntities in referenceEntitiesOneToMany)
                entitiesOneToMany.AddRange(((IList)referencesEntities).Cast<object>());

            foreach (var referenceEntity in referenceEntitiesOneToOne)
                GetEntities(referenceEntity, ref entitiesOneToOne, ref entitiesOneToMany);

            foreach (var referencesEntities in referenceEntitiesOneToMany)
                foreach (var referenceEntity in (IList)referencesEntities)
                    GetEntities(referenceEntity, ref entitiesOneToOne, ref entitiesOneToMany);
        }

        private static IEnumerable<object> GetEntities<TEntity>(TEntity entity)
        {
            var entities = new List<object>();

            var referenceEntitiesOneToOne = GetReferenceEntities(entity, RelationType.OneToOne);
            var referenceEntitiesOneToMany = GetReferenceEntities(entity, RelationType.OneToMany);

            entities.AddRange(referenceEntitiesOneToOne);

            foreach (var referencesEntities in referenceEntitiesOneToMany)
                entities.AddRange(((IList)referencesEntities).Cast<object>());

            return entities;
        }

        public static void UpdateEntities<TEntity>(this TEntity obj, int keyValue)
        {
            var key = GetKeyProperty(obj);
            key.SetValue(obj, keyValue);

            var entities = GetEntities(obj);

            foreach (var entity in entities)
            {
                foreach (var referenceProperty in GetReferenceProperties(entity, obj.GetType()))
                    referenceProperty.SetValue(entity, keyValue);

                foreach (var referenceProperty in GetReferenceProperties(obj, entity.GetType()))
                {
                    var entityKey = GetKeyProperty(entity);
                    var entityValueKey = entityKey.GetValue(entity);
                    referenceProperty.SetValue(obj, entityValueKey);
                }
            }
        }

        public static void UpdateKeyReferences(this IEnumerable<object> entities, Type entityType, int keyId)
        {
            foreach (var entity in entities)
            {
                foreach (var referenceKeyProperty in GetReferenceKeyProperties(entity, entityType))
                    referenceKeyProperty.SetValue(entity, keyId);
            }
        }
    }
}