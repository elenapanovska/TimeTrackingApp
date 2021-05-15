using SEDC.TimeTrackingApp.Hm.Domain.Enums;
using SEDC.TimeTrackingApp.Hm.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace SEDC.TimeTrackingApp.Hm.Domain.Entities
{
    public  class BaseActivity : BaseEntity, IBaseActivity
    {
        public DateTime StartTrackingActivity { get; set; }
        public DateTime StopTrackingActivity { get; set; }
        public TimeSpan TrackedTime { get; set; }
        public ActivityType ActivityType { get; set; }

        
        public void TrackTime()
        {
            StartTrackingActivity = DateTime.Now;
            Console.WriteLine($"You started tracking {ActivityType.ToString()} at {StartTrackingActivity}");

            Console.WriteLine("To stop tracking time please hit enter");
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.Enter)
            {
                StopTrackingActivity = DateTime.Now;
                TrackedTime = StopTrackingActivity - StartTrackingActivity;
                Console.WriteLine($"You were {ActivityType.ToString()} for {TrackedTime.Minutes} min {TrackedTime.Seconds} sec.");
            }
        }

    }

   
}
