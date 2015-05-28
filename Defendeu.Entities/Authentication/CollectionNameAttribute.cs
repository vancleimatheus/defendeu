using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defendeu.Attributes.Entities
{
    public class CollectionNameAttribute : Attribute
    {
        public CollectionNameAttribute(string collectionName)
        {
            this.CollectionName = collectionName;
        }

        public String CollectionName { get; set; }
    }
}
