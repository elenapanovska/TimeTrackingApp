using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.TimeTrackingApp.Services.Helpers
{
    public static class MessageHelepers
    {
        public static void Message(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
