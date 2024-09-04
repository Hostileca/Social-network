namespace BusinessLogicLayer.Exceptions;

public class DeleteException(
    string message) 
    : OperationFailedException(message)
{
    
}