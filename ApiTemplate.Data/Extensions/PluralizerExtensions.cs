using Pluralize.NET.Core;

namespace RabitMQTask.Data.Extensions
{
    public static class PluralizerExtensions
    {
        public static string Pluralize(this string key)
        {
            var pluralizer = new Pluralizer();
            return pluralizer.Pluralize(key);

        }
        public static string Singularize(this string key)
        {
            var pluralizer = new Pluralizer();
            return pluralizer.Singularize(key);

        }
    }
}