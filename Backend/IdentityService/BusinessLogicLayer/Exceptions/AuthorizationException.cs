namespace BusinessLogicLayer.Exceptions;

public class AuthorizationException(
    string message)
    : Exception(message)
{
    
}