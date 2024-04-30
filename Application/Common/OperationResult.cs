namespace Application.Common
{
    public class OperationResult<T>
    {
        public bool Success { get; }
        public string? ErrorMessage { get; }
        public int? ErrorCode { get; } // Error number ???
        public T? Data { get; }

        private OperationResult(bool success, T? data, string? errorMessage, int? errorCode)
        {
            Success = success;
            Data = data;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        public static OperationResult<T> Ok(T data)
        {
            return new OperationResult<T>(true, data, null, 200);
        }

        public static OperationResult<T> Fail(string errorMessage, int errorCode)
        {
            return new OperationResult<T>(false, default, errorMessage, errorCode);
        }
    }

    public class OperationResult
    {
        public bool Success { get; }
        public string? ErrorMessage { get; }
        public int? ErrorCode { get; }

        protected OperationResult(bool success, string? errorMessage, int? errorCode)
        {
            Success = success;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        public static OperationResult Ok()
        {
            return new OperationResult(true, null, 200);
        }

        public static OperationResult Fail(string errorMessage, int errorCode)
        {
            return new OperationResult(false, errorMessage, errorCode);
        }
    }
}
