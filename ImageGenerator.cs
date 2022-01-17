using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Kamera;

public class ImageGenerator
{
    private ImageProcessingStatus _status = ImageProcessingStatus.NotStarted;
    private int _points = 0;

    public ImageGenerator()
    {
        Program.Reader = new Device(Configuration.Port);
    }

    public void StartProcessing()
    {
        if (_status != ImageProcessingStatus.InProgress)
        {
            Program.Reader.Reset();
            _status = ImageProcessingStatus.InProgress;
            Program.Reader.Start();
            var processor = new Thread(Process);
            processor.Name = "ImageGenerator";
            processor.Start();
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
                image[Program.Reader.Column, Program.Reader.Row] = HeatMap(GetValue());
                Program.Reader.MoveNext();
                _points++;
            }
            image.SaveAsPng("image.png");
        }
    }

    private ushort GetValue()
    {
        ulong reading = 0;
        for (int i = 0; i < Configuration.ReadingsPerPoint; i++)
        {
            reading += Program.Reader.Read();
        }

        reading /= Configuration.ReadingsPerPoint;

        return (ushort)reading;
    }
    
    private Rgb48 HeatMap(ushort value)
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
            CurrentRow = Program.Reader.Row,
            CurrentColumn = Program.Reader.Column,
            Status = _status,
            Percentage =  (int)((double)_points / (Configuration.Width * Configuration.Height) * 100),
            Time = DateTime.Now
        };
    }
}