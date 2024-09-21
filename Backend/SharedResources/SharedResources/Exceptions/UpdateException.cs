namespace SharedResources.Exceptions;

public class UpdateException(
    string message)
    : OperationFailedException(message)
{
    
}