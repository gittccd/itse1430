using System;

namespace HelloWorld
{
    class Program
    {

       static void Main ( string[] args )
        {
            string name = "";

            //string if = "";

            //Definitely assigned
            //name = "Bob";
            string name2 = Console.ReadLine();
            //name2 = Console.ReadLine();

            name2 = name = "Sue";

            Console.WriteLine(name);

            Console.WriteLine ("Hello World!");


            int hours = 8;
            double payRate = 15.25;

            double totalPay = payRate * hours;

            string fullName = "Fred" + " " + "Jones";
        }
    }
}
