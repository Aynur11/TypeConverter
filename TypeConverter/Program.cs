using System;

namespace TypeConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Результат преобразования строки в double: {SafelyConverToDouble()}");
            Console.ReadKey();
        }

        /// <summary>
        /// Выполнить преобразование с обработкой ошибок ввода числа.
        /// </summary>
        /// <returns>Полученное число.</returns>
        public static double SafelyConverToDouble()
        {
            var converter = new StringConverter();
            double result = 0;
            bool isOk = false;
            Console.WriteLine("Введите число. Дробную часть необходимо указывать через точку (например: 83.2):");
            while (!isOk)
            {
                try
                {
                    string number = Console.ReadLine();
                    if (string.IsNullOrEmpty(number))
                    {
                        continue;
                    }
                    result = converter.ToDouble(number);
                    isOk = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Произошла ошибка при обработке введеной строки, повторите ввод.");
                    isOk = false;
                }
            }
            return result;
        }
    }
}
