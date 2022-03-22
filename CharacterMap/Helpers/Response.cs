using System;

namespace CharacterMap.Helpers
{
    public class Response
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public Response()
        {
            IsSuccess = false;
            Message = string.Empty;
        }
    }
}