using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HoAssetManagement.StockScreener
{
    class Utility
    {
        public static decimal? GetDecimal(string input)
        {
            if (input == null) return null;
            if (input.Contains("%"))
            {
                input = input.Replace("%", "");
                input = input.Replace(" ", "");
                return Decimal.Parse(input) / 100;
            }
            else if (input.Contains("B"))
            {
                input = input.Replace("B", "");
                input = input.Replace(" ", "");
                return Decimal.Parse(input) * 1000000000;
            }
            else
            {
                decimal value;
                if (Decimal.TryParse(input.Replace(" ", ""), out value)) return value;
                return null;
            }


        }

        public static double? GetDouble(string input)
        {
            return (double?)GetDecimal(input);
        }

        
        public static DateTime? GetDateTime(string input)
        {
            if (input == null) return null;

            DateTime value;

            if (DateTime.TryParse(input, out value)) return value;
            return null;
        }

        public static IEnumerable<T> GetEnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings. 
            try
            {
                return Regex.Replace(strIn, @"[^\w\s\.\[\]\(\){}@:""%^,/\-]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters,  
            // we should return Empty. 
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        
        }
    }
}
