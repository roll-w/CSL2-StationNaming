using System;

namespace StationNaming;

public enum SerialVersion
{
    Version1,
    Version2,
    Version3,
}

public static class SerialVersionExtensions
{
    public static string ToVersionString(this SerialVersion version)
    {
        return version switch
        {
            SerialVersion.Version1 => "VERSION_1",
            SerialVersion.Version2 => "VERSION_2",
            SerialVersion.Version3 => "VERSION_3",
            _ => throw new ArgumentOutOfRangeException(nameof(version), version, null),
        };
    }

    public static string ToFormatString(this SerialVersion version)
    {
        return "\n" + version.ToVersionString();
    }

    public const string Prefix = "\nVERSION_";
}