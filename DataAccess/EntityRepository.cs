using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public abstract class EntityRepository
    {
        protected string _connectionString = ConfigurationManager.ConnectionStrings["OrderManagementDb"].ConnectionString;
    }
}
