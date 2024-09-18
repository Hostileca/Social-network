namespace SharedResources.Exceptions;

public class AlreadyExistsException(
    string message) 
    : Exception(message)
{
    
}