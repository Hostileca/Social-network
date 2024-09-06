namespace BusinessLogicLayer.Exceptions;

public class AlreadyExistsException(
    string message)
    : OperationFailedException(message)
{
    
}