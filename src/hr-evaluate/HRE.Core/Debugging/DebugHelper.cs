namespace HRE.Core.Debugging
{
    public class DebugHelper
    {
        public static bool IsDebug
        {
            get
            {
#if  DEBUG
                return true;
#else
                return false;
#endif
            }
        }
    }
}
