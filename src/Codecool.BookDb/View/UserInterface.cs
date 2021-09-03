using System;

namespace Codecool.BookDb.View
{
    public class UserInterface
    {
        public void PrintLn(Object obj)
        {
            Console.WriteLine(obj);
        }

        public void PrintTitle(string title)
        {
            Console.WriteLine("\n --" + title + " --");
        }

        public void PrintOption(char option, string description)
        {
            Console.WriteLine("(" + option + ")" + " " + description);
        }

        public char Choice(string options)
        {
            // Given string options -> "abcd"
            // keep asking user for input until one of provided chars is provided
            Console.WriteLine("Enter your option:");
            String inputString = Console.ReadLine();
            while (inputString.Length == 0 || !options.Contains(inputString))
            {
                Console.WriteLine("Invalid option, please retry:");
                inputString = Console.ReadLine();
            }
            return inputString[0];
        } 

        public string ReadString(string prompt, string defaultValue)
        {
            // Ask user for data. If no data was provided use default value.
            // User must be informed what the default value is.
            Console.WriteLine(prompt);
            String inputString = Console.ReadLine();
            if (inputString == string.Empty)
            {
                Console.WriteLine($"Default value is: {defaultValue}");
                return defaultValue;
            }
            else
            {
                return inputString;
            }
        }

        public DateTime ReadDate(string prompt, DateTime defaultValue)
        {
            // Ask user for a date. If no data was provided use default value.
            // User must be informed what the default value is.
            // If provided date is in invalid format, ask user again.
            Console.WriteLine(prompt);
            string inputString = Console.ReadLine();
            DateTime inputDate;
            while (!DateTime.TryParseExact(inputString, "yyyy.MM.dd", null, System.Globalization.DateTimeStyles.None, out inputDate))
            {
                if (inputString == string.Empty)
                {
                    Console.WriteLine($"Default value is: {defaultValue}");
                    return defaultValue;
                }
                else
                {
                    Console.WriteLine("Invalid date, please retry");
                    inputString = Console.ReadLine();
                }
            }
            return inputDate;
        }

        public void ReadAnyKey()
        {
            Console.ReadKey();
        }

        public int ReadInt(string prompt, int defaultValue)
        {
            // Ask user for a number. If no data was provided use default value.
            // User must be informed what the default value is.
            Console.WriteLine(prompt);
            String inputString = Console.ReadLine();
            int inputInt;
            while (!int.TryParse(inputString, out inputInt))
            {
                if (inputString == string.Empty)
                {
                    Console.WriteLine($"Default value is: {defaultValue}");
                    return defaultValue;
                }
                else
                {
                    Console.WriteLine("Invalid integer, please retry.");
                    inputString = Console.ReadLine();
                }
            }
            return inputInt;
        }
    }
}
