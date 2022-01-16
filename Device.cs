using System.IO.Ports;

namespace Kamera;

public class Device : IDisposable
{
    private SerialPort _port;
    public int Row { get; private set; } = 0;
    public int Column { get; private set; } = 0;

    private int _way = 1;

    public Device(string name)
    {
        _port = new SerialPort(name);
    }

    public void Start() => _port.Open();

    public void MoveNext()
    {
        if (Row > Configuration.Height)
        {
            throw new InvalidOperationException();
        }
        
        Column += _way;
        if (Column > Configuration.Width || Column < 0)
        {
            _way = -_way;
            Column += _way;
            Row++;
        }
    }

    public void Reset()
    {
        Row = 0;
        Column = 0;
    }

    public ushort Read()
    {
        return 0;
    }

    public void Dispose()
    {
        _port.Dispose();
    }
}