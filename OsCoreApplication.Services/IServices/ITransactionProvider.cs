using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsCoreApplication.Services
{
    public interface ITransactionProvider : IDisposable
    {
        void Begin(System.Data.IsolationLevel isolationLevel);
        void Commit();
        void RollBack();
    }
}
