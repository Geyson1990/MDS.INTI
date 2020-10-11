using MDS.Inventario.Api.Application.Entities.Models;
using MDS.Inventario.Api.DataAccess.Contracts.Entities;
using MDS.Inventario.Api.DataAccess.Contracts.Entities.Certificado;

namespace MDS.Inventario.Api.Application.Mappers
{
    public static class CargoMapper
    {
        public static CargoEntity Map(CargoExtends dto)
        {
            return new CargoEntity()
            {
                ID_CARGO = dto.IdCargo,
                DESCRIPCION_CARGO = dto.DescripcionCargo
            };
        }

        public static CargoExtends Map(CargoEntity entity)
        {
            return new CargoExtends()
            {
                IdCargo = entity.ID_CARGO,
                DescripcionCargo = entity.DESCRIPCION_CARGO
            };
        }
    }
}
