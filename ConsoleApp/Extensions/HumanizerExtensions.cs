using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;

namespace ConsoleApp.Extensions
{
    public static class HumanizerExtensions
    {
        public static string InHuman(this string text)
        {
            return text.Humanize();
        }
        public static string InHuman(this DateTime dateTime)
        {
            return dateTime.Humanize();
        }
        public static string InWords(this int number)
        {
            return number.ToWords();
        }
        public static string InRomans(this int number)
        {
            return number.ToRoman();
        }
        public static string InRupees(this int number)
        {
            return $"rupee".ToQuantity(number, ShowQuantityAs.Words);
        }
        public static string InCurrencyWords(this int number, string currency)
        {
            return currency.ToQuantity(number, ShowQuantityAs.Words);
        }
    }
}