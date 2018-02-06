using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace Positivo.Dal.Interfaces
{
    public interface IConnection : IDisposable
    {
        SqlConnection Open();

        SqlConnection Find();

        void Close();

    }
}
