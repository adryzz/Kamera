using SixLabors.ImageSharp.PixelFormats;

namespace Kamera;

public static class HeatmapUtils
{
    private static readonly Rgb48[] MapColors =
    {
        new(ushort.MinValue, ushort.MinValue, ushort.MinValue), //black
        new(ushort.MinValue, ushort.MinValue, ushort.MaxValue), //blue
        new(ushort.MinValue, ushort.MaxValue, ushort.MaxValue), //cyan
        new(ushort.MinValue, ushort.MaxValue, ushort.MinValue), //green
        new(ushort.MaxValue, ushort.MaxValue, ushort.MinValue), //yellow
        new(ushort.MaxValue, ushort.MinValue, ushort.MinValue), //red
        new(ushort.MaxValue, ushort.MaxValue, ushort.MaxValue) //white
    };
    
    public static Rgb48 GetColor(ushort val)
    {
        double valPerc = (double)(val-ushort.MinValue) / (ushort.MaxValue-ushort.MinValue);
        double colorPerc = 1d / (MapColors.Length-1);
        double blockOfColor = valPerc / colorPerc;
        int blockIdx = (int)Math.Truncate(blockOfColor);
        double valPercResidual = valPerc - (blockIdx*colorPerc);
        double percOfColor = valPercResidual / colorPerc;
        
        Rgb48 cTarget = MapColors[blockIdx];
        Rgb48 cNext = val == ushort.MaxValue ? MapColors[blockIdx] : MapColors[blockIdx + 1];
        
        var deltaR = cNext.R - cTarget.R;
        var deltaG = cNext.G - cTarget.G;
        var deltaB = cNext.B - cTarget.B;

        var r = cTarget.R + (deltaR * percOfColor);
        var g = cTarget.G + (deltaG * percOfColor);
        var b = cTarget.B + (deltaB * percOfColor);
        
        Rgb48 c = MapColors[0];
        
        try
        {
            c = new Rgb48((ushort)r, (ushort)g, (ushort)b);
        }
        catch(InvalidCastException)
        {
        }
        return c;
    }
}