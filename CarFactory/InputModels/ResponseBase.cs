using System;

namespace CarFactory.InputModels
{
    public class ResponseBase<T>
    {
        public T Data { get; set; }
        public Error Error { get; set; }

        public ResponseBase()
        {

        }

        public ResponseBase(T data)
        {
            this.Data = data;
        }

        public ResponseBase(Exception ex)
        {
            Error = new Error()
            {
                Message = ex.Message
            };
        }
    }

    public class Error
    {
        public string Message { get; set; }
        public string Type { get; set; }
    }
}
