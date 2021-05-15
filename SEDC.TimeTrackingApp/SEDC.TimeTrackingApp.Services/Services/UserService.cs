using SEDC.TimeTrackingApp.Hm.Domain.Database;
using SEDC.TimeTrackingApp.Hm.Domain.Entities;
using SEDC.TimeTrackingApp.Hm.Domain.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SEDC.TimeTrackingApp.Services.Helpers;
using SEDC.TimeTrackingApp.Hm.Domain.Enums;
using System.Threading;

namespace SEDC.TimeTrackingApp.Services.Services
{
    public class UserService<T> : IUserService<T> where T :  User
    {
        private IDatabase<User> db;
        private ActivityServices<BaseActivity> activityServices = new ActivityServices<BaseActivity>();

        public  UserService()
        {
            db = new Database<User>();
        }
        public void ChangeInfo(int userId, string firstName, string lastName)
        {
            var user = db.GetUserById(userId);

            if (ValidationHelpers.ValidateFirstAndLastName(firstName, lastName) == null)
            {
                MessageHelepers.Message("You've entered somethig wrong. The first and last name should not be shorter than 2 characters " +
                                   "and they slould not conatin any numbers!", ConsoleColor.Red);
                return;

            }
            user.FirstName = firstName;
            user.LastName = lastName;
            db.UpdateUser(user);
            MessageHelepers.Message("You succesfully changed your first and last name!", ConsoleColor.Green);
        }

        public void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = db.GetUserById(userId);
            if (user.Password == oldPassword && oldPassword != newPassword)
            {
                if(ValidationHelpers.ValidatePassword(newPassword) == null)
                {

                   MessageHelepers.Message("Password should not be shorter than 6 characters, should contain at least one capital letter" +
                                           "and should contain at least one number", ConsoleColor.Red);
                    Thread.Sleep(3000);
                    return;
                }
            }
            else
            {
                MessageHelepers.Message("You entered your old password wrong or you new password cannot be your old password!",ConsoleColor.Red);
                Thread.Sleep(3000);
                return;
            }
            user.Password = newPassword;
            db.UpdateUser(user);
            MessageHelepers.Message("You succesfully changed your password!", ConsoleColor.Green);
        }

        public bool DeactivateAccount(User user)
        {
            Console.WriteLine("Are you sure you want to deactivate your account? y/n" );
            string choice = Console.ReadLine();
            if (choice == "y")
            {
                user.IsActive = false;
                MessageHelepers.Message("Your account has been deacivated", ConsoleColor.Green);
                return true;
            }
            return false;
        }

        public User LogIn(string username, string password)
        {
            var users = db.GetAll();
            User user = null;

            if(!ValidationHelpers.DoesUserNameExist(users, username))
            {
                for (int i = 1; i <= 3; i++)
                {
                    MessageHelepers.Message($"Wrong username you have 3 attepts otherwise the app wil close", ConsoleColor.Red);
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                    if (ValidationHelpers.DoesUserNameExist(users, username))
                    {
                        break;
                    }
                    if (i == 3 && !ValidationHelpers.DoesUserNameExist(users, username))
                    {
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                    }
                }
            }
            Console.Clear();
            user = users.FirstOrDefault(u => u.Username == username);

            if (!ValidationHelpers.ThreeAttemptsPass(user, password))
            {
                Thread.Sleep(2000);
                Environment.Exit(0);
            }
            Console.Clear();
            

            if(user.IsActive == false)
            {
                Console.WriteLine("Your account is deactivated! Do you want to activate it? y/n");
                string choice = Console.ReadLine();
                if(choice == "y")
                {
                    user.IsActive = true;
                    MessageHelepers.Message("Your account is now active!", ConsoleColor.Green);
                    MessageHelepers.Message("You succesfully logged in!", ConsoleColor.Green);
                    return user;
                }
                else
                {
                    MessageHelepers.Message("You accout is still deactivated!", ConsoleColor.Red);
                    return null;
                }
            }
            MessageHelepers.Message("You succesfully logged in!", ConsoleColor.Green);
            return user;
        }

        public User Register(T user)
        {
            if (ValidationHelpers.ValidateFirstAndLastName(user.FirstName, user.LastName) == null
                || ValidationHelpers.ValidateAge(user.Age) == -1
                || ValidationHelpers.ValidateUsername(user.Username) == null 
                || ValidationHelpers.ValidatePassword(user.Password) == null)
            {
                MessageHelepers.Message("You have entered something wrong!", ConsoleColor.Red);
                Console.ReadLine();
                return null;
            }
                
            int id = db.Insert(user);
            Console.Clear();
            return db.GetUserById(id);
        }

        public void  SeeStatistics(User user, int choice)
        {
            switch (choice)
            {
                case 1: //Reading
                    activityServices.ReadingStatistics(user);
                    break;
                case 2: //Working
                    activityServices.WorkingStatistics(user);
                    break;
                case 3: // Exercising
                    activityServices.ExercisingStatistics(user);
                    break;
                case 4: // Other hobbies
                    activityServices.OtherHobbiesStatistics(user);
                    break;
                case 5: // General
                    activityServices.GeneralStatistics(user);
                    break;
                default:
                    break;
            }
        }

        public bool AccountSettings(int id, int choice, User user)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter your new first and last name!");
                    Console.Write("Fisrt name: ");
                    string fisrtName = Console.ReadLine();
                    Console.Write("Last name: ");
                    string lastName = Console.ReadLine();
                    ChangeInfo(id, fisrtName, lastName);
                    break;
                case 2:
                    Console.WriteLine("Enter your old password");
                    Console.Write("Old password: ");
                    string oldPassword = Console.ReadLine();
                    Console.WriteLine("Enter your new password");
                    Console.Write("New password: ");
                    string newPassword = Console.ReadLine();
                    ChangePassword(id, oldPassword, newPassword);
                    break;
                case 3:
                    if(DeactivateAccount(user))
                    {
                        return true;
                    }
                    break;
                case 4:
                    break;
            }
            return false;
        }

        public void AddActivity<F>(User user, F activity, List<F> list) where F : BaseActivity
        {
            list.Add(activity);
            db.UpdateUser(user);
        }
    }
}
