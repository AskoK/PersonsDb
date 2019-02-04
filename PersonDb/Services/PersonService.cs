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
    public class PersonService : IPersonService
    {
        private readonly IMongoCollection<Persons> _persons;

        public PersonService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("PersonsDb"));
            var database = client.GetDatabase("PersonsDb");
            _persons = database.GetCollection<Persons>("Persons");
        }

        public List<Persons> Get() => _persons.Find(person => true).ToList();

        public Persons Get(string id)
        {
            return _persons.Find<Persons>(person => person.Id == id).FirstOrDefault();
        }

        public Persons Create(Persons person)
        {
            _persons.InsertOne(person);
            return person;
        }

        public void Update(string id, Persons personIn)
        {
            _persons.ReplaceOne(person => person.Id == id, personIn);
        }
    }
}
