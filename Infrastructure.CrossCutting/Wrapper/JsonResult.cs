namespace Infrastructure.CrossCutting.Wrapper;

public class JsonResult<TData>
{
    public JsonResult()
    {
    }

    public JsonResult(TData data)
    {
        Data = data;
    }

    public JsonResult(TData data, string message)
    {
        Data = data;
        Message = message;
    }

    public string Message { get; set; }
    public TData Data { get; set; }
    public bool Warning { get; set; }
}