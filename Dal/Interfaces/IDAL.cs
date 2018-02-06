using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace Positivo.Dal.Interfaces
{
    public interface IDAL<T> : IDisposable where T: class, new()
    {
        T insert(T model);

        void update(T model);

        bool remove(T model);

        T findPerCode(params Object[] keys);

        Collection<T> ListAll();
    }
}
