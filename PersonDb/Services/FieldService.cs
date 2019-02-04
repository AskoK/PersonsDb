using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PersonDb.Models;
using PersonDb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonDb.Services
{
    public class FieldService : IFieldService
    {
        private readonly IMongoCollection<Fields> _fields;

        public FieldService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("PersonsDb"));
            var database = client.GetDatabase("PersonsDb");
            _fields = database.GetCollection<Fields>("Selections");
        }

        public List<Fields> Get() => _fields.Find(field => true).ToList();

        public Fields Get(string id)
        {
            return _fields.Find<Fields>(field => field.Id == id).FirstOrDefault();
        }
    }
}
