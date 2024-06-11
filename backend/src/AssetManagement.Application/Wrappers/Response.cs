﻿namespace AssetManagement.Application.Wrappers
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public T Data { get; set; }

        public Response()
        {
            Succeeded = true;
        }

        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Errors = null;
            Data = data;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
    }
}