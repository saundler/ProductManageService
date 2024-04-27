namespace Infrastructure.Exception;

public class ProductException : System.Exception
{
    public ProductException(string message) : base(message)
    {
    }
}