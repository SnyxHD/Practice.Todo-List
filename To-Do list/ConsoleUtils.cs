using System;
using System.Collections.Generic;

namespace To_Do_list
{
    public static class ConsoleUtils
    {
        public static string PrintMenu(string message, List<string> options, bool returnChoice = false, ConsoleColor messageColor = ConsoleColor.White)
        {
            // this method handles Printing the menu Options
            void PrintOptions()
            {
                for (int index = 0; index < options.Count; index++) { Console.WriteLine($"{index + 1}. {options[index]}"); }
            }

            // Handles Printing the Header Message and if the Messahe will ba a different color
            Console.Clear();
            Console.ForegroundColor = messageColor;
            Console.WriteLine(message);
            Console.ResetColor();

            PrintOptions();

            // if the user wants to return a choice
            // then it will try to parse the input to an int
            // if it fails it will clear the console and print an error message
            // if it succeeds it will return the option at the index
            if (returnChoice)
            {
                // the option index is set to -1 so that the while loop will run at least once
                // and if the input is not a valid number it will keep running until a valid number is entered
                int optionIndex = -1;
                while (optionIndex < 0 || optionIndex >= options.Count)
                {
                    Console.Write("\n-> ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int parsedIndex))
                    {
                        // the parsed index is subtracted by 1 to get the correct index
                        // and then it checks if the index is within the range of the options
                        optionIndex = parsedIndex - 1;
                        if (optionIndex < 0 || optionIndex >= options.Count)
                        {
                            Console.Clear();
                            Console.WriteLine("Error! Please Select a number between 1 and " + options.Count);
                            PrintOptions();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Error! Please enter a valid number.");
                        PrintOptions();
                    }
                }
                // returns the option at the index
                // this is used in the App.cs file to get the option that the user selected
                return options[optionIndex];
            }
            // returns null if the user does not want to return a choice
            return null;
        }
    }
}
