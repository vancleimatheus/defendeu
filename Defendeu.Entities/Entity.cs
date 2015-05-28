using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defendeu.Entities
{
    public class Entity
    {
        [Key]
        public ObjectId Id { get; set; }
    }
}
