using PersonDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonDb.Services.Interfaces
{
    public interface IPersonService
    {
        List<Persons> Get();

        Persons Get(string id);

        Persons Create(Persons person);

        void Update(string id, Persons persons);
    }
}
