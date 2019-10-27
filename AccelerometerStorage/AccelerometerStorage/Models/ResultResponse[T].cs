namespace AccelerometerStorage.WebApi
{
    public class ResultResponse<T> : ResultResponse
    {
        public T Value { get; set; }

        public static ResultResponse<T> Ok(T value)
        {
            return new ResultResponse<T>
            {
                IsFailure = false,
                IsSuccess = true,
                Value = value,
            };
        }
    }
}
