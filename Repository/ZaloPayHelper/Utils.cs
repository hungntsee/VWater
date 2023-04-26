namespace ZaloPay.Helper
{
    public class Utils
    {
        public static long GetTimeStamp(DateTime date) {
            return (long)(date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        }

        public static long GetTimeStamp(){
            return GetTimeStamp(DateTime.UtcNow.AddHours(7));
        }

        public static string GenTransID() {
            return DateTime.UtcNow.AddHours(7).ToString("yyMMdd") + "_" + (GetTimeStamp());
        }

    }
}