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
        //SendPacket(BitConverter.GetBytes(Column), BitConverter.GetBytes(Row));
    }

    public void Reset()
    {
        Row = 0;
        Column = 0;
        //SendPacket(BitConverter.GetBytes(Column), BitConverter.GetBytes(Row));
    }

    public ushort Read()
    {
        byte[] packet = SendPacket(0, 0, 0);
        return BitConverter.ToUInt16(packet, 1);
    }

    private byte[] SendPacket(params byte[] packet)
    {
        _port.Write(packet, 0, packet.Length);
        byte[] p = new byte[3];
        
        for(int i = 0; i < p.Length; i++)
        {
            int read = _port.ReadByte();
            if (read == -1)
                throw new InvalidOperationException();

            p[i] = (byte) read;
        }
        return p;
    }

    public void Dispose()
    {
        _port.Dispose();
    }
}