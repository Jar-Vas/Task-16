namespace Task_16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Calculator c = new Calculator();
            Console.WriteLine(c.Calc("((1)+2+2)", true));
        }
    }
}
