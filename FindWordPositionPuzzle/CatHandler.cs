using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindWordPositionPuzzle
{
    public static class CatHandler
    {
        private static int _deadCatCount = 0;

        public static int DeadCatCount
        {
            get { return _deadCatCount; }
        }

        public static void MurderCat()
        {
            _deadCatCount++;
        }

        public static void ResetDeadCatCounter()
        {
            _deadCatCount = 0;
        }

    }
}
