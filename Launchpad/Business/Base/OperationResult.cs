
namespace Agap2It.Labs.Launchpad.Business.Base;
public class OperationResult
{

    public Exception? Exception { get; set; }
    public bool Success => Exception == null;


    public OperationResult(Exception ex)
    {
        Exception = ex;
    }

    public OperationResult() { }
}

public class OperationResult<T> : OperationResult
{
    public OperationResult(T result) 
    {
        Result = result;
    }
    public T? Result { get; set; }
}
