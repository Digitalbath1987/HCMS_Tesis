using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Healthcare_MS
{
    public class DateRangeValidation : RangeAttribute
    {
        public DateRangeValidation()
              : base(typeof(DateTime),
                      DateTime.Now.AddYears(-120).ToShortDateString(),
                      DateTime.Now.ToShortDateString())
        {
            
        }
    }
}