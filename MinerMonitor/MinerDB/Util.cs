using System;
using System.Collections.Generic;

namespace MDatabase
{
    public static class RandomDevice
    {
        public static int GetRandomID(List<int> alreadyIDs = null)
        {
            Random random = new Random();
            int result = 0;

            if (alreadyIDs != null)
            {
                do
                {
                    result = random.Next(10000, 99999);

                    if (alreadyIDs.Contains(result))
                        result = 0;
                }
                while (result == 0);

                return result;
            }
            else
                return random.Next(10000, 99999);
        }
    }

    public class DBConnType
    {
        public DBConnType()
        {

        }

        public static string MinerServer
        {
            get
            {
                return "d7Pt1WHH9OZCj9eKrmsxiLewtdGZR9SWxddRuVd5mixG3Dq3nwSnQaPGCqq+ynxJvuYYPUE+J3YO28ocxcLT1yzxzP5EdvIoex44in1snBYElskye5A9Lc6KEBYl8J2cIpjXncHweU8=";
            }
        }
    }
}
