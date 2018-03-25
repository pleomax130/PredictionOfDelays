using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictionOfDelays.Core;

namespace PredictionOfDelays.Infrastructure
{
    public class ServiceException : PredictionOfDelaysException
    {
        public ServiceException(string code) : base(code)
        {
        }
    }
}
