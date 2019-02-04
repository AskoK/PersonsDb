using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonDb.Models
{
    public class Persons
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string PersonName { get; set; }

        [BsonElement("Selection")]
        public string SelectionName { get; set; }

        [BsonElement("Consent")]
        public bool Consent { get; set; }
    }
}
