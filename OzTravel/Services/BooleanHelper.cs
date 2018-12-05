using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OzTravel.Services
{
    public static class BooleanHelper
    {
        public static string IsProvider(bool value)
        {
            string provider = string.Format("{0:on; 0;OFF}", true.GetHashCode());
            string customer = string.Format("{0:on; 0;OFF}", false.GetHashCode());

            return provider;
        }

        public static string IsAvailable(bool value)
        {
            string available = string.Format("{0:on; 0;OFF}", true.GetHashCode());
            string notavailable = string.Format("{0:on; 0;OFF}", false.GetHashCode());

            return available;
        }
    }
}
