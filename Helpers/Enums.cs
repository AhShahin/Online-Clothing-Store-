using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Helpers
{
    public class Enums
    {
        public enum Gender
        {
            Male,
            Female
        }

        public enum order_status
        {
            Pending,
            Processing,
            Rejected,
            Completed
        }

        public enum Size
        {
            XS,
            S,
            M,
            L,
            XL,
            XXL
        }
    }
}
