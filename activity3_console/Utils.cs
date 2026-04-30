namespace Exercise3;

public class Utils
{
    public void Separator()
    {
        Console.WriteLine("---------------------------");
    }
    public void Sleep(int ms)
    {
        Thread.Sleep(ms);
    }
    
    // Validations
    public bool validateNumber(int number)
    {
        if (number <= 0)
        {
            return false;
        }

        return true;
    }
}