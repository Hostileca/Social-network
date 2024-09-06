namespace BusinessLogicLayer.Exceptions;

public class CreationException(
    string message)
    : OperationFailedException(message)
{
    
}