using System.Diagnostics.CodeAnalysis;
using Npgsql.TypeMapping;
using SIUnits.Npgsql.Internal;

namespace SIUnits.Npgsql;

public static class NpgsqlSIUnitsExtensions
{
    [Experimental("NPG9001")]
    public static INpgsqlTypeMapper UseSIUnits(this INpgsqlTypeMapper mapper)
    {
        mapper.AddTypeInfoResolverFactory(new SIUnitsTypeInfoResolverFactory());
        return mapper;
    }
}
