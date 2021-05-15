using SEDC.TimeTrackingApp.Hm.Domain.Enums;
using SEDC.TimeTrackingApp.Hm.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.TimeTrackingApp.Hm.Domain.Entities
{
    public class Reading : BaseActivity
    {
        public  int Pages { get; set; }
        public BookType BookType { get; set; }

        public Reading()
        {
            ActivityType = ActivityType.Reading;
        }


    }
}