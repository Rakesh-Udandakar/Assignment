using System;
using System.Collections.Generic;
using System.Numerics;

namespace Word2NumberConverter
{
    class Program
    {
        static void Main(string[] args)
        {


            ShortScaleWord2NumberConverter word2NumberConverter = new ShortScaleWord2NumberConverter();

            while (true)
            {
                Console.WriteLine("Enter your words for number conversion: ");

                // Input for conversion
                var input = Console.ReadLine();

                try
                {
                    BigInteger result = word2NumberConverter.ConvertWordsToNumber(input);
                    Console.WriteLine("Result for " + input + ":" + result);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Exception for " + input + ":" + ex.Message);
                }
               
            }
        }
    }
}
