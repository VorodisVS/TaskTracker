using System;
using ToastNotification;

namespace ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ToastNotification.ToastNotification r = new ToastNotification.ToastNotification();
            r.Show();

            Console.ReadLine();
        }
    }
}
