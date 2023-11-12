using Poker.Enum_Files;
using System;
using System.Diagnostics.Metrics;

namespace Poker
{
    internal class TheGame
    {
        private static int copacity = 5;

        private CardDeck deck;
        private Player player;

        private Card[] player_hand; // карты игрока

        public int PlayDeckSize { get; private set; } = CardDeck.card_count;
        public int PlayerHandSize { get; private set; }

        public int Bet { get; private set; } = 0;

        // конструктор
        public TheGame()
        {
            Console.WriteLine("\t\t=> Игра Покер <=\n");
            this.deck = new CardDeck();
            this.player = new Player();

            this.player_hand = new Card[copacity];
        }

        // индексатор
        private Card this[int index] => player_hand[index];

        public void LetPlay()
        {           
            while (true)
            {
                Console.WriteLine("\n*****************************");
                Console.WriteLine("*                           *");
                Console.WriteLine("*  Игровое меню :           *");
                Console.WriteLine("*                           *");
                Console.WriteLine("*  1 - Лучшие игроки        *");
                Console.WriteLine("*  2 - Игра                 *");
                Console.WriteLine("*  3 - Выйти                *");
                Console.WriteLine("*                           *");
                Console.WriteLine("*****************************\n");

                int desition = int.Parse(Console.ReadLine());

                switch (desition)
                {
                    case 1:
                        player.ShowStatistic();
                        break;
                    case 2:
                        GameBody();
                        break;
                    case 3:
                        Console.WriteLine("\nЖаль что вы уходите...");
                        return;
                    default:
                        break;
                }
            }
        }

        // метод игры
        private void GameBody()
        {
            string stop_word = ""; // для выхода из игры
            int win = 0; // выигрыш   

            while (stop_word != "quit")
            {
                while (true) // проверка счета
                {
                    Console.Write("\nСделайте ставку (минимум 1$) : ");
                    Bet = int.Parse(Console.ReadLine());

                    if (Bet > player.Score) 
                        Console.WriteLine($"\nНе достаточно денег! Доступно {player.Score}\n");
                    else
                        break;
                }

                player.Score -= Bet; // снимаем деньги со счета игрока
                                     
                deck.ShakeDeck(); // перемешиваем колоду               

                CardDeck play_deck = new CardDeck(); // делаем копию текущей колоды   
                for (int i = 0; i < PlayDeckSize; i++)
                    play_deck[i] = deck[i];

                PlayerHandSize = 0; // обнуляем руку игрока

                GiveCards(play_deck);
                ShowPlayerHand();

                Console.WriteLine("\nНужна замена карт ?\n1 - Да\n2 - Нет\n");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:

                        Console.Write("\nУкажите количество карт для замены : ");
                        int change_count = int.Parse(Console.ReadLine());

                        for (int i = 0; i < change_count; i++) // замена карт 
                        {
                            Console.Write($"Укажите {i + 1} карту для замены : ");
                            int change = int.Parse(Console.ReadLine());

                            if (change < 0 || change > copacity)
                                throw new ArgumentException("Указывайте номер карты от 1 до 5");

                            player_hand[change - 1] = play_deck[0]; // меняем карту в руке

                            RemoveFromPlayDeck(play_deck, 1);
                        }

                        ShowPlayerHand();
                        break;
                   
                    default: break;
                }

                // проверка есть ли джокер 
                // и замена если есть
                if (CheckJoker()) 
                    ShowPlayerHand();

                win = Bet * Combinations();
                Console.WriteLine($"\nВаш выигрыш {win}\n");

                player.Score += win;
                Console.WriteLine($"Ваш счет  {player.Score}\n");

                Console.WriteLine("\nПродолжаем ?\nжмем ENTER - для продолжения\nquit - для выхода ");
                stop_word = Console.ReadLine();

                if(player.Score == 0) // игрок проиграл
                {
                    Console.WriteLine("Вы не можете продолжать игру. На вашем счету 0! Приходите как бедет деньги!");
                    break;
                }
            }

            player.SafeStatistic();
        }

        // раздача карт 
        private void GiveCards(CardDeck play_deck)
        {
            int counter = 0;
            for (int i = PlayerHandSize; i < copacity; ++i)
            {
                player_hand[i] = play_deck[i];
                ++counter;
            }
            PlayerHandSize += counter;
            RemoveFromPlayDeck(play_deck, counter);
        }

        // удаляем карты из колоды (которые вышли) ... удаление сверху
        private void RemoveFromPlayDeck(CardDeck play_deck, int counter)
        {
            for (int i = 0; i < counter; ++i)
            {
                for (int j = 0; j < PlayDeckSize - 1; ++j)
                    play_deck[j] = play_deck[j + 1];

                --PlayDeckSize;
            }
        }

        private void ShowPlayerHand()
        {
            Console.WriteLine("\n\t**********************************************************************");
            Console.WriteLine();
            ShowScoreAndBet();
            Console.Write("\n\t\t\t");
            for (int i = 0; i < PlayerHandSize; ++i)
            {
                Console.Write($"{player_hand[i],10}");
            }

            Console.WriteLine("\n\n\n\t**********************************************************************");
        }

        // счет и ставка игрока
        private void ShowScoreAndBet()
        {
            Console.WriteLine($"\tВаш счет : {player.Score,10}\n\tВаша ставка {Bet,9}");
        }

        // проверка есть ли джокер в руке
        private bool CheckJoker()
        {
            bool for_check = false;
            for (int i = 0; i < PlayerHandSize; ++i)
            {
                if (player_hand[i].Value == CardValue.Joker)
                {
                    player_hand[i] = ChangeJoker(); // замена джокера
                    for_check = true;
                }
            }
            
            return for_check;
        }

        // замена джокера
        private Card ChangeJoker()
        {
            Console.WriteLine("\nВ руке есть джокер. Замените его на любую карту.");
            int for_card_suit = 0;

            while (true)
            {
                Console.WriteLine("Выбери масть :\n1 - Пики\n2 - Крести\n3 - Черви\n4 - Бубны\n");
                for_card_suit = int.Parse(Console.ReadLine());

                if (for_card_suit >= 1 && for_card_suit <= 4)
                    break;
                Console.WriteLine("Вводите число от 1 до 4");
            }

            int for_card_value = 0;

            while (true)
            {
                Console.WriteLine("Введите значение карты : 1 - Двойка   2 - Тройка   3 - Четверка   4 - Пятерка\n\t\t\t 5 - Шестерка   6 - Семерка   7 - Восьмерка   8 - Девятка\n\t\t\t 9 - Десятка   10 - Валет   11 - Дама   12 - Король   13 -Туз\n");
                for_card_value = int.Parse(Console.ReadLine());

                if (for_card_value >= 1 && for_card_value <= 13)
                    break;

                Console.WriteLine("Вводите число от 1 до 13");
            }

            return new Card((CardValue)for_card_value - 1, (CardSuit)for_card_suit - 1);
        }

        // проверка на "Масть"
        private bool CheckForFlash()
        {
            IEnumerable<IGrouping<CardSuit, Card>> cards = player_hand.GroupBy(player_hand => player_hand.Suit);

            foreach (IGrouping<CardSuit, Card> card in cards)
            {
                if (card.Count() == 5)
                    return true;
            }
            return false;
        }

        // проверка на "Порядок"
        private bool ChecForStreet()
        {       
            // разница карт в отсортированой руке будет равна 4 
            // проверка на пару должна быть раньше
            if (player_hand[4].Value - player_hand[0].Value == 4)
                return true;

            else if (player_hand[0].Value == CardValue.Two && player_hand[1].Value == CardValue.Jack && player_hand[2].Value == CardValue.Queen && player_hand[3].Value == CardValue.King && player_hand[4].Value == CardValue.Ace)
                return true;

            else if (player_hand[0].Value == CardValue.Two && player_hand[1].Value == CardValue.Three && player_hand[2].Value == CardValue.Queen && player_hand[3].Value == CardValue.King && player_hand[4].Value == CardValue.Ace)
                return true;

            else if (player_hand[0].Value == CardValue.Two && player_hand[1].Value == CardValue.Three && player_hand[2].Value == CardValue.Four && player_hand[3].Value == CardValue.King && player_hand[4].Value == CardValue.Ace)
                return true;

            else if (player_hand[0].Value == CardValue.Two && player_hand[1].Value == CardValue.Three && player_hand[2].Value == CardValue.Four && player_hand[3].Value == CardValue.Five && player_hand[4].Value == CardValue.Ace)
                return true;

            return false;
        }

        // проверка комбинаций
        private int Combinations()
        {
            IEnumerable<Card> ordered_cards = player_hand.OrderBy(player_hand => player_hand.Value); // сортировка карт   

            player_hand = ordered_cards.ToArray();


            IEnumerable<IGrouping<CardValue, Card>> cards = player_hand.GroupBy(player_hand => player_hand.Value);

            int pair_counter = 0; // счетчик пар
            int triple_cointer = 0; // счетчик троек

            foreach (IGrouping<CardValue, Card> card in cards)
            {
                if (card.Count() == 2)
                    ++pair_counter;
                else if (card.Count() == 3)
                    ++triple_cointer;
                else if (card.Count() == 4)
                {
                    Console.WriteLine("\nВаша комбинация : Каре");
                    return 5;
                }
                else if (card.Count() == 5)
                {
                    Console.WriteLine("\nВаша комбинация : Покер");
                    return 20;
                }
            }

            if (triple_cointer == 1 && pair_counter == 1)
            {
                Console.WriteLine("\nВаша комбинация : Двойка и тройка");
                return 3;
            }
            else if (pair_counter == 2)
            {
                Console.WriteLine("\nВаша комбинация : Две пары");
                return 1;
            }
            else if (triple_cointer == 1)
            {
                Console.WriteLine("\nВаша комбинация : Тройка");
                return 2;
            }

            pair_counter = 0;

            foreach (IGrouping<CardValue, Card> card in cards) // для пары отдельный цикл ... исключаются пары значение которых меньше валета(вальта???) 
            {
                if (card.Count() == 2 && card.Key >= CardValue.Jack)
                {
                    Console.WriteLine("\nВаша комбинация : Пара");
                    return 1;
                }
                else if (card.Count() == 2 && card.Key < CardValue.Jack)
                {
                    Console.WriteLine("\nВаша комбинация : Пара (меньше валета)");
                    return 0;
                }
            }

            if (ChecForStreet() && CheckForFlash())
            {
                Console.WriteLine("\nВаша комбинация : Порядок в масти");
                return 10;
            }
            else if (CheckForFlash())
            {
                Console.WriteLine("\nВаша комбинация : Масть");
                return 4;
            }
            else if (ChecForStreet())
            {
                Console.WriteLine("\nВаша комбинация : Порядок");
                return 2;
            }

            return 0;
        }

    }
}
