using SEDC.TimeTrackingApp.Hm.Domain.Entities;
using SEDC.TimeTrackingApp.Hm.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SEDC.TimeTrackingApp.Services.Helpers
{
    public static class ValidationHelpers
    {
        public static int ParseNumber(string number, int range)
        {
            int parsedNum = 0;
            bool IsNumParsed = int.TryParse(number, out parsedNum);

            if (!IsNumParsed)
            {
                return -1;
            }
            if(parsedNum <= 0 || parsedNum > range)
            {
                return -1;
            }
            return parsedNum;
        }

        public static string ValidateUsername(string username)
        {
            if(username.Length < 5)
            {
                return null;
            }
            return username;
        }

        public static string ValidatePassword(string password)
        {
            if(password.Length < 6)
            {
                return null;
            }
            
            foreach(var letter in password)
            {
                if (Char.IsUpper(letter) && !Char.IsNumber(letter))
                {
                    return password;
                }
            }

            return null;
        }

        public static string ValidateFirstAndLastName(string firstName, string lastName)
        {
            if(firstName.Length < 2 && lastName.Length < 2)
            {
                return null;
            }

            foreach (var letter in firstName )
            {
                if(!Char.IsNumber(letter))
                {
                    return firstName;
                }
            }

            foreach (var letter in lastName)
            {
                if (!Char.IsNumber(letter))
                {
                    return lastName;
                }
            }

            return null; 
        }

        public static int ValidateAge(int age)
        {
            if(age < 18 && age > 120)
            {
                return -1;
            }
            return age;
        }

        public static bool ThreeAttemptsPass(User user, string password)
        {
            for (int i = 1; i <= 3; i++)
            {
                if (user.Password != password)
                {
                    MessageHelepers.Message("Wrong password you have 3 attepts otherwise the app wil close", ConsoleColor.Red);
                    Console.Write("Password: ");
                    password = Console.ReadLine();

                    if (i == 3 && user.Password != password)
                    {
                        return false;
                    };
                }
            }
            return true;
        }

        public static bool DoesUserNameExist<T>(List<T> users, string username) where T : User
        {
            foreach (var userN in users)
            {
                if (userN.Username == username)
                {
                    return true;
                }
            }
            return false;

        }

        public static int GetFavouriteType<T>(T type, List<T> listOfactivities ) where T : BaseActivity
        {
            int count = listOfactivities.Where(x => x.Equals(type)).Count();

            return count;
        }

        public static bool CheckIfListIsEmpty<T>(List<T> list, string activity)
        {
            if(list.Count == 0)
            {
                MessageHelepers.Message($"Sorry your {activity} are empty because you haven't tracked any activity!", ConsoleColor.Red);
                return false;
            }
            return true;


        }
    }
}
