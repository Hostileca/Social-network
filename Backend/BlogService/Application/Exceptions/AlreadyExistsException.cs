namespace Application.Exceptions;

public class AlreadyExistsException(
    string message) 
    : Exception(message)
{
    
}