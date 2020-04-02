namespace HG
{
    public class NetSend
    {
        public byte[] Buffer { get; private set; }
        public int Pos { get; set; }
        public int Length { get; private set; }
        
        public NetSend()
        {
            Reset();
        }

        public void SetBuffer(byte[] bytes)
        {
            Buffer = bytes;
            Pos = 0;
            Length = Buffer.Length;
        }

        public void Reset()
        {
            Buffer = null;
            Pos = 0;
            Length = 0;
        }
    }
}