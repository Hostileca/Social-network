namespace BusinessLogicLayer.Result;

public static class AuthErrors
{
    public static readonly Error EmailAlreadyTaken = new Error(
        "Auth.EmailAlreadyTaken", "Email is already taken!");
    
    public static readonly Error IncorrectEmail = new Error(
        "Auth.IncorrectEmail", "Email is incorrect!");

    public static readonly Error IncorrectUserData = new Error(
        "Auth.IncorrectUserData", "Password or email is incorrect!");

    public static readonly Error UndefinedError = new Error(
        "Auth.UndefinedError", "Sorry! An error occured.");

}