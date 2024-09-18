namespace SharedResources.Exceptions;

public class ExpireException(
    string message) 
    : UnauthorizedException(message)
{
    
}