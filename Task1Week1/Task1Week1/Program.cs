using System;
using Task1Week1;
using static Task1Week1.CheckInput;
using static Task1Week1.Calculator; 
class Program
{ static void Main(string[] args)
    {
        while (true)
        {
            float FirstNum, SecondNum, result;
            string Arifmetic;
            try
            {
                FirstNum = GetNumber("Введите первое число");
                Arifmetic = GetOperator();
                SecondNum = GetNumber("Введите второе число");
                result = Calculate(FirstNum, Arifmetic, SecondNum);
                Console.WriteLine($"Результат = {result}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Введите число" +
                "\n1. Нажмите 1 если хотите выполнить новую операцию" +
                "\n2. Нажмите любую кнопку чтобы выйти из программы");

            string quitOrReturn = Console.ReadLine();
            if (quitOrReturn == "1")
            {
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Заверешение программы");
                Thread.Sleep(2000);
                break;
            }
        }
    }
}

