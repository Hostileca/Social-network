namespace BusinessLogicLayer.Exceptions;

public class ExpireException(
    string message) 
    : UnauthorizedException(message)
{
    
}