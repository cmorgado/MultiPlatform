using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPlatform.Domain.Models.REST
{
    public class ErrorMessage
    {
        private bool _HasError = false;
        public bool HaError
        {
            get { return _HasError; }
            set { _HasError = value; }
        }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
    }
}
