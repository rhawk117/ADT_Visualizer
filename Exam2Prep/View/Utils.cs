using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exam2Prep.View
{
    public static class Utils
    {
        // this is not a utils class this ( wink wink )
        public static void StartUp()
        {
            HugeDisclaimer();
            questionaire();
        }
        private static void promptText(string text, int delay = 1500)
        {
            Console.WriteLine(text);
            Thread.Sleep(delay);
        }
        private static void HugeDisclaimer()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            promptText("[ ! ] DISCLAIMER (please read) [ ! ]");
            Console.ResetColor();
            promptText("[ i ] I made this program / UI to help study for Exam 2.");
            promptText("[ i ] I SOLELY made the UI itself (everything in the 'View' folder");
            promptText("[ i ] The rest / ADTs themselves were made by Professor Dowell ");
            promptText("[ i ] If there are any issues or bugs let me know and I'll address them.");
            promptText("[ i ] This program is not perfect and I created it as a fun way to study.");
            promptText("[ i ] I hope you find it helpful");
            Console.WriteLine("[ Press ENTER to continue ... ]");
            Console.ReadLine();
        }

        private static void questionaire()
        {
            promptText("[ Let's see if you were paying attention... ]", 1000);
            bool isUserIntelligent = question(" QUESTION 1 Did I make the data structures", new string[] { "Yes", "No, Dowell did", "A Unicorn did", "I cannot read" }, 'b');
            if (!isUserIntelligent)
            {
                Incorrect();
            }
            Correct();
            bool isUserSmart = question("Who made the UI? ", new string[] { "Candice", "Mike Hawk", "You Made the UI", "bob" }, 'c');
            if (!isUserSmart)
            {
                Incorrect();
            }
            Correct();
        }

        private static bool question(string prompt, string[] choices, char ans)
        {
            char[] options = { 'a', 'b', 'c', 'd' };
            Console.WriteLine($"[ ? ] {prompt}");
            for (int i = 0; i < choices.Length; i++)
            {
                Console.WriteLine($"[ {options[i]} ] {choices[i]}");
            }
            char c = Console.ReadKey().KeyChar;
            if (options.Contains(char.ToLower(c)))
            {
                return c == ans;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\a\a[ X ] Provide a Valid Response [ X ]");
                Console.ResetColor();
                return question(prompt, choices, ans);
            }
        }
        private static void Incorrect()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            throw new UnauthorizedAccessException("\a\a\a\a\a\a\a\adude did you read the disclaimer??");
        }
        private static void Correct()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            promptText("[ Correct! ]", 2000);
            Console.ResetColor();
        }
    }
}
