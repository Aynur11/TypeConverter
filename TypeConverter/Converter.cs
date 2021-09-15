using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeConverter
{
    /// <summary>
    /// Содержит методы приведения типов.
    /// </summary>
    public class StringConverter
    {
        private const string ErrorMessage = "Входное чило вероятно имеет неверный формат. Проверьте вводимое число на корректность.";

        private Dictionary<int, char> DigitCharCompliance { get; set; }

        public StringConverter()
        {
            DigitCharCompliance = new Dictionary<int, char>();
            DigitCharCompliance.Add(0, '0');
            DigitCharCompliance.Add(1, '1');
            DigitCharCompliance.Add(2, '2');
            DigitCharCompliance.Add(3, '3');
            DigitCharCompliance.Add(4, '4');
            DigitCharCompliance.Add(5, '5');
            DigitCharCompliance.Add(6, '6');
            DigitCharCompliance.Add(7, '7');
            DigitCharCompliance.Add(8, '8');
            DigitCharCompliance.Add(9, '9');
        }

        /// <summary>
        /// Конвертирует строку в Double.
        /// </summary>
        /// <param name="number">Число в строковом представлении.</param>
        /// <returns>Число конвертированное в Double.</returns>
        public double ToDouble(string number)
        {
            List<char> charDigits = number.ToList();
            List<double> digitsIntPart = new List<double>();
            List<double> digitsFractionalPart = new List<double>();

            bool hasMetPoint = false;
            bool hasMetMinusSign = false;
            bool hasMetPlusSign = false;
            bool hasMetNumber = false;
            bool isKnownSymbol = false;
            foreach (var digit in charDigits)
            {
                foreach (KeyValuePair<int, char> keyValuePair in DigitCharCompliance)
                {
                    if (digit == keyValuePair.Value && !hasMetPoint)
                    {
                        digitsIntPart.Add(keyValuePair.Key);
                        isKnownSymbol = true;
                        hasMetNumber = true;
                        break;
                    }
                    if (digit == keyValuePair.Value && hasMetPoint)
                    {
                        digitsFractionalPart.Add(keyValuePair.Key);
                        isKnownSymbol = true;
                        hasMetNumber = true;
                        break;
                    }
                    if (digit == '.')
                    {
                        if (hasMetPoint)
                        {
                            throw new FormatException(ErrorMessage);
                        }
                        hasMetPoint = true;
                        isKnownSymbol = true;
                        break;
                    }
                    if (digit == '+' && !hasMetPlusSign && !hasMetNumber)
                    {
                        hasMetPlusSign = true;
                        isKnownSymbol = true;
                        break;
                    }
                    if (digit == '-' && !hasMetMinusSign && !hasMetNumber)
                    {
                        hasMetMinusSign = true;
                        isKnownSymbol = true;
                        break;
                    }
                    isKnownSymbol = false;
                }
                if (!isKnownSymbol)
                {
                    throw new FormatException(ErrorMessage);
                }
            }
            if(hasMetPlusSign && hasMetMinusSign)
            {
                throw new FormatException(ErrorMessage);
            }

            double numberIntPart = MakeSingleNumberFromList(digitsIntPart);
            double numberFractionalPart = MakeSingleNumberFromList(digitsFractionalPart);
            numberFractionalPart /= Math.Pow(10, digitsFractionalPart.Count);
            double resultNumber = numberIntPart + numberFractionalPart;
            if (hasMetMinusSign)
            {
                resultNumber *= -1;
            }
            return resultNumber;
        }

        private static double MakeSingleNumberFromList(List<double> digitsIntPart)
        {
            double totalNumber = 0;
            int count = digitsIntPart.Count - 1;
            foreach (var digit in digitsIntPart)
            {
                var intPart = digit * Math.Pow(10, count);
                count--;
                totalNumber += intPart;
            }

            return totalNumber;
        }
    }
}
