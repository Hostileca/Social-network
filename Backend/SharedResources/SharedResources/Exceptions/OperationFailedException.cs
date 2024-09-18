namespace SharedResources.Exceptions;

public class OperationFailedException(
    string message) 
    : Exception(message)
{
    
}