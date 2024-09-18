using System.Runtime.InteropServices;

namespace SIUnits;

/// <summary>
/// SIUnit: A struct designed to represent an SI Unit of measurement.
/// Struct layout attempts to match that of  <seealso href="https://github.com/df7cb/postgresql-unit/blob/master/unit.h">postgresql-unit</seealso> extension's as closely as possible:
/// #define N_UNITS		8
/// typedef struct Unit {
/// double			value;
/// signed char		units[N_UNITS];
/// } Unit;
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Explicit)]
public struct SIUnit
{
    [field: FieldOffset(0)] public required double Value { get; set; }

    [field: FieldOffset(8), MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public required sbyte[] Units { get; set; }
}