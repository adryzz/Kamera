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
        _port = new SerialPort(name, Configuration.ReaderSpeed);
    }

    public void Start() => _port.Open();

    public void MoveNext()
    {
        Column += _way;
        if (Column is > Configuration.Width or < 0)
        {
            _way = -_way;
            Column += _way;
            Row++;
        }
        
        if (Row > Configuration.Height)
        {
            throw new InvalidOperationException();
        }
    }

    public void Reset()
    {
        Row = 0;
        Column = 0;
    }

    public ushort Read()
    {
        _port.Write(new byte[] { 0 }, 0, 1);
        
        Thread.Sleep(10);

        byte[] packet = new byte[2];
        _port.Read(packet, 0, 2);
        return BitConverter.ToUInt16(packet);
    }

    public void Dispose()
    {
        _port.Dispose();
    }
}