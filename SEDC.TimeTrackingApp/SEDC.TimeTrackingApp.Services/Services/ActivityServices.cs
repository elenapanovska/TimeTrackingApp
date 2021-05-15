using SEDC.TimeTrackingApp.Hm.Domain.Database;
using SEDC.TimeTrackingApp.Hm.Domain.Entities;
using SEDC.TimeTrackingApp.Hm.Domain.Enums;
using SEDC.TimeTrackingApp.Hm.Domain.Interfaces;
using SEDC.TimeTrackingApp.Services.Helpers;
using SEDC.TimeTrackingApp.Services.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.TimeTrackingApp.Services.Services
{
    public class ActivityServices<T> where T : BaseActivity
    {
        public static Menu menus = new Menu();
        private IDatabase<User> _db;

        public ActivityServices()
        {
            _db = new Database<User>();
        }
        public void TrackingTime(ActivityType activity, User user, IUserService<User> userService)
        {
            Console.Clear();
            switch (activity)
            {
                case ActivityType.Reading:
                    var reading  = new Reading();
                    reading.TrackTime();
                    Console.WriteLine("Please enter how many pages you've read and what kind Of book you were reading:");
                    Console.Write("Pages: ");
                    reading.Pages = ValidationHelpers.ParseNumber(Console.ReadLine(),int.MaxValue);
                    reading.BookType = (BookType)menus.ShowBookTypes();
                    reading.Id = user.Id;

                    user.ListOfActivities.Add(reading);
                    userService.AddActivity(user, reading, user.ReadingActivities);

                    MessageHelepers.Message("Activity is succesfully tracked!", ConsoleColor.Green);
                    break;
                case ActivityType.Exercising:
                    var exercising = new Exercising();
                    exercising.TrackTime();
                    Console.WriteLine("Please enter what kind of workout did you do");
                    exercising.ExercisingType = (ExercisingType)menus.ShowExercisingTypes();
                    
                    user.ListOfActivities.Add(exercising);
                    userService.AddActivity(user, exercising, user.ExercisingActivities);

                    MessageHelepers.Message("Activity is succesfully tracked!", ConsoleColor.Green);
                    break;
                case ActivityType.Working:
                    var working = new Working();
                    working.TrackTime();
                    Console.WriteLine("Where were you working from?");
                    working.WorkingFrom = (WorkingFrom)menus.ShowWorkingOptions();
                   
                    user.ListOfActivities.Add(working);
                    userService.AddActivity(user, working, user.WorkingActivities);

                    MessageHelepers.Message("Activity is succesfully tracked!", ConsoleColor.Green);
                    break;
                case ActivityType.OtherHobbies:
                    var otherHobbies = new OtherHobbies();
                    otherHobbies.TrackTime();
                    Console.WriteLine("Please enter the name of the hobby");
                    otherHobbies.Hobby = Console.ReadLine();
                    
                    user.ListOfActivities.Add(otherHobbies);
                    userService.AddActivity(user, otherHobbies, user.OtherHobbiesActivities);
                        
                    MessageHelepers.Message("Activity is succesfully tracked!", ConsoleColor.Green);
                    break;
                default:
                    break;
            }
        }

        public void ReadingStatistics(User user)
        {
            Console.Clear();
        
            //var allReadingAc = user.ListOfActivities.OfType<Reading>().ToList();
            //var allReadingAc = user.ListOfActivities.Where(ac => ac.ActivityType == ActivityType.Reading).ToList();

            var allReadingAc = user.ReadingActivities;
            
            if (!ValidationHelpers.CheckIfListIsEmpty(allReadingAc, "reading statistics")) return;

            var totalaReadingHours = allReadingAc.Sum(hours => hours.TrackedTime.Minutes);
            var averageReading = allReadingAc.Average(min => min.TrackedTime.Minutes);
            var totalPages = allReadingAc.Sum(pages => pages.Pages);
            
            // Favourite type

            int bellesLetters = allReadingAc.Where(x => x.BookType == BookType.BellesLettres).Count();
            int fiction = allReadingAc.Where(x => x.BookType == BookType.Fiction).Count();
            int profesionlLit = allReadingAc.Where(x => x.BookType == BookType.ProfessionalLiterature).Count();

            var readingDictionary = new Dictionary<BookType, int>()
            {
                {BookType.BellesLettres, bellesLetters },
                {BookType.Fiction, fiction },
                {BookType.ProfessionalLiterature, profesionlLit },
            };
            var favoritetype = readingDictionary.FirstOrDefault(type => type.Value == readingDictionary.Values.Max()).Key.ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append($"Total reading hours: {totalaReadingHours}\n")
              .Append($"Average time spent reading: {averageReading} min\n")
              .Append($"Total number of read pages: {totalPages}\n")
              .Append($"Favourite type: {favoritetype}\n");

            Console.WriteLine(sb.ToString());
        }

        public void WorkingStatistics(User user)
        {
            Console.Clear();

            //var allWorkingAc = user.ListOfActivities.OfType<Working>().ToList();
            
            var allWorkingAc = user.WorkingActivities;

            if (!ValidationHelpers.CheckIfListIsEmpty(allWorkingAc, "working statistics")) return;

            var totalWorkingHours = allWorkingAc.Sum(hours => hours.TrackedTime.Minutes);
            var averageWorking = allWorkingAc.Average(min => min.TrackedTime.Minutes);
            var homeWorking = allWorkingAc.Where(working => working.WorkingFrom == WorkingFrom.Home)
                                            .Sum(hours => hours.TrackedTime.Hours);
            var officeWorking = allWorkingAc.Where(working => working.WorkingFrom == WorkingFrom.Offce)
                                            .Sum(hours => hours.TrackedTime.Hours);

            // Favourite type

            int home = allWorkingAc.Where(x => x.WorkingFrom == WorkingFrom.Home).Count();
            int office = allWorkingAc.Where(x => x.WorkingFrom == WorkingFrom.Offce).Count();

            var workingDictionary = new Dictionary<WorkingFrom, int>()
            {
                {WorkingFrom.Home, home },
                {WorkingFrom.Offce, office },
            };
            var favoritetype = workingDictionary.FirstOrDefault(type => type.Value == workingDictionary.Values.Max()).Key.ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append($"Total working hours: {totalWorkingHours}\n")
              .Append($"Average time spent working: {averageWorking} min.\n")
              .Append($"Home worknig hours: {homeWorking}\n")
              .Append($"Office working hours {officeWorking}\n")
              .Append($"Favourite type: {favoritetype}\n");

            Console.WriteLine(sb.ToString());
        }

        public void ExercisingStatistics(User user)
        {
            Console.Clear();

            //var allExercisingAc = user.ListOfActivities.OfType<Exercising>().ToList();

            var allExercisingAc = user.ExercisingActivities;

            if (!ValidationHelpers.CheckIfListIsEmpty(allExercisingAc, "exercising statistics")) return;

            var totalalExercisingHours = allExercisingAc.Sum(hours => hours.TrackedTime.Minutes);
            var averageExercising = allExercisingAc.Average(min => min.TrackedTime.Minutes);
            
            // Favourite type

            int running = allExercisingAc.Where(x => x.ExercisingType == ExercisingType.Running).Count();
            int general = allExercisingAc.Where(x => x.ExercisingType == ExercisingType.General).Count();
            int sport = allExercisingAc.Where(x => x.ExercisingType == ExercisingType.Sport).Count();

            var exercisingDictionary = new Dictionary<ExercisingType, int>()
            {
                {ExercisingType.Running, running },
                {ExercisingType.General, general },
                {ExercisingType.Sport, sport },
            };
            var favoritetype = exercisingDictionary.FirstOrDefault(type => type.Value == exercisingDictionary.Values.Max()).Key.ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append($"Total exercising hours: {totalalExercisingHours}\n")
              .Append($"Average time spent exercising: {averageExercising} min.\n")
              .Append($"Favourite type: {favoritetype}\n");

            Console.WriteLine(sb.ToString());
        }

        public void OtherHobbiesStatistics(User user)
        {
            Console.Clear();

            //var allOtherHobbies = user.ListOfActivities.OfType<OtherHobbies>().ToList();

            var allOtherHobbies = user.OtherHobbiesActivities;

            if (!ValidationHelpers.CheckIfListIsEmpty(allOtherHobbies, "other hobbies statistics")) return;

            var totalOtherHobbiesHours = allOtherHobbies.Sum(hours => hours.TrackedTime.Minutes);
            var namesOfHobbies = allOtherHobbies.Select(names => names.Hobby).ToList();

            Console.WriteLine($"Total hours spent in your hobbies: {totalOtherHobbiesHours}");
            Console.WriteLine("Your hobbies:");
            foreach (var hobby in namesOfHobbies.Distinct())
            {
                Console.WriteLine($"-{hobby}");
            }
        }

        public void GeneralStatistics(User user)
        {
            Console.Clear();

            var totalTimeOfAllAc = user.ListOfActivities.Sum(hours => hours.TrackedTime.Minutes);

            // Favourite Activity

            int reading = user.ListOfActivities.Where(x => x.ActivityType == ActivityType.Reading).Count();
            int working = user.ListOfActivities.Where(x => x.ActivityType == ActivityType.Working).Count();
            int exercising = user.ListOfActivities.Where(x => x.ActivityType == ActivityType.Exercising).Count();
            int otherHobbies = user.ListOfActivities.Where(x => x.ActivityType == ActivityType.OtherHobbies).Count();

            var activitiesDictionary = new Dictionary<ActivityType, int>()
            {
                {ActivityType.Reading, reading },
                {ActivityType.Working, working },
                {ActivityType.Exercising, exercising },
                {ActivityType.OtherHobbies, otherHobbies },
            };

            var favoriteActivity = activitiesDictionary.FirstOrDefault(type => type.Value == activitiesDictionary.Values.Max()).Key.ToString();

            Console.WriteLine($" Total time of all your activities: {totalTimeOfAllAc}\n Favorite activity: {favoriteActivity}");
        }
            
    }
}


