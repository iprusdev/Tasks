using System;
main();
void main()
{
    while (true)
    {
        float FirstNum, SecondNum, result;
        string Arifmetic;
        Console.WriteLine("Введите первое число");
        while (true)
        {
            if (float.TryParse(Console.ReadLine(), out FirstNum))
            {
                break;
            }
            else
            {
                Console.WriteLine("Ошибка: вы ввели не число!");
            }
        }
        Console.WriteLine("Выберите операцию +,-,/,*");
        Arifmetic = Console.ReadLine();
        Console.WriteLine("Введите второе число");
        while (true)
        {
            if (float.TryParse(Console.ReadLine(), out SecondNum))
            {
                break;
            }
            else
            {
                Console.WriteLine("Ошибка: вы ввели не число!");
            }
        }
        switch (Arifmetic)
        {
            case "+":
                result = FirstNum + SecondNum;
                Console.WriteLine($"Результат = {result}");
                break;
            case "-":
                result = FirstNum - SecondNum;
                Console.WriteLine($"Результат = {result}");
                break;

            case "*":
                result = FirstNum * SecondNum;
                Console.WriteLine($"Результат = {result}");
                break;

            case "/":

                if (SecondNum != 0)
                {
                    result = FirstNum / SecondNum;
                    Console.WriteLine($"Результат = {result}");
                }
                else
                {
                    Console.WriteLine("Ошибка! На ноль делить нельзя");
                }
                break;
            default:
                Console.WriteLine("Неизвестная операция!");
                break;
        }

        Console.WriteLine("Введите число");
        Console.WriteLine("1. Нажмите 1 если хотите выполнить новую операцию");
        Console.WriteLine("2. Нажмите любую кнопку чтобы выйти из программы");
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
