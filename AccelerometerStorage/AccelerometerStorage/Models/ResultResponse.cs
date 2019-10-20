namespace AccelerometerStorage.WebApi
{
    public class ResultResponse
    {
        public bool IsSuccess { get; set; }

        public bool IsFailure { get; set; }

        public string Error { get; set; }

        public static ResultResponse Fail(string message)
        {
            return new ResultResponse
            {
                IsFailure = true,
                IsSuccess = false,
                Error = message,
            };
        }

        public static ResultResponse Ok()
        {
            return new ResultResponse
            {
                IsFailure = false,
                IsSuccess = true,
            };
        }
    }
}
