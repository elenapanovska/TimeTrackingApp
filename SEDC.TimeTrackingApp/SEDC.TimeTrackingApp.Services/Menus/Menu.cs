using SEDC.TimeTrackingApp.Hm.Domain.Enums;
using SEDC.TimeTrackingApp.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SEDC.TimeTrackingApp.Services.Menus
{
    public class Menu
    {
        public int ChooseOnMenu<T>(List<T> list)
        {
            //int choice;
            while (true)
            {
                Console.WriteLine("Enter a number to choose");
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.) {list[i]} ");
                }

                int choice = ValidationHelpers.ParseNumber(Console.ReadLine(), list.Count);

                if (choice == -1)
                {
                    MessageHelepers.Message("You've entered something wrong! Try again!", ConsoleColor.Red);
                    Thread.Sleep(2000);
                    continue;
                }
                else
                {
                    return choice;
                }
            }
        }

        public int LogInMenu()
        {
            List<string> list = new List<string>() { "Log in", "Register", "Exit" };
            return ChooseOnMenu(list);
        }

        public int MainMenu()
        {
            List<string> list = new List<string>() {  "Track your reading", "Track your exercising", "Track your working", 
                                                    "Track your other hobbies", "See Statistics","Account", "Log Out"};
            return ChooseOnMenu(list);
        }

        public int ShowBookTypes()
        {
            List<string> list = new List<string>() { "BellesLettres", "Fiction", "Professional Literature" };
            return ChooseOnMenu(list);
        }

        public int ShowExercisingTypes()
        {
            List<string> list = new List<string>() { "General", "Running", "Sport" };
            return ChooseOnMenu(list);
        }

        public int ShowWorkingOptions()
        {
            List<string> list = new List<string>() { "Home", "Office" };
            return ChooseOnMenu(list);
        }

        public int AccountMenu()
        {
            List<string> list = new List<string>() {"Change info", "Change password", "Deactive account", "<= Back"};
            return ChooseOnMenu(list);
        }

        public int StatisticsMenu()
        {
            List<string> list = new List<string>() { "Reading", "Working", "Exercising", "Other hobbies", "General", "<= Back" };
            return ChooseOnMenu(list);
        }
    }
}


