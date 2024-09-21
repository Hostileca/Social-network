namespace SharedResources.Exceptions;

public class DeleteException(
    string message) 
    : OperationFailedException(message)
{
    
}