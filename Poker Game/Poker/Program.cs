﻿/*
# Автомат для игры в покер без противника

Игрок приходит в казино со 100$. Чтобы сыграть, игрок делает ставку, минимум 1$, максимум все его деньги. Автомат списывает столько денег со счёта игрока.

Автомат создаёт колоду из 52 карт (Т, К, Д, В, 10, 9, 8, 7, 6, 5, 4, 3, 2, без джокеров). У каждой карты есть масть и достоинство. Используйте символы Юникода, чтобы выводить масти. Автомат тасует колоду: расставляет все карты в случайном порядке. Автомат следит за картами колоды, чтобы они не удваивались и не пропадали. Карты можно только перемещать из одной стопки в другую.

Автомат сдаёт игроку руку из 5 верхних карт. Игрок смотрит на свои карты и решает, какие из них он заменит. Заменяемые карты уходят в сброс. Автомат сдаёт новые карты с верха колоды взамен их.

Автомат определяет, какая наилучшая комбинация у игрока на руке, и делает соответствующую выплату игроку.


Программа показывает текущий баланс игроку и предлагает снова сделать ставку. Игрок может отказаться и выйти из игры, если введёт специальное слово “quit”.



### Необязательное усложнение: джокеры

Включите в колоду два джокера. Джокер может быть любой картой по выбору игрока. Запрограммируйте один из вариантов:

1. Игрок должен вручную ввести, чему равен каждый из его джокеров — какой масти и какого достоинства карта, ИЛИ
2. Игрок не вводит, чему равны джокеры. Программа автоматически определяет, чему должны равняться джокеры, чтобы максимизировать прибыль игрока.



### Необязательное усложнение: таблица чемпионов

Программа хранит список 10 игроков с наибольшим счётом в файле champions.txt. Когда игрок покидает игру, программа смотрит, сколько у него денег. Если у него достаточно денег, чтобы попасть в таблицу чемпионов, он вставляется в список на нужную строку, смещая всех игроков с меньшим счётом ниже. Один игрок будет вытеснен из таблицы.

Пример содержимого файла champions.txt:


Иван,235
Мария,199
Антон,175
Кирилл,150
Ольга,134


Программа загружает содержимое таблицы, когда ей нужно показать таблицу или сравнить с результатами игрока. Если таблица обновилась, программа перезаписывает файл.

Изначально таблица пустая. Любого результата достаточно, чтобы попасть в неё.
*/




using Poker.Enum_Files;
using System.Text;

namespace Poker
{
    internal class Program
    {
        static void Main(string[] args)
        {    
            //для правильного отображения значков юникода
            Console.OutputEncoding = Encoding.Unicode;

            try
            {
                TheGame game = new TheGame();
                game.LetPlay();
            }
            catch (ArgumentException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

        }
    }
}