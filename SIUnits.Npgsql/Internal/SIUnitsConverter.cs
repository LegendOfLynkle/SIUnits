using System.Diagnostics.CodeAnalysis;
using Npgsql.Internal;

namespace SIUnits.Npgsql.Internal;

[Experimental("NPG9001")]
sealed class SIUnitsConverter : PgBufferedConverter<SIUnit>
{
    public override bool CanConvert(DataFormat format, out BufferRequirements bufferRequirements)
    {
        // Probably a better way to do this, but I know it is/should be 16 bytes.
        bufferRequirements = BufferRequirements.CreateFixedSize(16);
        return format is DataFormat.Binary;
    }

    protected override SIUnit ReadCore(PgReader reader)
    {
        // Again this is probably very wrong but the initial implementation is less important than actually getting here
        // to try and debug it
        var test = reader.ReadDouble();
        // var valueBytes = reader.ReadBytes(8);
        var unitBytes = reader.ReadBytes(8);
        // TODO: Pull out the relevant data from the bytes...
        return new SIUnit
        {
            // Value = BitConverter.ToDouble(valueBytes.FirstSpan),
            Value = test,
            Units = (sbyte[])(Array)unitBytes.FirstSpan.ToArray()
        };
    }

    protected override void WriteCore(PgWriter writer, SIUnit value)
    {
        // This is probably wrong since I don't account for endianness but for now I'll take writing something over
        // writing nothing
        writer.WriteDouble(value.Value);
        // writer.WriteBytes(BitConverter.GetBytes(value.Value));
        writer.WriteBytes((byte[])(Array)value.Units);
    }
}