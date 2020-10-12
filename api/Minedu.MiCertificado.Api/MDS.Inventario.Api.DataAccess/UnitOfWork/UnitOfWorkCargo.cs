using Minedu.Comun.Data;
using MDS.Inventario.Api.DataAccess.Contracts.Entities.Certificado;
using MDS.Inventario.Api.DataAccess.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MDS.Inventario.Api.Application.Entities.Models;

namespace MDS.Inventario.Api.DataAccess.UnitOfWork
{
    public partial class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        public async Task<int> InsertarCargo(CargoEntity request)
        {
            var parm = new Parameter[] {
                //new Parameter ({PARAMETRO_DEL_PROCEDIMIENTO_ALMACENADO},{VARIABLE DEL MÈTODO})
                new Parameter("@ID_CARGO" , request.ID_CARGO),
                new Parameter("@DESCRIPCION_CARGO" , request.DESCRIPCION_CARGO),
                new Parameter("@USUARIO" , request.USUARIO_CREACION)
            };

            try
            {
                var result = this.ExecuteScalar<int>(
                    "dbo.USP_MDS_INVENTARIO_CARGO_INSERT_UPDATE"
                    , CommandType.StoredProcedure
                    , ref parm
                );

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
