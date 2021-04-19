using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourthWebAPI.Models
{
    public interface IBookstoreDatabaseSettings
    {
        string BookCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
