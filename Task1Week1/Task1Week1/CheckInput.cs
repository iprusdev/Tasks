using System;
using System.Collections.Generic;
using System.Text;

namespace Task1Week1
{
    public static class CheckInput
    {
        public static float GetNumber(string prompt)
        {
            float number;
            Console.WriteLine(prompt);
            while (!float.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Ошибка: вы ввели не число! Попробуйте еще раз.");
            }
            return number;
        }

        public static string GetOperator()
        {
            const string operation = "+-/*";
            Console.WriteLine("Выберите операцию: +, -, /, *");
            while (true)
            {
                string input = Console.ReadLine();
                if (input.Length==1 && operation.Contains(input))
                {
                    return input;
                }
                Console.WriteLine("Ошибка: такой операции не существует! Выберите операцию: +, -, /, *");
            }
        }
    }
}
