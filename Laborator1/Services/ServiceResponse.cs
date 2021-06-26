using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator1.Services
{
 
        public class ServiceResponse<TResponseOk, TResponseError>
        {
            public TResponseOk ResponseOk { get; set; }
            public TResponseError ResponseError { get; set; }
        }
}
