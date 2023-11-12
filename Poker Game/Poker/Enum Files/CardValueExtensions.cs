using Poker.Enum_Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    static class CardValueExtensions
    {
        private static string[] rus_name = {"2" , "3" , "4" , "5" , "6" , "7" , "8" , "9" , "10" , "В" , "Д" , "К" , "Т" , "Дж"};

        public static string RusName(this CardValue value) => rus_name[(int)value];
    }
}
