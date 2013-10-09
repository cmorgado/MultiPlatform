using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPlatform.Domain.Models.REST
{
    public enum ErrorType { HTTP, JSON, OTHER};
    public class Error
    {
        private bool _HasError = false;
        public bool HasError
        {
            get { return _HasError; }
            set { _HasError = value; }
        }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public ErrorType Type { get; set; }
    }
}
