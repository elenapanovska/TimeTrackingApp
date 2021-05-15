using SEDC.TimeTrackingApp.Hm.Domain.Enums;
using SEDC.TimeTrackingApp.Hm.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.TimeTrackingApp.Hm.Domain.Entities
{
    public class User : BaseEntity, IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public List<Reading> ReadingActivities { get; set; }
        public List<Working> WorkingActivities { get; set; }
        public List<Exercising> ExercisingActivities { get; set; }
        public List<OtherHobbies> OtherHobbiesActivities { get; set; }

        public List<BaseActivity> ListOfActivities { get; set; }

        public User(string firstName, string lastName, int age, string username, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Username = username;
            Password = password;
            ListOfActivities = new List<BaseActivity>();
            ReadingActivities = new List<Reading>();
            WorkingActivities = new List<Working>();
            ExercisingActivities = new List<Exercising>();
            OtherHobbiesActivities = new List<OtherHobbies>();
            IsActive = true;
        }

        public User()
        {
            IsActive = true;
            ListOfActivities = new List<BaseActivity>();
            ReadingActivities = new List<Reading>();
            WorkingActivities = new List<Working>();
            ExercisingActivities = new List<Exercising>();
            OtherHobbiesActivities = new List<OtherHobbies>();
        }
    }
}
