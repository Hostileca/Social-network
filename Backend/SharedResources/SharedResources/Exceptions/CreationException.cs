namespace SharedResources.Exceptions;

public class CreationException(
    string message)
    : OperationFailedException(message)
{
    
}