using PersonDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonDb.Services.Interfaces
{
    public interface IFieldService
    {
        List<Fields> Get();

        Fields Get(string id);
    }
}
