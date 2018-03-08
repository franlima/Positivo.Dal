using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Positivo.Log.Interfaces
{
    public interface ILog : IDisposable
    {
        int Load();

        int Save();

        string ToString();

        string ToCvs();

        string ToIni();

        string ToXml();

    }
}
