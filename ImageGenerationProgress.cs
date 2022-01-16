namespace Kamera;

public class ImageGenerationProgress
{
    public bool Started { get; set; }
    
    public int CurrentRow { get; set; }
    
    public int CurrentColumn { get; set; }
    
    public int Percentage { get; set; }
    
    public DateTime Time { get; set; }
}