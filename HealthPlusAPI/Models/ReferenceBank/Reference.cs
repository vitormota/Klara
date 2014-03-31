using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthPlusAPI.Models.ReferenceBank
{
    public class Reference
    {
        public static List<Int32> NewReference()
        {
            List<Int32> reference = new List<Int32>();
            
            reference.Add(123456789);
            reference.Add(25693);

            return reference;
        }
    }
}