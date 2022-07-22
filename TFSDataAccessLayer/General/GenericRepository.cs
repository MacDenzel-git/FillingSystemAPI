 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.Repository;
using TFSDataAccessLayer.Models;

namespace TFSDataAccessLayer.General
{
    public class GenericRepository<T> : Repository<T> where T : class
    {
        public GenericRepository(TFSDatabaseContext dBContext) : base(dBContext)
        {

        }

        public GenericRepository() : this(new TFSDatabaseContext())
        {

        }
    }
}

// Scaffold-DbContext "Server=.;Database=TFSDatabase; Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models