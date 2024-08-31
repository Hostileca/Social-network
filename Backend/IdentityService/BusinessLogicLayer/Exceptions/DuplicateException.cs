namespace BusinessLogicLayer.Exceptions;

public class DuplicateException(
    string message)
    : Exception(message)
{
    
}