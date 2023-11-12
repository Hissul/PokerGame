using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    static class CardSuitExtensions
    {
        private static char[] symbols = { '\u2664' , '\u2667' , '\u2665' , '\u2666' , '\0' };

        public static char PrintSymbol(this CardSuit suit) => symbols[(int)suit];
    }
}
