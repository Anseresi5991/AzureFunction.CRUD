using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunction.CRUD.Entities
{
    internal class User
    {
        User()
        {
            Id = Guid.NewGuid();
        }
        [BsonSerializer(typeof(MongoDB.Bson.Serialization.Serializers.GuidSerializer))]
        [BsonId]
        public Guid Id { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
