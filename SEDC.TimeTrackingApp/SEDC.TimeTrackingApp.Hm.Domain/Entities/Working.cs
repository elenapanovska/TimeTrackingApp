using SEDC.TimeTrackingApp.Hm.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.TimeTrackingApp.Hm.Domain.Entities
{
    public class Working : BaseActivity
    {
        public WorkingFrom WorkingFrom { get; set; }

        public Working()
        {
            ActivityType = ActivityType.Working;
        }

    }
}
