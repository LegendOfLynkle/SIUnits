using System.Diagnostics.CodeAnalysis;
using Npgsql.Internal;
using Npgsql.Internal.Postgres;

namespace SIUnits.Npgsql.Internal;

[Experimental("NPG9001")]
sealed class SIUnitsTypeInfoResolverFactory : PgTypeInfoResolverFactory
{
    public override IPgTypeInfoResolver CreateResolver() => new Resolver();
    public override IPgTypeInfoResolver CreateArrayResolver() => new ArrayResolver();

    private class Resolver : IPgTypeInfoResolver
    {
        private TypeInfoMappingCollection? _mappings;
        protected TypeInfoMappingCollection Mappings => _mappings ??= AddMappings(new());

        private static TypeInfoMappingCollection AddMappings(TypeInfoMappingCollection mappings)
        {
            mappings.AddStructType<SIUnit>("unit", static (options, mapping, _) =>
                mapping.CreateInfo(options, new SIUnitsConverter()));

            return mappings;
        }

        public PgTypeInfo? GetTypeInfo(Type? type, DataTypeName? dataTypeName, PgSerializerOptions options)
            => Mappings.Find(type, dataTypeName, options);
    }

    private class ArrayResolver : Resolver, IPgTypeInfoResolver
    {
        private TypeInfoMappingCollection? _mappings;
        private new TypeInfoMappingCollection Mappings => _mappings ??= AddMappings(new(base.Mappings));

        public new PgTypeInfo? GetTypeInfo(Type? type, DataTypeName? dataTypeName, PgSerializerOptions options)
            => Mappings.Find(type, dataTypeName, options);

        private static TypeInfoMappingCollection AddMappings(TypeInfoMappingCollection mappings)
        {
            mappings.AddStructArrayType<SIUnit>("unit");

            return mappings;
        }
    }
}