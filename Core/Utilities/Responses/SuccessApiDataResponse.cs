using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Responses
{
    public class SuccessApiDataResponse<T>:ApiDataResponse<T>
    {
        public SuccessApiDataResponse(T data) : base(success: true)
        {
            Data = data;
        }
        public SuccessApiDataResponse(T data, string message) : base(success: true, message: message)
        {
            Data = data;
        }
    }
}
