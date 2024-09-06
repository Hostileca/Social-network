namespace Application.Exceptions;

public class AlreadyExistException(
    string message) 
    : Exception(message)
{
    
}