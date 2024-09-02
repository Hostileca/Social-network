namespace BusinessLogicLayer.Exceptions;

public class AlreadyExistsException(
    string message)
    : Exception(message)
{
    
}