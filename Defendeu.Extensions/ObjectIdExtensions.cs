using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defendeu.Extensions
{
    public static class ObjectIdExtensions
    {
        public static bool HasValue(this ObjectId objectId)
        {
            if (objectId == null)
                return false;

            if (objectId == new ObjectId())
                return false;

            return true;
        }
    }
}
