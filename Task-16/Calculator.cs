using System;
using System.Collections.Generic;
using System.Text;

namespace Task_16
{
    internal class Calculator
    {
        static char[] delimiters = new char[5] { '+', '-', '*', '/', '^' };
        public double Add(double x, double y) { return x + y; }
        public double Subtract(double x, double y) {return x - y; }
        public double Multiply(double x, double y) { return x * y; }
        public double Divide(double x, double y) { if (y ==  0) throw new Exception("Error"); return x / y; }
        public double Power(double x, double y) { if (x < 0) { throw new Exception("Error"); } else { return Math.Pow(x, y); } }
        //   ^
        //  <O>
        //   v
        public void normalazeSign(ref string s)
        {
            string sOut = "";
            for (int i = 0; i < s.Length-1; i++)
            {
                if (s[i] == '*' || s[i+1]  == '*') { sOut += '^'; i++; }
                else { sOut += s[i]; }
            }
        }
        public double Calc(string eq, bool p = true)
        {
            if (!p)
            {
                double? result;
                string[] items = eq.Split(delimiters);
                int count = items.Length;
                List<double> numbers = new List<double>();
                foreach (string s in items)
                {
                    double.TryParse(s, out double temp);
                    numbers.Add(temp);
                    Console.WriteLine("Test: " + temp);
                }
                List<char> signs = new List<char>();
                int i = 0;
                foreach (char c in eq)
                {
                    foreach (char c2 in delimiters)
                    {
                        
                        if (c == c2) { Console.WriteLine("Test: " + c); signs.Add(c); }
                    }
                }
                // step 1;
                for (int j = signs.Count - 1; i >= 0; i--)
                {
                    if (signs[i] == '^')
                    {
                        double res = Power(numbers[j], numbers[j + 1]);
                        numbers[j] = res;
                        numbers.RemoveAt(j + 1);
                        signs.RemoveAt(j);
                    }
                }
                // step 2;
                for (int j = signs.Count - 1; i >= 0; i--)
                {
                    if (signs[i] == '/')
                    {
                        double res = Divide(numbers[j], numbers[j + 1]);
                        numbers[j] = res;
                        numbers.RemoveAt(j + 1);
                        signs.RemoveAt(j);
                    }
                }
                // step 3;
                for (int j = 0; j < signs.Count; j++)
                {
                    if (signs[i] == '*')
                    {
                        double res = Multiply(numbers[j], numbers[j + 1]);
                        numbers[j] = res;
                        numbers.RemoveAt(j + 1);
                        signs.RemoveAt(j);
                    }
                }
                // step 4;
                for (int j = signs.Count - 1; i >= 0; i--)
                {
                    if (signs[i] == '-')
                    {
                        double res = Subtract(numbers[j], numbers[j + 1]);
                        numbers[j] = res;
                        numbers.RemoveAt(j + 1);
                        signs.RemoveAt(j);
                    }
                }
                // step 5;
                for (int j = 0; j < signs.Count; j++)
                {
                    if (signs[i] == '+')
                    {
                        double res = Add(numbers[j], numbers[j + 1]);
                        numbers[j] = res;
                        numbers.RemoveAt(j + 1);
                        signs.RemoveAt(j);
                    }
                }
                return numbers[0];
            }
            return 0.0d;
        }
    }
}
