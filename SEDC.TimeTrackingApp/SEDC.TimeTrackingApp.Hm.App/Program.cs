using SEDC.TimeTrackingApp.Hm.Domain.Entities;
using SEDC.TimeTrackingApp.Hm.Domain.Enums;
using SEDC.TimeTrackingApp.Services.Helpers;
using SEDC.TimeTrackingApp.Services.Menus;
using SEDC.TimeTrackingApp.Services.Services;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SEDC.TimeTrackingApp.Hm.App
{
    class Program
    {
        public static Menu menus = new Menu();
        public static User currentUser = new User();
        public static IUserService<User> userService = new UserService<User>();
        public static ActivityServices<BaseActivity> appServices = new ActivityServices<BaseActivity>();
        
        static void Main(string[] args)
        {
            //UserData();
            while (true)
            {
                int userChoice = menus.LogInMenu();
                Console.Clear();
                switch (userChoice)
                {
                    case 1:
                        Console.Write("Enter username: ");
                        string username = Console.ReadLine();
                        Console.Write("Enter password: ");
                        string password = Console.ReadLine();

                        currentUser = userService.LogIn(username, password);
                        break;
                    case 2:
                        Console.WriteLine("Enter the folowing to register:");

                        Console.WriteLine("First name:");
                        string firstName = Console.ReadLine();
                        Console.WriteLine("Last name:");
                        string lastName = Console.ReadLine();
                        Console.WriteLine("Age:");
                        int age = ValidationHelpers.ParseNumber(Console.ReadLine(), 120);
                        Console.WriteLine("Username:");
                        string registerUserame = Console.ReadLine();
                        Console.WriteLine("Password:");
                        string registerPassword = Console.ReadLine();

                        var user = new User(firstName, lastName, age, registerUserame, registerPassword);
                        userService.Register(user);
                        MessageHelepers.Message("You succesfully registered!", ConsoleColor.Green);

                        currentUser = user;
                        if (currentUser == null) continue;
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
                if (currentUser == null) continue;
                bool isLoggedIn = true;
                while (isLoggedIn)
                {
                    Console.WriteLine($"Hi {currentUser.FirstName} choose one of the following?");
                    int choice = menus.MainMenu();
                    ActivityType currentActivity = (ActivityType)choice;
                    Console.Clear();
                    switch (choice)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            appServices.TrackingTime(currentActivity, currentUser, userService);
                            break;
                        case 5:
                           if(!ValidationHelpers.CheckIfListIsEmpty(currentUser.ListOfActivities, "statistics")) continue;
                            int statisticsMenu = menus.StatisticsMenu();
                            userService.SeeStatistics(currentUser, statisticsMenu);
                            break;
                        case 6:
                            int accountMenu = menus.AccountMenu();
                            if(userService.AccountSettings(currentUser.Id, accountMenu, currentUser))
                            {
                                isLoggedIn = !isLoggedIn;
                            }
                            break;
                        case 7:
                            isLoggedIn = !isLoggedIn;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static void  UserData()
        {
            userService.Register(new User("Bob", "Bobsky", 20, "bobsky", "Bobsky123"));
            userService.Register(new User("John", "Smith", 25, "smith", "Smith123")); 
        }

    }
}
