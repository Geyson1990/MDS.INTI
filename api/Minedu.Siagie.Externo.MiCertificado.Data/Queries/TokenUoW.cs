using Minedu.Core.NetConnect.SQL.Data;
using Minedu.Siagie.Externo.MiCertificado.Domain.Entities;
using Minedu.Siagie.Externo.MiCertificado.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Serilog;

namespace Minedu.Siagie.Externo.MiCertificado.Data.Queries
{
    public class TokenUoW : BaseUnitOfWork, ITokenQuery
    {
        private int? _commandTimeOutSeconds = 30;
        public TokenUoW(string connectionString) : base(connectionString)
        {
            _commandTimeOutSeconds = 360;
        }

        public async Task<bool> TokenValido(string filtro)

        {
            var parm = new Parameter[]
            {
                new Parameter("@P_TOKEN", filtro)
            };

            try
            {
                var result = await this.ExecuteScalarAsync<bool>("dbo.USP_INS_TOKEN_VALIDAR", CommandType.StoredProcedure, parm, _commandTimeOutSeconds);
                return result;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "TokenUoW.cs -> TokenValido");
                throw;
            }
        }
    }
}
