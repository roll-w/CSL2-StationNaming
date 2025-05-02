using System;

namespace StationNaming;

public enum SerialVersion
{
    Version1,
    Version2,
    Version3,
    Version4
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
            SerialVersion.Version4 => "\04",
            _ => throw new ArgumentOutOfRangeException(nameof(version), version, null),
        };
    }

    public static string ToFormatString(this SerialVersion version)
    {
        if (version == SerialVersion.Version4)
        {
            return "\04";
        }
        return "\n" + version.ToVersionString();
    }

    public const string Prefix = "\nVERSION_";

    public static bool ParseVersionMark(string line, out SerialVersion? version)
    {
        if (line.StartsWith(Prefix))
        {
            switch (line)
            {
                case "\nVERSION_1":
                    version = SerialVersion.Version1;
                    return true;
                case "\nVERSION_2":
                    version = SerialVersion.Version2;
                    return true;
                case "\nVERSION_3":
                    version = SerialVersion.Version3;
                    return true;
            }
        }
        if (line.StartsWith("\04"))
        {
            version = SerialVersion.Version4;
            return true;
        }
        version = null;
        return false;
    }
}