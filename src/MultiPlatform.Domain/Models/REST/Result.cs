using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPlatform.Domain.Models.REST
{
    public class RESTResult<T>
    {
        public Error Error { get; set; }
        public T DATA { get; set; }

        public RESTResult()
        {
            Error = new Error();
        }
    }
}
