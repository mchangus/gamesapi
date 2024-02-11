namespace GamesApi.Domain.Models.Results
{
    /// <summary>
    /// Class to return results from business logic with data, messages and code
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultWithData<T>
    {
        private ResultWithData() { }

        private T? data { get; init; }

        public string Message { get; set; }

        public int ResponseCode { get; set; }

        public bool Succeeded { get; set; }

        public T GetDataOnSuccess() 
        {
            if (data is null) 
            { 
              throw new ArgumentNullException(nameof(data));
            }

            return data;
        }

        /// <summary>
        /// Create a new instance of the class for succesful result with data
        /// </summary>
        /// <param name="data"></param>
        /// <returns>a new instance of <see cref="ResultWithData{T}"/></returns>
        public static ResultWithData<T> Success(T data)
        {
            return new ResultWithData<T>
            {
                Message = string.Empty,
                Succeeded = true,
                data = data
            };

        }

        /// <summary>
        /// Create a new instance of the class for Failure result with Code
        /// </summary>
        /// <param name="message"></param>
        /// <param name="responseCode"></param>
        /// <returns>a new instance of <see cref="ResultWithData{T}"/></returns>
        public static ResultWithData<T> Failure(string message, int responseCode)
        {
            return new ResultWithData<T>
            {
                Message = message,
                ResponseCode = responseCode,
                data = default

            };
        }
    }
}
