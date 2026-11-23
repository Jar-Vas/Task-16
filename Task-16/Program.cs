namespace Task_16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Calculator c = new Calculator();
            c.Calc("1+2+3^2/2");
            c.Calc("23^2");
            c.Calc("100-5^2");

        }
    }
}
