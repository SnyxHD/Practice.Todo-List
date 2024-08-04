using System;
using System.Collections.Generic;

namespace To_Do_list
{
    public static class ConsoleUtils
    {
        public static string PrintMenu(string message, List<string> options, bool returnChoice = false, ConsoleColor messageColor = ConsoleColor.White)
        {
            Console.Clear();
            Console.ForegroundColor = messageColor;
            Console.WriteLine(message);
            Console.ResetColor();

            for (int index = 0; index < options.Count; index++) { Console.WriteLine($"{index + 1}. {options[index]}"); }

            /*int optionIndex = int.Parse(Console.ReadLine()); */
            if (returnChoice)
            {
                int optionIndex = -1;
                while (optionIndex < 0 || optionIndex >= options.Count)
                {
                    Console.Write("\n-> ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int parsedIndex))
                    {
                        optionIndex = parsedIndex - 1;
                        if (optionIndex < 0 || optionIndex >= options.Count)
                        {
                            Console.Clear();
                            Console.WriteLine("Error! Please Select a number between 1 and " + options.Count);
                            for (int index = 0; index < options.Count; index++) { Console.WriteLine($"{index + 1}. {options[index]}"); }
                        }
                    }
                    else
                    {
                        /*Console.WriteLine("Error! Please enter a valid number.");*/
                        Console.Clear();
                        Console.WriteLine("Error! Please enter a valid number.");
                        for (int index = 0; index < options.Count; index++) { Console.WriteLine($"{index + 1}. {options[index]}"); }
                    }
                }
                return options[optionIndex];
            }
            return null;
        }
    }
}
