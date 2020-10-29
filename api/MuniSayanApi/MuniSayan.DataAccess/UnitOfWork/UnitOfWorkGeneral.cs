using Microsoft.Extensions.Configuration;
using Minedu.Comun.Data;
using MuniSayan.DataAccess.Contracts.UnitOfWork;
using System.Data;
using System.Data.Common;

namespace MuniSayan.DataAccess.UnitOfWork
{
    public partial class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        public UnitOfWork(IDbContext context) : base(context, true)
        {

        }
    }
}
