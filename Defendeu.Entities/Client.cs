using Defendeu.Entities.Authentication;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Defendeu.Extensions;
using Defendeu.Attributes.Entities;

namespace Defendeu.Entities
{
    [CollectionName("clients")]
    public class Client : Entity
    {
        private string _secret;
        
        [Required]
        public String ClientId { get; set; }
        [Required]
        public string Secret
        {
            get { return _secret; }
            set { _secret = value.GetHash(); }
        }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        [MaxLength(100)]
        public string AllowedOrigin { get; set; }
    }
}
