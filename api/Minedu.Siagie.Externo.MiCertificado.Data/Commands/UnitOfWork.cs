using Minedu.Core.NetConnect.SQL.Data;
using Minedu.Siagie.Externo.MiCertificado.Domain.Contract;

namespace Minedu.Siagie.Externo.MiCertificado.Data.Commands
{
    public partial class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        public UnitOfWork(string connectionString) : base(connectionString)
        {
        }
    }
}
