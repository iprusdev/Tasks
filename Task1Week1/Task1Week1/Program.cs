using System;
main();
void main()
{
    while (true)
    {
        float FirstNum, SecondNum, result;
        string Arifmetic;
        FirstNum = GetNumber("Введите первое число");
        Arifmetic = GetOperator();
        SecondNum = GetNumber("Введите второе число");
        
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

float GetNumber(string prompt)
{
    float number;
    Console.WriteLine(prompt);
    while (!float.TryParse(Console.ReadLine(), out number))
    {
        Console.WriteLine("Ошибка: вы ввели не число! Попробуйте еще раз.");
    }
    return number;
}

string GetOperator()
{
    string operation;
    Console.WriteLine("Выберите операцию: +, -, /, *");
    while (true)
    {
        operation = Console.ReadLine();
        if (operation == "+" || operation == "-" || operation == "*" || operation == "/")
        {
            return operation;
        }
        else
        {
            Console.WriteLine("Ошибка: такой операции не существует! Выберите операцию: +, -, /, *");
        }
    }
}