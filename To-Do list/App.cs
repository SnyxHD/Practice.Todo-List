using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace To_Do_list
{
    internal static class App
    {
        static string menuSelect;
        static string inputString;
        static string dir = Environment.CurrentDirectory + @"\Data\";
        static string Path = Environment.CurrentDirectory + "\\Data\\list.json";
        static string menuMessage = "--------- Menu ---------";
        static ConsoleColor color = ConsoleColor.White;
        public static bool isRunning = true;
        static List<string> todoList = new List<string>();

        static App()
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);

            }
            if (!File.Exists(Path))
            {
                File.WriteAllText(Path, "[]");
            }

            LoadJsonToList();
        }

        internal static void Run()
        {
            List<string> menuOptions = new List<string> { "Add To List", "Read List", "Remove From List", "Save & Quit" };
            while (isRunning)
            {
                menuSelect = ConsoleUtils.PrintMenu(menuMessage, menuOptions, true, color);
                menuMessage = "--------- Menu ---------";
                color = ConsoleColor.White;

                switch (menuSelect)
                {
                    case "Add To List":
                        Console.Write("\nAdd to List: ");
                        inputString = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(inputString))
                        {
                            addList(inputString);
                            menuMessage = "--- Added Successfully! ---";
                            color = ConsoleColor.Green;
                        }
                        else { Console.Clear(); menuMessage = "- Cant Add Empty String to List -"; color = ConsoleColor.Red; }
                        break;

                    case "Read List":
                        /*printList(todoList);*/
                        if (todoList.Count() != 0)
                        {
                            ConsoleUtils.PrintMenu("------ ToDo List ------\n", todoList);
                            Console.ReadLine();
                            break;
                        }
                        menuMessage = "--- Nothing In List ---";
                        break;

                    case "Remove From List":
                        if (todoList.Any()) // Checks if todolist is empty and if its not empty, try to remove item at x index. Otherwise Print out Error.
                        {
                            ConsoleUtils.PrintMenu("--- Remove From List ---\n" +
                                "Type 'all' to remove all items from the list.\n", todoList);
                            Console.Write("-> ");
                            try
                            {
                                string rmIndex = Console.ReadLine();
                                if(rmIndex == "all")
                                {
                                    todoList.Clear();
                                    menuMessage = "--- Removed All Successfully! ---";
                                    break;
                                }
                                int rmLine = int.Parse(rmIndex);
                                if (rmLine > 0 && rmLine <= todoList.Count())
                                {
                                    todoList.RemoveAt(rmLine - 1);
                                    menuMessage = "--- Removed Successfully! ---";
                                    color = ConsoleColor.Green;
                                }
                                else
                                {
                                    menuMessage = "Error! Index out of Range.";
                                    color = ConsoleColor.Red;
                                }
                            }
                            catch (FormatException)
                            {
                                menuMessage = "Error! Please Enter a valid Number.";
                                color = ConsoleColor.Red;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error!\n{ex.Message}");
                                Console.ReadLine();
                            }
                            break;
                        }
                        menuMessage = "--- Nothing In List ---";
                        break;

                    case "Save & Quit":
                        isRunning = false;
                        SaveJsonToFile(todoList, true);
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Quitting Application...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void SaveJsonToFile(List<string> list, bool prettyPrint = false)
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = prettyPrint;
            string jsonString = JsonSerializer.Serialize(todoList, options);

            File.WriteAllText(Path, jsonString);
        }

        static void LoadJsonToList()
        {
            var jsonData = File.ReadAllText(Path);
            if (jsonData != "")
            {
                todoList = JsonSerializer.Deserialize<List<string>>(jsonData);
            }
        }

        static void addList(string item)
        {
            todoList.Add(item);
        }

        static List<string> getList()
        {
            return todoList;
        }

        static void printList(List<string> list)
        {
            int id = 0;
            if (todoList.Count() == 0)
            {
                Console.Clear(); Console.WriteLine("Nothing in List!" +
                "\n\nPress Enter To Continue..."); return;
            }

            foreach (string item in getList())
            {
                id++;
                Console.WriteLine(id + ". " + item);
            }
        }
    }
}
