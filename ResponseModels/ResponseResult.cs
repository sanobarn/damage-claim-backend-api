namespace damage_assessment_api.ResponseModels
{

    public class ResponseResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = String.Empty; 
        public T? Data { get; set; }         

        public static ResponseResult<T> Success(T data, string message = "Request successful")
        {
            return new ResponseResult<T> { IsSuccess = true, Message = message, Data = data };
        }

        public static ResponseResult<T> Fail(string message)
        {
            return new ResponseResult<T> { IsSuccess = false, Message = message, Data = default };
        }

        public static ResponseResult<T> Fail(string message,List<string> errors)
        {
            return new ResponseResult<T> { IsSuccess = false, Message = $"{message} {string.Join('.',errors)}", Data = default };
        }
    }
    


}
