using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Task_16
{
    internal class Calculator
    {
        static char[] delimiters = new char[5] { '^', '*', '/', '+', '-' };
        public static double Add(double x, double y) { return x + y; }
        public static double Subtract(double x, double y) {return x - y; }
        public static double Multiply(double x, double y) { return x * y; }
        public static double Divide(double x, double y) { if (y ==  0) throw new Exception("Error"); return x / y; }
        public static double Power(double x, double y) { if (x < 0) { throw new Exception("Error"); } else { return Math.Pow(x, y); } }

        private static List<Func<double, double, double>> function = new List<Func<double, double, double>> 
        { Power, Multiply, Divide, Add, Subtract };
        private static Dictionary<char, Func<double, double, double>> func = new Dictionary<char, Func<double, double, double>>
        { {'+', Add }, {'*', Multiply }, {'-', Subtract }, {'/', Divide }, {'^', Power } };
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
                string[] items = eq.Split(delimiters);
                int count = items.Length;
                List<double> numbers = new List<double>();
                foreach (string s in items)
                {
                    double.TryParse(s, out double temp);
                    numbers.Add(temp);
                }
                List<char> signs = new List<char>();
                foreach (char c in eq)
                {
                    foreach (char c2 in delimiters)
                    {
                        
                        if (c == c2) { signs.Add(c); }
                    }
                }
                for (int i = 0; i < delimiters.Count(); i++)
                {

                    for (int j = signs.Count - 1; j >= 0; j--)
                    {
                        if (signs[j] == delimiters[i])
                        {
                            double res = function[i](numbers[j], numbers[j + 1]);
                            numbers[j] = res;
                            numbers.RemoveAt(j + 1);
                            signs.RemoveAt(j);
                        }
                    }
                }
                /*
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
                */
                return numbers[0];
            }
            else
            {
                Queue<int> queue = new Queue<int>();
                List<double> numbers = new List<double>();
                List<char> signs = new List<char>();

                bool k = false;
                int l = 0;
                for (int i = 0; i < eq.Length; i++)
                {
                    if (eq[i] == '(') { queue.Enqueue(i); k = false;  }
                    else if (eq[i] == ')')
                    {
                        if (k)
                        {
                            numbers.Add(Calc(eq[(l+2)..i]));

                            for (int i1 = 0; i1 < delimiters.Count(); i1++)
                            {

                                for (int j = signs.Count - 1; j >= 0; j--)
                                {
                                    if (signs[j] == delimiters[i1])
                                    {
                                        double res = function[i1+1](numbers[j], numbers[j + 1]);
                                        numbers[j] = res;
                                        numbers.RemoveAt(j + 1);
                                        signs.RemoveAt(j);
                                    }
                                }
                            }
                        }
                        else
                        {
                            numbers.Add(Calc(eq[(queue.Dequeue() + 1)..i], false));
                            if (i == eq.Length - 1) { continue; }
                            if (eq[i+1] != ')')
                            {
                                if (eq[i + 1] == '(') signs.Add('*');
                                else { signs.Add(eq[i + 1]); }
                            }
                        }
                        k = true;
                        l = i;
                    }
                }

                return numbers[0];

            }
            return 0.0d;
        }
    }
}
