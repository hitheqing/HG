using System.Diagnostics;

namespace HG
{
    public static class ScriptsTime
    {
        private static readonly Stopwatch Stopwatch = new Stopwatch();

        private const string LogPrefix = "<color=yellow>[ScriptsTime]  </color>";

        public static void Start()
        {
            Stopwatch.Restart();
        }

        public static void Show()
        {
            var offset = Stopwatch.Elapsed.TotalSeconds;
            Loger.Info(LogPrefix + offset.ToString("#0.00000000"));
            Stopwatch.Restart();
        }
    }
}