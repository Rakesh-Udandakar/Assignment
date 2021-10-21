using System;
using System.Collections.Generic;
using System.Text;

namespace HomeQuiz
{
    class Program
    {
        public static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Enter your input seperated by comma as 1,2,3 : ");

                // Input for conversion
                string input = Console.ReadLine();

                string[] values = input.Split(",");
                try
                {
                    int[] inputArray = Array.ConvertAll(values, s => int.Parse(s));
                    int result = TrappedRainWaterCalculator.CalculateTrappedRainWaterUnits(inputArray);
                    Console.WriteLine("Result for " + input + " is " + result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception for " + input + " is " + ex.Message);
                }

            }
        }
    }
}
