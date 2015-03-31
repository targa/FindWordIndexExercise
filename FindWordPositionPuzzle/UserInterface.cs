using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindWordPositionPuzzle
{
    /// <summary>
    /// Class that prints out the user inteface. Static for the sake of simplicity.
    /// </summary>
    static class UserInterface
    {
        public static void ShowUserInterface()
        {
            WordSearcher wordSearcher = new WordSearcher();
            Console.Clear();
            Console.Write("Type the word that you want to search: ");
            String word = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Searching...");
            Console.WriteLine();
            Task<int> task = wordSearcher.FindWord(word.ToUpper().Trim());
            task.Wait();
            if (task.Result == -1)
                Console.Write("Word not found... YOU MONSTER!");
            else
                Console.Write("Word found on index: " + task.Result);
            Console.WriteLine();
            Console.Write("Dead cats: " + CatHandler.DeadCatCount+" :(");
            Console.WriteLine();
            Console.Write("(Type any Key to search again)");
            Console.ReadKey();
            WordSearcher.ResetSearch();
            ShowUserInterface();
        }
    }
}
