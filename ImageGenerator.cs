using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Kamera;

public class ImageGenerator
{
    private bool _started = false;
    private Device _device;
    private int _points = 0;

    public ImageGenerator()
    {
        _device = new Device(Configuration.Port);
    }

    public void StartProcessing()
    {
        if (!_started)
        {
            _started = true;
            new Thread(Process).Start();
        }
    }
    
    public void StopProcessing()
    {
        
    }

    private void Process()
    {
        using (Image<Rgb48> image = new Image<Rgb48>(Configuration.Width+1, Configuration.Height))
        {
            while (_points < Configuration.Width * Configuration.Height)
            {
                image[_device.Column, _device.Row] = HeatMap(_device.Read());
                _device.MoveNext();
                _points++;
            }
            image.SaveAsPng("image.png");
        }
    }
    
    public Rgb48 HeatMap(ushort value)
    {
        double val = value / (double)ushort.MaxValue;
        return new Rgb48()
        {
            R = Convert.ToUInt16(ushort.MaxValue * val),
            G = 0,
            B = Convert.ToUInt16(ushort.MaxValue * (1-val))
        };
    }
    
    public ImageGenerationProgress GetProgress()
    {
        return new ImageGenerationProgress
        {
            CurrentRow = _device.Row,
            CurrentColumn = _device.Column,
            Started = _started,
            Percentage =  (int)((double)_points / (Configuration.Width * Configuration.Height) * 100),
            Time = DateTime.Now
        };
    }
}