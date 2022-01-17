namespace Kamera;

public class Configuration
{
    public const byte BitDepth = 12;
    
    public static readonly Type DataPoint = typeof(ushort);

    public const int Width = 270;
    public const int Height = 270;
    public const int ReadingsPerPoint = 256;
    public const int ReaderSpeed = 115200;

    public static readonly string Port = "/dev/ttyS0";
}