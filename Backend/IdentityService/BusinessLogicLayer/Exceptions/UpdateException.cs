namespace BusinessLogicLayer.Exceptions;

public class UpdateException(
    string message)
    : OperationFailedException(message)
{
    
}