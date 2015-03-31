using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindWordPositionPuzzle
{
    public interface IWordRetriever
    {
        Task<String> GetWordAsync(int position);
    }
}
