using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    internal class Player
    {
        private static string statistic_file = @"..\..\..\..\GameStatistic.txt";

        public string Name { get; }
        public int Score { get; set; }
        
        
        public Player() 
        {
            Console.Write("Введите ваше имя : ");
            Name = Console.ReadLine();
            Score = 100; // начальный счет
        }

        //public override string ToString() => $"{Name}";

        // просмотр статистики
        public void ShowStatistic()
        {            
            if (File.Exists(statistic_file)) // если файл существует - читаем его
            {
                using TextReader reader = File.OpenText(statistic_file);
                string line;

                Console.WriteLine("    => Лучшие игроки <=\n");
                while ((line = reader.ReadLine()) != null)
                {
                    string[] for_lines = line.Split(',');
                    Console.WriteLine($"{for_lines[0],-20}{for_lines[1],7}");
                }
            }
        }

        // запись статистики
        public void SafeStatistic()
        {
            IList<string> names = new List<string>(); // для имен из файла
            IList<int> scores = new List<int>(); // для счета из файла           

            if (File.Exists(statistic_file)) {
                // предварительно читаем файл 
                using (TextReader reader = File.OpenText(statistic_file))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] for_lines = line.Split(','); 
                        names.Add(for_lines[0]); // заполняем  List
                        scores.Add(int.Parse(for_lines[1]));
                    }
                }

                using TextWriter writer = File.CreateText(statistic_file);
                // проверка на количество записей в таблице лидеров
                // и по количеству очков
                if (scores.Count >= 10 && Score > scores[9])
                {
                    names.RemoveAt(9);
                    scores.RemoveAt(9);
                    names.Add(Name);
                    scores.Add(Score);
                    Console.WriteLine($"\nИгрок {Name} внесен в таблицу лидеров");
                }
                else if (scores.Count < 10)
                {
                    names.Add(Name);
                    scores.Add(Score);
                    Console.WriteLine($"\nИгрок {Name} внесен в таблицу лидеров");
                }

                SortStatistic(ref names, ref scores); // сортируем List по убыванию

                for (int i = 0; i < scores.Count; ++i) // сохраняем List в файл 
                    writer.WriteLine($"{names[i]},{scores[i]}"); 
            }
        }

        // сортировка счета по убыванию
        private void SortStatistic(ref IList<string> names ,ref IList<int> scores)
        {
            int score_tmp = 0;
            string name_tmp = "";

            for (int i = 0; i < scores.Count -1; ++i)
            {
                for(int j = i +1; j < scores.Count; ++j)
                {
                    if (scores[i] < scores[j])
                    {
                        score_tmp = scores[j];                        
                        scores[j] = scores[i];
                        scores[i] = score_tmp;

                        name_tmp = names[j];
                        names[j] = names[i];
                        names[i] = name_tmp;
                    }
                }
            }   
        }

    }
}
