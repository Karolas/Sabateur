using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabateur
{
    interface IDataWorker
    {
        void GetData();
        void SaveData();
        void ReturnData();
    }
}
