using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FindWordPositionPuzzle
{
    /// <summary>
    /// Class that will search for the given word in the provided webservice and return its index.
    /// It is a static class for the sake of simplicity.
    /// </summary>
    public class WordSearcher
    {
        /// <summary>
        /// The only method that needs to be called in order to search for the index of a given word.
        /// </summary>
        /// <param name="wordToBeFound">The word to search</param>
        /// <returns>Index of the word. returns -1 if the word wasn't found.</returns>
        public async Task<int> FindWord(string wordToBeFound)
        {
            int previousIndex = 0;
            int currentIndex = 2;
            string currentWord = string.Empty;
            while (true)
            {
                currentWord = await GetWordAsync(currentIndex);
                //we got out of bounds in the dictionary, so let's start the regular binary search with the currentIndex being the last possible result(right).
                if (currentWord == null)
                {
                    return await binarySearch(wordToBeFound, previousIndex, currentIndex);
                }

                // If found return its index
                if (currentWord == wordToBeFound)
                {
                    return currentIndex;
                }

                //we got past the word that we are looking for.. starting regular binary search in this segment of the dictionary
                if (currentWord.CompareTo(wordToBeFound) > 0)
                {
                    return await binarySearch(wordToBeFound, previousIndex, currentIndex);
                }
                else
                {
                    previousIndex = currentIndex;
                    // This is a workaround because we don't know the lenght of the dictionary.
                    // We keep looking exponentially to an element farther from the beginning.
                    currentIndex = currentIndex * currentIndex;
                }
            }
        }

        public static void ResetSearch()
        {
            //TODO: if we need to do any other cleanups, do it here.
            CatHandler.ResetDeadCatCounter();
        }

        /// <summary>
        /// Simple binary search algorithm. It is used internally after we found the segment of the dictionary that we want to search the word.
        /// </summary>
        /// <param name="wordToBeFound">The word that was given out by the user</param>
        /// <param name="left">Left index of the dicitonary segment that we are going to search for</param>
        /// <param name="right">Right index of the dicitonary segment that we are going to search for</param>
        /// <returns></returns>
        private async Task<int> binarySearch(string wordToBeFound, int left, int right)
        {

            var middle = (int)Math.Ceiling((decimal)((left + right) / 2));
            string currentWord = await GetWordAsync(middle);

            //found the word! return its index!
            if (currentWord == wordToBeFound)
            {
                return middle;
            }

            //word not found in the dictionary
            if (left == right || left > right)
            {
                return -1;
            }

            //checking if we should go upwards or downwards in the dictionary. If currentWord is null, we are out of bounds, so go downwards.
            if (wordToBeFound.CompareTo(currentWord) > 0 && currentWord != null)
            {
                return await binarySearch(wordToBeFound, middle + 1, right);
            }
            else
            {
                return await binarySearch(wordToBeFound, left, middle - 1);
            }
        }

        
        /// <summary>
        /// Method that will retrieve words from the provided webservice. Everytime we get a word from it, a cat dies :(
        /// </summary>
        /// <param name="position">Index of the word.</param>
        /// <returns>Word found by the given index.</returns>
        public virtual async Task<String> GetWordAsync(int position)
        {
            using (var client = new HttpClient())
            {
                CatHandler.MurderCat();
                client.BaseAddress = new Uri("http://teste.way2.com.br/");
                client.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage response = await client.GetAsync("dic/api/words/" + position);
                if (response.IsSuccessStatusCode)
                {
                    String word = await response.Content.ReadAsStringAsync();
                    return word;
                }

                return null;
            }
        }
    }
}
