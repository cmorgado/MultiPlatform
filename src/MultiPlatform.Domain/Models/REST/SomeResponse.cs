using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPlatform.Domain.Models.REST
{

    /// <summary>
    /// This is the POCO of the JSON resquest response
    /// </summary>
    public class SomeResponse
    {
        public string Name { get; set; }
    }


    /// <summary>
    /// This is a upgraded response with some Error managing
    /// </summary>
    public class SomeResponseREST : SomeResponse
    {
        public ErrorMessage ErrorMessage { get; set; }
    }
}
