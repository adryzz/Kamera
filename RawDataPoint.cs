using MessagePack;

namespace Kamera;

[MessagePackObject]
public struct RawDataPoint
{
    [Key(0)]
    public int X { get; set; }

    [Key(1)]
    public int Y { get; set; }

    [Key(2)]
    public ushort Data { get; set; }
}