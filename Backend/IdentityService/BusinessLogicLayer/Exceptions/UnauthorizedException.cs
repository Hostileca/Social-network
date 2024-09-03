namespace BusinessLogicLayer.Exceptions;

public class UnauthorizedException(
    string message) 
    : Exception(message)
{
    
}