using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Utils
{
    public static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<T> HasSnakeCaseColumnName<T>(this PropertyBuilder<T> propertyBuilder, string suffix = "")
        {
            return propertyBuilder.HasColumnName($"{propertyBuilder.Metadata.Name}{suffix}".CastToUpperSnakeCase());
        }
    }
}