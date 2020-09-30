using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var newCalc = new CalculatePostfix();

                Console.WriteLine("Please choose the mode \n " +
                                 "c - input from console \n " +
                                 "f - input from the file \n " +
                                 "e - exit");

                string mode;

                do
                {
                    mode = Console.ReadLine();

                    if (string.IsNullOrEmpty(mode))
                    {
                        Console.WriteLine("Empty input, please try again");
                    }
                } while (string.IsNullOrEmpty(mode));

                if (mode == "c")
                {
                    Console.WriteLine("Please enter an expression:");

                    while (true)
                    {
                        string inputStr;

                        do
                        {
                            inputStr = Console.ReadLine();

                            if (string.IsNullOrEmpty(inputStr))
                            {
                                Console.WriteLine("Empty input, please try again");
                            }
                        } while (string.IsNullOrEmpty(inputStr));

                        var stringToPostfix = newCalc.GetExpression(inputStr);

                        if (stringToPostfix == null)
                        {
                            Console.WriteLine("Invalid Input! Please enter an expression without letters!");
                        }
                        else
                        {
                            var calcString = newCalc.Counting(stringToPostfix);

                            Console.WriteLine(calcString);
                        }
                    }
                }
                else if (mode == "f")
                {
                    Console.WriteLine("Please enter a path to the source file");

                    string inputStr;

                    do
                    {
                        inputStr = Console.ReadLine();

                        if (string.IsNullOrEmpty(inputStr))
                        {
                            Console.WriteLine("Empty input, please try again");
                        }
                    } while (string.IsNullOrEmpty(inputStr));

                    List<string> filePath = File.ReadAllLines(inputStr).ToList();

                    List<string> output = new List<string>();

                    foreach (var item in filePath)
                    {
                        var stringToPostfix = newCalc.GetExpression(item);

                        if (stringToPostfix == null)
                        {
                            output.Add("Invalid Input!");
                        }
                        else
                        {
                            var calcString = newCalc.Counting(stringToPostfix);
                            string resultToStr = calcString.ToString();
                            output.Add(resultToStr);
                        }
                    }

                    Console.WriteLine("Please enter a path for the result file");

                    string path = Console.ReadLine();

                    var newList = filePath.Join(output, s => filePath.IndexOf(s), i => output.IndexOf(i), (s, i) => new { sv = s, iv = i }).ToList();

                    using (TextWriter tw = new StreamWriter(path))
                    {
                        foreach (var item in newList)
                        {
                            tw.WriteLine(string.Format("{0} = {1}", item.sv, item.iv));
                        }
                    }

                    Console.WriteLine("Result has been successfully added to the file!");
                }
                else if (mode == "e")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input, please select one of the options provided!");
                }
                
            }
        }
    }
}
