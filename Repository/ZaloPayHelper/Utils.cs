namespace ZaloPay.Helper
{
    public class Utils
    {
        public static long GetTimeStamp(DateTime date) {
            return (long)(date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        }

        public static long GetTimeStamp(){
            return GetTimeStamp(DateTime.Now);
        }

        public static string GenTransID() {
            return DateTime.Now.ToString("yyMMdd") + "_" + (GetTimeStamp());
        }

    }
}