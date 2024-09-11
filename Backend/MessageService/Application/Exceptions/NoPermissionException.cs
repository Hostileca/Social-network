namespace Application.Exceptions;

public class NoPermissionException(
    string message) 
    : Exception(message);