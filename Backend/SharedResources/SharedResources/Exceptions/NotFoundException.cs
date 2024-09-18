namespace SharedResources.Exceptions;

public class NotFoundException(
    string type, string identifier) 
    : Exception($"Item type of {type} with identifier {identifier} not found") { };