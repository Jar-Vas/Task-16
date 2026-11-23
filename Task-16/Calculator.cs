using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Task_16
{
    internal class Calculator
    {
        static Logger log = new Logger("log.log");
        static char[] delimiters = new char[5] { '^', '*', '/', '+', '-' };
        public static double Add(double x, double y) { return x + y; }
        public static double Subtract(double x, double y) {return x - y; }
        public static double Multiply(double x, double y) { return x * y; }
        public static double Divide(double x, double y) { if (y ==  0) throw new Exception("Error"); return x / y; }
        public static double Power(double x, double y) { if (x < 0) { throw new Exception("Error"); } else { return Math.Pow(x, y); } }

        private static List<Func<double, double, double>> function = new List<Func<double, double, double>> 
        { Power, Multiply, Divide, Add, Subtract };
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
        public double Calc(string eq)
        {
            Exception? ex;
            double ans = 0;
            try
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
                ans = numbers[0];
            }
            catch (Exception e)
            {
                log.Error("ERROR, eq = " + eq, e);
                log.Write("Calculation error, exception: " + e.ToString + ", completed at " + DateTime.Now);
            }
            log.Info("Complete calculation '" + eq + "' = " + ans);
            log.Write("Complete calculation '" + eq + "' = " + ans + ", completed at " + DateTime.Now);
            return ans;
        }
    }
}
