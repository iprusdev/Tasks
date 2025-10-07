using System;
using System.Collections.Generic;
using System.Text;

namespace Task1Week1
{
    using System;

    public static class Calculator
    {
        public static float Calculate(float FirstNum, string Arifmetic, float SecondNum)
        {
            switch (Arifmetic)
            {
                case "+":
                    return FirstNum + SecondNum;
                case "-":
                    return FirstNum - SecondNum;
                case "*":
                    return FirstNum * SecondNum;

                case "/":
                    if (SecondNum == 0)
                    {
                        throw new DivideByZeroException("Ошибка на ноль делить нельзя");
                    }
                    return FirstNum / SecondNum; 
                    
                default:
                    throw new ArgumentException("Передан неизвестный оператор", nameof(Arifmetic));
            }
        }
    }
}

