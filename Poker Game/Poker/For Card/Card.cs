using Poker.Enum_Files;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Poker
{
    internal class Card
    {       
        public CardValue Value { get; private set; }
        public CardSuit Suit { get; private set; }       

        public Card(CardValue value, CardSuit suit) 
        {            
            Value = value;
            Suit = suit;           
        }       

        public override string ToString() => $"[{Value.RusName()}{Suit.PrintSymbol() }]";

    }
}
