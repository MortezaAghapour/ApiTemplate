using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace RabitMQTask.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void RegisterEntities<TType>(this ModelBuilder builder, params Assembly[] assemblies)
        {

            //GetExportedTypes():Gets the public types defined in this assembly that are visible outside the assembly
            var types = assemblies.SelectMany(c => c.GetExportedTypes()).Where(c =>
                c.IsClass && !c.IsAbstract && c.IsPublic && typeof(TType).IsAssignableFrom(c));
            foreach (var type in types)
            {
                builder.Entity(type);
            }
        }



        public static void RegisterConfigurations(this ModelBuilder builder, Assembly assembly)
        {
            builder.ApplyConfigurationsFromAssembly(assembly);
        }


        public static void AddRestrictDeleteBehaviorConvention(this ModelBuilder builder)
        {
            var cascadeFks = builder.Model.GetEntityTypes().SelectMany(c => c.GetForeignKeys())
                .Where(c => c.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFks)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public static void PluralizingTableNameConvention(this ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().Pluralize());
            }
        }
    }
}