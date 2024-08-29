namespace BusinessLogicLayer.Result;

public class Results
{
    private Results(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public Error Error { get; }

    public static Results Success() => new(true, Error.None);

    public static Results Failure(Error error) => new(false, error);
}