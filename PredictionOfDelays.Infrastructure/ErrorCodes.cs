using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionOfDelays.Infrastructure
{
    public static class ErrorCodes
    {
        public static string EntityNotFound => "entity_not_found";
        public static string BadRequest => "bad_request";
        public static string DatabaseError => "database_error";
    }
}
