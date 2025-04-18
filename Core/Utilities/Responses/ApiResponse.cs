﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Responses
{
    public class ApiResponse
    {
        public ApiResponse()
        {

        }
        public ApiResponse(bool success)
        {
            Success = success;
        }
        public ApiResponse(bool success,string message)
        {
            Success = success;
            Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
