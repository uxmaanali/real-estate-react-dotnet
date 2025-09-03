namespace RealEstate.Shared.Models;
public record ApiResponse<T>
{
    public bool Success { get; set; }
    public T Response { get; set; }
    public string Message { get; set; }

    private ApiResponse(bool success, T response, string message)
    {
        Success = success;
        Response = response;
        Message = message;
    }

    public static ApiResponse<T> SuccessResponse(T response, string message = null)
    {
        return new ApiResponse<T>(true, response, message);
    }

    public static ApiResponse<T> FailureResponse(string message, T response = default)
    {
        return new ApiResponse<T>(false, response, message);
    }

    public static ApiResponse<T> FailureResponse(string message)
    {
        return new ApiResponse<T>(false, default, message);
    }
}
