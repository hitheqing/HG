namespace HG
{
    public partial class TimeMgr
    {
        public const int DaySeconds = 86400;
        public const int HourSeconds = 3600;
        
        public static int TimeArea { get { return 0; } }
        
        public static int GetDayEndTime(int timeStamp)
        {
            return ((timeStamp + TimeArea * HourSeconds) / DaySeconds + 1) * DaySeconds - TimeArea * HourSeconds;
        }
    }
}