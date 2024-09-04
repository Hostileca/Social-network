namespace BusinessLogicLayer.Exceptions;

public class OperationFailedException(
    string message) 
    : Exception(message)
{
    
}