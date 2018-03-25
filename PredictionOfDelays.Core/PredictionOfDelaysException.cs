using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionOfDelays.Core
{
    public class PredictionOfDelaysException : Exception
    {
        public string Code { get; }

        protected PredictionOfDelaysException(string code)
        {
            Code = code;
        }

    }
}
