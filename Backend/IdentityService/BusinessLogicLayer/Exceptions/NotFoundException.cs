namespace BusinessLogicLayer.Exceptions;

public class NotFoundException(
    string message) 
    : Exception(message)
{
    
}