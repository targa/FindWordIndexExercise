using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using FindWordPositionPuzzle;
using System.Threading.Tasks;

namespace FindWordPositionPuzzleTests
{
   
    [TestClass]

    public class WordSearcherTests
    {

        /*
         * Using the NSubstitute framework to mimick the webservice behavior without actually using it.
         * In my opinion this is kinda dirty, but works for my purposes for now...
         */
        WordSearcher SetUpFakeWordSearcher()
        {
            var wordSearcher = Substitute.ForPartsOf<WordSearcher>();

            Task<string> t0 = new Task<string>(() => { return null; });
            t0.Start();

            Task<string> t1 = new Task<string>(() => { return "AAAAA"; });
            t1.Start();

            Task<string> t2 = new Task<string>(() => { return "BBBBB"; });
            t2.Start();

            Task<string> t3 = new Task<string>(() => { return "CCCCC"; });
            t3.Start();

            Task<string> t4 = new Task<string>(() => { return "DDDDD"; });
            t4.Start();

            Task<string> t5 = new Task<string>(() => { return "EEEEE"; });
            t5.Start();

            Task<string> t6 = new Task<string>(() => { return "FFFFF"; });
            t6.Start();

            Task<string> t7 = new Task<string>(() => { return "GGGGG"; });
            t7.Start();

            Task<string> t8 = new Task<string>(() => { return "HHHHH"; });
            t8.Start();

            Task<string> t9 = new Task<string>(() => { return "IIIII"; });
            t9.Start();

            Task<string> t10 = new Task<string>(() => { return "JJJJJ"; });
            t10.Start();

            wordSearcher.GetWordAsync(Arg.Any<int>()).Returns(t0);

            wordSearcher.GetWordAsync(Arg.Is(0)).Returns(t1);
            wordSearcher.GetWordAsync(Arg.Is(1)).Returns(t2);
            wordSearcher.GetWordAsync(Arg.Is(2)).Returns(t3);
            wordSearcher.GetWordAsync(Arg.Is(3)).Returns(t4);
            wordSearcher.GetWordAsync(Arg.Is(4)).Returns(t5);
            wordSearcher.GetWordAsync(Arg.Is(5)).Returns(t6);
            wordSearcher.GetWordAsync(Arg.Is(6)).Returns(t7);
            wordSearcher.GetWordAsync(Arg.Is(7)).Returns(t8);
            wordSearcher.GetWordAsync(Arg.Is(8)).Returns(t9);
            wordSearcher.GetWordAsync(Arg.Is(9)).Returns(t10);

            return wordSearcher;
        }

        [TestMethod]
        public void FindFirstElement()
        {
            WordSearcher wordSearcher = SetUpFakeWordSearcher();
            Task<int> task = wordSearcher.FindWord("AAAAA");
            task.Wait();
            Assert.AreEqual(0, task.Result);
        }

        [TestMethod]
        public void FindLastElement()
        {
            WordSearcher wordSearcher = SetUpFakeWordSearcher();
            Task<int> task = wordSearcher.FindWord("JJJJJ");
            task.Wait();
            Assert.AreEqual(9, task.Result);
        }

        [TestMethod]
        public void FindMiddleElement()
        {
            WordSearcher wordSearcher = SetUpFakeWordSearcher();
            Task<int> task = wordSearcher.FindWord("EEEEE");
            task.Wait();
            Assert.AreEqual(4, task.Result);
        }

        [TestMethod]
        public void NonExistentElement()
        {
            WordSearcher wordSearcher = SetUpFakeWordSearcher();
            Task<int> task = wordSearcher.FindWord("QWERTY");
            task.Wait();
            Assert.AreEqual(-1, task.Result);
        }

        [TestMethod]
        public void CrazyInput()
        {
            WordSearcher wordSearcher = SetUpFakeWordSearcher();
            Task<int> task = wordSearcher.FindWord(" @$%@ 4%@ 039()/fa ");
            task.Wait();
            Assert.AreEqual(-1, task.Result);
        }

        [TestMethod]
        public void DeadCatCount()
        {
            var wordSearcher = new WordSearcher();
            wordSearcher.GetWordAsync(0).Wait();
            wordSearcher.GetWordAsync(1).Wait();
            wordSearcher.GetWordAsync(2).Wait();
            Assert.AreEqual(3,CatHandler.DeadCatCount);
        }
    }
}
