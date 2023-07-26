﻿namespace Domain.Entities.Responses
{
    public class BaseAPIResponse<T>
    {
        public BaseAPIResponse()
        {
        }

        public BaseAPIResponse(T data)
        {
            Data = data;
        }
        public T? Data { get; set; }
        public int TotalData { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class BaseAPIResponse
    {
        public BaseAPIResponse()
        {
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class BaseAPIErrorResponse
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

    }
}
