namespace SharedResources.Exceptions;

public class NoPermissionException(
    string message) 
    : Exception(message)
{
    
}