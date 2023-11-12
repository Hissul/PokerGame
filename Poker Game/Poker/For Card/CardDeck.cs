using Poker.Enum_Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    internal class CardDeck
    {
        Random rand = new Random(); // для перемешивания колоды

        // количество карт в колоде + 2 джокера
        public static int card_count = (Enum.GetValues(typeof(CardValue)).Length -1)* (Enum.GetValues(typeof(CardSuit)).Length -1) + 2;

        private Card[] deck;

        public CardDeck()
        {           
            List<Card> spades = GetCards(CardSuit.Spades);
            spades.AddRange(GetCards(CardSuit.Clubs));
            spades.AddRange(GetCards(CardSuit.Hearts));
            spades.AddRange(GetCards(CardSuit.Diamonds));
            spades.Add(new Card(CardValue.Joker , CardSuit.NoName));
            spades.Add(new Card(CardValue.Joker, CardSuit.NoName)); 

            deck = spades.ToArray();
        }

        private List<Card> GetCards(CardSuit suit) // получаем массив карт по мастям
        {
            List<Card> cards = new List<Card>();

            for (int i = 0; i < Enum.GetValues(typeof(CardValue)).Length - 1; ++i)
            {
                cards.Add(new Card((CardValue)i, suit));
            }

            return cards;
        }       

        // индексатор   
        public Card this[int index]
        {
            get => deck[index];
            set => deck[index] = value;
        }

        // нужна только во время написания кода
        public override string ToString() 
        {
            string tmp = "";
            for (int i = 0; i < deck.Length; ++i)
            {
                if (i % 13 == 0)
                    tmp += "\n";
                tmp += deck[i].ToString();
                tmp += "\t";
            }
            return tmp;
        }

        // перемешиваем колоду
        public void ShakeDeck() 
        {
            for (int i = deck.Length - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                Card tmp = deck[j];
                deck[j] = deck[i];
                deck[i] = tmp;
            }
        }

    }
}
