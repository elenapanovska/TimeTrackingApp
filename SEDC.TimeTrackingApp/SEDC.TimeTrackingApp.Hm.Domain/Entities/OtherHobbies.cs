using System;
using SEDC.TimeTrackingApp.Hm.Domain.Enums;
using System.Collections.Generic;
using System.Text;

namespace SEDC.TimeTrackingApp.Hm.Domain.Entities
{
    public class OtherHobbies : BaseActivity
    {
        public string Hobby { get; set; }

        public OtherHobbies()
        {
            ActivityType = ActivityType.OtherHobbies;
        }

    }
}
