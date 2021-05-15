using SEDC.TimeTrackingApp.Hm.Domain.Entities;
using SEDC.TimeTrackingApp.Hm.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.TimeTrackingApp.Hm.Domain.Interfaces
{
    public interface IBaseActivity
    {
        DateTime StartTrackingActivity { get; set; }
        DateTime StopTrackingActivity { get; set; }
        TimeSpan TrackedTime { get; set; }
        ActivityType ActivityType { get; set; }
        void TrackTime();
        //void PrintStatistics();


    }
}
