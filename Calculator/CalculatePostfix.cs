using System;
using System.Collections.Generic;

namespace Calculator
{
    public class CalculatePostfix
    {
        public CalculatePostfix()
        {
        }

        public string GetExpression(string input)
        {
            string output = string.Empty;

            Stack<char> operStack = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
            {
                if (IsDelimeter(input[i]))
                    continue;

                if (char.IsDigit(input[i]))
                {
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i];
                        i++;

                        if (i == input.Length) break;
                    }

                    output += " ";
                    i--;
                }
                else if (char.IsLetter(input[i]))
                {
                    return null;
                }

                if (IsOperator(input[i]))
                {
                    if (input[i] == '(')
                    {
                        operStack.Push(input[i]);
                    }
                    else if (input[i] == ')')
                    {
                        char s = operStack.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else
                    {
                        if (operStack.Count > 0)
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek()))
                                output += operStack.Pop().ToString() + " ";

                        operStack.Push(char.Parse(input[i].ToString()));
                    }
                }
            }

            while (operStack.Count > 0)
            {
                output += operStack.Pop() + " ";
            }
            return output;
        }

        public double Counting(string input)
        {
            double result = 0;

            Stack<double> temp = new Stack<double>();

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    string a = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        a += input[i];
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(double.Parse(a));
                    i--;
                }
                else if (char.IsLetter(input[i]))
                {
                    break;
                }
                else if (IsOperator(input[i]))
                {
                    double a = temp.Pop();
                    double b = temp.Pop();

                    switch (input[i])
                    {
                        case '+': result = b + a; break;
                        case '-': result = b - a; break;
                        case '*': result = b * a; break;
                        case '/':
                            if (a == 0)
                            {
                                Console.WriteLine("Error! Division by 0!");
                                break;
                            }
                            else
                            {
                                result = b / a; break;
                            }
                    }
                    temp.Push(result);
                }
            }
            return temp.Peek();
        }

        private bool IsDelimeter(char c)
        {
            if ((" =".IndexOf(c) != -1))
                return true;
            return false;
        }

        private bool IsOperator(char с)
        {
            if (("+-/*^()".IndexOf(с) != -1))
                return true;
            return false;
        }

        private byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 4;
                case '^': return 5;
                default: return 6;
            }
        }
    }
}
